using FPVenturesZohoInventory.Models;
using FPVenturesZohoInventory.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace FPVenturesZohoInventory.Services
{
	public class ZohoInventoryService : IZohoInventoryService
	{
		public string ZohoAccessToken = string.Empty;
		public ZohoCRMAndInventoryConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;

		public ZohoInventoryService(ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfiguration)
		{
			_zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfiguration;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_zohoCRMAndInventoryConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryRefreshToken, _zohoCRMAndInventoryConfigurationSettings.ZohoClientId, _zohoCRMAndInventoryConfigurationSettings.ZohoClientSecret));
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Data == null || response.Data.AccessToken == null)
				return null;

			return response.Data.AccessToken;
		}

		public List<ZohoInventoryResponseModel> AddLeadsToZohoInventory(List<ZohoInventoryModel> zohoInventoryModels)
		{
			List<ZohoInventoryResponseModel> zohoInventoryResponseModel = new();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryAddItemPath)
			{
				Timeout = -1
			};

			foreach (var zohoInventoryModel in zohoInventoryModels)
			{
				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "text/plain");
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

				var body = JsonConvert.SerializeObject(zohoInventoryModel);
				request.AddParameter("text/plain", body, ParameterType.RequestBody);
				request.AddParameter("organization_id", "758026604");
				var response = client.Execute<ZohoInventoryResponseModel>(request);

				if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
					var requestRetry = new RestRequest(Method.POST);
					requestRetry.AddHeader("Content-Type", "text/plain");
					ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
					requestRetry.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
					requestRetry.AddParameter("text/plain", body, ParameterType.RequestBody);
					response = client.Execute<ZohoInventoryResponseModel>(request);
				}
				//CreateErrorSuccessModels(errorData, batch, response);

				zohoInventoryResponseModel.Add(response.Data);
			}

			return zohoInventoryResponseModel;
		}


	}
}
