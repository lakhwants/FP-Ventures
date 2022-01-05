using Five9Test.Shared;
using FPVenturesFive9UnbounceDisposition.Models;
using FPVenturesFive9UnbounceDisposition.Services.Interfaces;
using FPVenturesFive9UnbounceDisposition.Services.Mapper;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesFive9UnbounceDisposition.Services
{
	public class ZohoService : IZohoService
	{
		public string ZohoAccessToken = string.Empty;
		public readonly Five9ZohoConfigurationSettings _five9ZohoConfigurationSettings;
		public ZohoService(Five9ZohoConfigurationSettings five9ZohoConfigurationSettings)
		{
			_five9ZohoConfigurationSettings = five9ZohoConfigurationSettings;
		}
		//public string SearchCriteria = "(Caller_ID:equals:{0})";

		/// <summary>
		/// Check for duplicate leads in ZOHO using the inbound ID
		/// </summary>
		/// <param name="ringbaRecords"></param>
		/// <returns></returns>
		public List<Data> GetZohoLeads(List<Five9Model> phoneNumbers)
		{
			string SearchCriteria = "(Phone:equals:{0})";
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			List<Data> zohoLeadsModels = new();
			ZohoLeadsModel zohoLeadsModelResponse = new();
			string criteriaString = "";
			var batches = Utility.BuildBatches(phoneNumbers.Select(x => x.DNIS).Distinct().ToList(), 10);

			foreach (var batch in batches)
			{
				var lastRecord = batch.LastOrDefault();
				foreach (var fields in batch)
				{
					criteriaString += string.Format(SearchCriteria, fields);

					if (lastRecord != fields)
						criteriaString += "or";
				}

				var client = new RestClient(_five9ZohoConfigurationSettings.ZohoBaseUrl + string.Format(_five9ZohoConfigurationSettings.ZohoCheckDuplicateLeadsPath, criteriaString))
				{
					Timeout = -1
				};
				var request = new RestRequest(Method.GET);
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
				var response = client.Execute<ZohoLeadsModel>(request);

				criteriaString = string.Empty;

				if (response == null)
					return null;

				if (response.Data == null)
					continue;

				if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
					zohoLeadsModels.AddRange(response.Data.Data.Where(x => x.IsUnbounceRecord));
			}

			return zohoLeadsModels.ToList();
		}

		/// <summary>
		/// Check for duplicate Disposition records in ZOHO using the inbound ID
		/// </summary>
		/// <param name="ringbaRecords"></param>
		/// <returns></returns>
		public List<CallDispositionRecordModel> DuplicateZohoDispositions(List<Five9Model> five9Models)
		{
			string SearchCriteria = "(Call_ID:equals:{0})";
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			List<CallDispositionRecordModel> callDispositionRecordModels = new();
			string criteriaString = "";
			var batches = Utility.BuildBatches(five9Models, 10);

			foreach (var batch in batches)
			{
				var lastRecord = batch.LastOrDefault();
				foreach (var fields in batch)
				{
					criteriaString += string.Format(SearchCriteria, fields.CallID);

					if (lastRecord.CallID != fields.CallID)
						criteriaString += "or";
				}

				var client = new RestClient(_five9ZohoConfigurationSettings.ZohoBaseUrl + string.Format(_five9ZohoConfigurationSettings.ZohoCheckDuplicateDispositionsPath, criteriaString))
				{
					Timeout = -1
				};
				var request = new RestRequest(Method.GET);
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
				var response = client.Execute<CallDispositionModel>(request);

				criteriaString = string.Empty;

				if (response == null)
					return null;

				if (response.Data == null)
					continue;

				if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
					callDispositionRecordModels.AddRange(response.Data.Data);
			}

			return callDispositionRecordModels.ToList();
		}

		public (List<Datum> successModels, List<ZohoCallDispositionErrorModel> errorModels) PostZohoDispositions(CallDispositionModel callDispositionModel)
		{
			List<Datum> data = new();
			List<ZohoCallDispositionErrorModel> errorData = new();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var batches = Utility.BuildBatches(callDispositionModel.Data, 100);
			var client = new RestClient(_five9ZohoConfigurationSettings.ZohoBaseUrl + _five9ZohoConfigurationSettings.ZohoAddDispositionsPath)
			{
				Timeout = -1
			};

			foreach (var batch in batches)
			{
				CallDispositionModel callDispositionModelBatch = new()
				{
					Data = batch
				};

				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "text/plain");
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

				var body = JsonConvert.SerializeObject(callDispositionModelBatch);
				request.AddParameter("text/plain", body, ParameterType.RequestBody);
				var response = client.Execute<ZohoResponseModel>(request);

				if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
					var requestRetry = new RestRequest(Method.POST);
					requestRetry.AddHeader("Content-Type", "text/plain");
					ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
					requestRetry.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
					requestRetry.AddParameter("text/plain", body, ParameterType.RequestBody);
					response = client.Execute<ZohoResponseModel>(request);
				}
				CreateErrorSuccessModels(errorData, batch, response);

				data.AddRange(response.Data.Data.Where(x => x.Status == Enums.Status.success.ToString()));
			}

			return (data, errorData);
		}

		private static void CreateErrorSuccessModels(List<ZohoCallDispositionErrorModel> errorData, List<CallDispositionRecordModel> batch, IRestResponse<ZohoResponseModel> response)
		{
			var indicis = response.Data.Data.Select((c, i) => new { c.Status, c.Details.ApiName, c.Message, Index = i })
												 .Where(x => x.Status == Enums.Status.error.ToString())
												 .Select(x => new { x.Index, x.ApiName, x.Status, x.Message });

			foreach (var index in indicis)
			{
				var errorModel = batch.ElementAt(index.Index);

				var callDispositionErrorModel = ModelMapper.MapCallDispostionsToCallDispostionsErrorModel(errorModel);
				callDispositionErrorModel.Message = index.Message.ToUpper();
				callDispositionErrorModel.Status = index.Status.ToUpper();
				callDispositionErrorModel.ApiName = index.ApiName;

				errorData.Add(callDispositionErrorModel);
			}
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_five9ZohoConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, "1000.53379e5113b42493099a326e6a0cddc9.99b05e07e74a28c86d08337febd12665", "1000.KGAQ5OHG3RREM70F468UA2MI7NXTCK", "16a37f8f68fb2bfe6a7eb4fa88177184a40c9adbb5"))
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
				return null;

			return response.Data.AccessToken;
		}
	}
}
