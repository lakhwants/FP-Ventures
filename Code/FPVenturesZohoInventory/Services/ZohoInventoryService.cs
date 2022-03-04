using FPVenturesZohoInventory.Models;
using FPVenturesZohoInventory.Services.Interfaces;
using FPVenturesZohoInventory.Shared;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading;

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
			var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryAddItemPath);

			var batches = Utility.BuildBatches<ZohoInventoryModel>(zohoInventoryModels, 90);
			foreach (var batch in batches)
			{
				foreach (var zohoInventoryModel in batch)
				{
					var request = new RestRequest(Method.POST);
					request.AddHeader("Content-Type", "text/plain");
					request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

					var body = JsonConvert.SerializeObject(zohoInventoryModel);
					request.AddParameter("text/plain", body, ParameterType.RequestBody);
					request.AddParameter("organization_id", "758026604");
					var response = client.Execute<ZohoInventoryResponseModel>(request);

					if (response.Data.code == 43)
					{
						return null;
					}

					zohoInventoryResponseModel.Add(response.Data);
				}

				Thread.Sleep(2 * 1000);
			}

			return zohoInventoryResponseModel;
		}


	}
}
