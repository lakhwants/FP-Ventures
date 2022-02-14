using FPVenturesHAWXIntegration.Models;
using FPVenturesHAWXIntegration.Services.Interfaces;
using FPVenturesHAWXIntegration.Services.Mapper;
using FPVenturesHAWXIntegration.Shared;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesHAWXIntegration.Services
{
	public class HAWXZohoLeadsService : IHAWXZohoLeadsService
	{
		public string ZohoAccessToken = string.Empty;
		public ZohoHAWXConfigurationSettings _zohoHAWXConfigurationSettings;

		public HAWXZohoLeadsService(ZohoHAWXConfigurationSettings zohoHAWXConfigurationSettings)
		{
			_zohoHAWXConfigurationSettings = zohoHAWXConfigurationSettings;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_zohoHAWXConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _zohoHAWXConfigurationSettings.ZohoHAWXRefreshToken, _zohoHAWXConfigurationSettings.ZohoHAWXClientId, _zohoHAWXConfigurationSettings.ZohoHAWXClientSecret));
			
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || string.IsNullOrEmpty(response.Data.AccessToken))
				return null;

			return response.Data.AccessToken;
		}
		public List<Record> DuplicateZohoHAWXLeads(List<Data> zohoRecords)
		{

			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			List<Record> hawxZohoRecords = new();
			string criteriaString = string.Empty;
			var batches = Utility.BuildBatches(zohoRecords, 10);

			foreach (var batch in batches)
			{
				var lastRecord = batch.LastOrDefault();
				foreach (var fields in batch)
				{
					criteriaString += string.Format(_zohoHAWXConfigurationSettings.SearchCriteria, fields.Email);

					if (lastRecord.Email != fields.Email)
						criteriaString += _zohoHAWXConfigurationSettings.SearchOperator;
				}

				var client = new RestClient(_zohoHAWXConfigurationSettings.ZohoLeadsBaseUrl + string.Format(_zohoHAWXConfigurationSettings.ZohoCheckDuplicateLeadsPath, criteriaString));
				
				var request = new RestRequest(Method.GET);
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
				var response = client.Execute<HawxZohoLeadsModel>(request);

				criteriaString = string.Empty;

				if (response == null)
					return null;

				if (response.Data == null)
					continue;

				if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
					hawxZohoRecords.AddRange(response.Data.Data);
			}

			return hawxZohoRecords.Distinct().ToList();
		}

		public (List<Datum> successModels, List<ZohoErrorModel> errorModels) AddZohoLeadsToHawx(List<Record> hawxZohoLeadRecords)
		{
			List<Datum> data = new();
			List<ZohoErrorModel> errorData = new();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var batches = Utility.BuildBatches(hawxZohoLeadRecords, 100);
			var client = new RestClient(_zohoHAWXConfigurationSettings.ZohoLeadsBaseUrl + _zohoHAWXConfigurationSettings.ZohoAddLeadsPath);

			foreach (var batch in batches)
			{
				HawxZohoLeadsModel hawxZohoLeadsModelBatch = new()
				{
					Data = batch
				};

				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "text/plain");
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

				var body = JsonConvert.SerializeObject(hawxZohoLeadsModelBatch);
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

		private static void CreateErrorSuccessModels(List<ZohoErrorModel> errorData, List<Record> batch, IRestResponse<ZohoResponseModel> response)
		{
			var indicis = response.Data.Data.Select((c, i) => new { c.Status, c.Details.ApiName, c.Message, Index = i })
												 .Where(x => x.Status == Enums.Status.error.ToString())
												 .Select(x => new { x.Index, x.ApiName, x.Status, x.Message });

			foreach (var index in indicis)
			{
				var errorlist = batch.ElementAt(index.Index);

				var zohoErrorModel = ModelMapper.MapHawxLeadToHawxZohoErrorModel(errorlist);
				zohoErrorModel.Message = index.Message.ToUpper();
				zohoErrorModel.Status = index.Status.ToUpper();
				zohoErrorModel.ApiName = index.ApiName;

				errorData.Add(zohoErrorModel);
			}
		}

	}
}
