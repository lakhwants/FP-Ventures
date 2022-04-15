using FPVentureFacebookLeadsToHAWX.Models;
using FPVentureFacebookLeadsToHAWX.Services.Interfaces;
using FPVentureFacebookLeadsToHAWX.Services.Mapper;
using FPVentureFacebookLeadsToHAWX.Shared;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace FPVentureFacebookLeadsToHAWX.Services
{
	public class HAWXZohoLeadsService : IHAWXZohoLeadsService
	{
		public string ZohoAccessToken = string.Empty;
		public string SeacrchCriteria = "(Email:equals:{0})";
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
			var client = new RestClient(string.Format(_zohoHAWXConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _zohoHAWXConfigurationSettings.ZohoHAWXRefreshToken, _zohoHAWXConfigurationSettings.ZohoHAWXClientId, _zohoHAWXConfigurationSettings.ZohoHAWXClientSecret))
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
				return null;

			return response.Data.AccessToken;
		}
		public List<Record> DuplicateZohoHAWXLeads(List<Data> zohoRecords)
		{
			List<Record> hawxZohoRecords = new();
			var duplicateZohoLeads = zohoRecords.Where(leads => leads.IsFacebookLead == true).ToList();
			hawxZohoRecords = ModelMapper.MapZohoLeadsToHawxLeads(duplicateZohoLeads);
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
					data = batch
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
				var lead = new HawxZohoLeadsModel();
				lead.data = batch;
				UpdateCRMFacebookLead(lead);
			}

			return (data, errorData);
		}

		public void UpdateCRMFacebookLead(HawxZohoLeadsModel records)
		{
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var client = new RestClient(_zohoHAWXConfigurationSettings.ZohoLeadsBaseUrl + _zohoHAWXConfigurationSettings.ZohoAddLeadsPath);
			var request = new RestRequest(Method.PUT);
			request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
			var data = Utility.UpdateZohoCRMLeads(records);

			var body = JsonConvert.SerializeObject(data);
			request.AddParameter("text/plain", body, ParameterType.RequestBody);
			IRestResponse response = client.Execute(request);
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
