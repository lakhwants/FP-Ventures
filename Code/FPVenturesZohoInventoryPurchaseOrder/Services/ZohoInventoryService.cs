using FPVenturesZohoInventoryPurchaseOrder.Models;
using FPVenturesZohoInventoryPurchaseOrder.Services.Interfaces;
using FPVenturesZohoInventoryPurchaseOrder.Shared;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FPVenturesZohoInventoryPurchaseOrder.Services
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

		public ZohoInventoryTaxesModel GetZohoInventoryTaxes()
		{
			var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryTaxesPath);
			client.Timeout = -1;
			var request = new RestRequest(Method.GET);
			request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
			request.AddHeader("Authorization", "Zoho-oauthtoken " + GetZohoAccessTokenFromRefreshToken());
			var response = client.Execute<ZohoInventoryTaxesModel>(request);
			return response.Data;
		}

		public List<InventoryItem> GetInventoryItems(List<Data> zohoLeads)
		{
			List<InventoryItem> inventoryItems = new();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryItemPath);

			var batches = Utility.BuildBatches<Data>(zohoLeads, 90);

			foreach (var batch in batches)
			{
				foreach (var zohoLead in batch)
				{
					var request = new RestRequest(Method.GET);
					request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
					request.AddParameter(_zohoCRMAndInventoryConfigurationSettings.ZohoInventorySearchParameter, zohoLead.LeadId);
					request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
					var response = client.Execute<ZohoInventoryItemModel>(request);

					if (response != null || response.Data != null || response.Data.Items.Any())
					{
						inventoryItems.AddRange(response.Data.Items);
					}
				}

				Thread.Sleep(2 * 1000);
			}

			return inventoryItems;
		}

		public ZohoInventoryPurchaseOrderResponseModel PostPurchaseOrdertoZohoInventory(ZohoInventoryPurchaseOrderRequestModel zohoInventoryPurchaseOrderRequestModel)
		{
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoPurchaseOrderPath);

			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "text/plain");
			request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

			var body = JsonConvert.SerializeObject(zohoInventoryPurchaseOrderRequestModel);
			request.AddParameter("text/plain", body, ParameterType.RequestBody);
			request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
			var response = client.Execute<ZohoInventoryPurchaseOrderResponseModel>(request);

			return response.Data;
		}
	}
}
