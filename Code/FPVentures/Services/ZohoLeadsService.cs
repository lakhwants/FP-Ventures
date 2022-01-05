using FPVentures.Models;
using FPVentures.Services.Interfaces;
using FPVentures.Services.Mapper;
using FPVentures.Shared;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace FPVentures.Services
{
	public class ZohoLeadsService : IZohoLeadsService
	{
		public string ZohoAccessToken = string.Empty;
		public string SeacrchCriteria = "(Inbound_Call_ID:equals:{0})";
		public RingbaZohoConfigurationSettings _ringbaZohoConfigurationSettings;

		public ZohoLeadsService(RingbaZohoConfigurationSettings ringbaZohoConfigurationSettings)
		{
			_ringbaZohoConfigurationSettings = ringbaZohoConfigurationSettings;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_ringbaZohoConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _ringbaZohoConfigurationSettings.ZohoRefreshToken, _ringbaZohoConfigurationSettings.ZohoClientId, _ringbaZohoConfigurationSettings.ZohoClientSecret))
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
				return null;

			return response.Data.AccessToken;
		}

		/// <summary>
		/// Adds all the Leads to ZOHO
		/// </summary>
		/// <param name="zohoLeadsModel"></param>
		/// <returns></returns>
		public (List<Datum> successModels, List<ZohoErrorModel> errorModels) AddCallLogsToZoho(ZohoLeadsModel zohoLeadsModel)
		{
			List<Datum> data = new();
			List<ZohoErrorModel> errorData = new();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var batches = Utility.BuildBatches(zohoLeadsModel.Data, 100);
			var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoLeadsBaseUrl + _ringbaZohoConfigurationSettings.ZohoAddLeadsPath)
			{
				Timeout = -1
			};

			foreach (var batch in batches)
			{
				ZohoLeadsModel zohoLeadsModelBatch = new()
				{
					Data = batch
				};

				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "text/plain");
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

				var body = JsonConvert.SerializeObject(zohoLeadsModelBatch);
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

			return (data,errorData);
		}

		/// <summary>
		/// Check for duplicate leads in ZOHO using the inbound ID
		/// </summary>
		/// <param name="ringbaRecords"></param>
		/// <returns></returns>
		public List<string> DuplicateZohoLeads(List<Record> ringbaRecords)
		{

			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			List<string> InboundIds = new();
			ZohoLeadsModel zohoLeadsModelResponse = new();
			string criteriaString = "";
			var batches = Utility.BuildBatches(ringbaRecords, 10);

			foreach (var batch in batches)
			{
				var lastRecord = batch.LastOrDefault();
				foreach (var fields in batch)
				{
					criteriaString += string.Format(SeacrchCriteria, fields.InboundCallId);

					if (lastRecord.InboundCallId != fields.InboundCallId)
						criteriaString += "or";
				}

				var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoLeadsBaseUrl + string.Format(_ringbaZohoConfigurationSettings.ZohoCheckDuplicateLeadsPath, criteriaString))
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
					InboundIds.AddRange(response.Data.Data.Select(x => x.InboundCallID));
			}

			return InboundIds.Distinct().ToList();
		}
		
		private static void CreateErrorSuccessModels(List<ZohoErrorModel> errorData, List<Data> batch, IRestResponse<ZohoResponseModel> response)
		{
			var indicis = response.Data.Data.Select((c, i) => new { c.Status, c.Details.ApiName, c.Message, Index = i })
												 .Where(x => x.Status == Enums.Status.error.ToString())
												 .Select(x => new { x.Index, x.ApiName, x.Status, x.Message });

			foreach (var index in indicis)
			{
				var errorlist = batch.ElementAt(index.Index);

				var zohoErrorModel = ModelMapper.MapLeadToZohoErrorModel(errorlist);
				zohoErrorModel.Message = index.Message.ToUpper();
				zohoErrorModel.Status = index.Status.ToUpper();
				zohoErrorModel.ApiName = index.ApiName;

				errorData.Add(zohoErrorModel);
			}
		}


	}
}
