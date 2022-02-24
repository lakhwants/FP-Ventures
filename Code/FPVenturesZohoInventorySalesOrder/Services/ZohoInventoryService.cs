using FPVenturesZohoInventorySalesOrder.Models;
using FPVenturesZohoInventorySalesOrder.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventorySalesOrder.Services
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
			var client = new RestClient("https://inventory.zoho.com/api/v1/settings/taxes?organization_id=758026604");
			client.Timeout = -1;
			var request = new RestRequest(Method.GET);
			request.AddHeader("Authorization", "Zoho-oauthtoken " + GetZohoAccessTokenFromRefreshToken());
			var response = client.Execute<ZohoInventoryTaxesModel>(request);
			return response.Data;
		}

		public List<InventoryItem> GetInventoryItems(List<Data> zohoLeads)
		{
			List<InventoryItem> inventoryItems = new();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var client = new RestClient("https://inventory.zoho.com/api/v1/items");
			foreach (var zohoLead in zohoLeads)
			{
				var request = new RestRequest(Method.GET);
				request.AddParameter("organization_id", "758026604");
				request.AddParameter("sku_contains", zohoLead.LeadId);
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
				var response = client.Execute<ZohoInventoryItemModel>(request);

				if (response != null || response.Data != null || response.Data.items.Any())
				{
					inventoryItems.AddRange(response.Data.items);
				}
			}

			return inventoryItems;
		}

		public ZohoInventorySalesOrderResponseModel AddSalesOrdertoZohoInventory(ZohoInventorySalesOrderModel zohoInventorySalesOrderModel)
		{
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryAddSalesOrderPath);

			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "text/plain");
			request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

			var body = JsonConvert.SerializeObject(zohoInventorySalesOrderModel);
			request.AddParameter("text/plain", body, ParameterType.RequestBody);
			request.AddParameter("organization_id", "758026604");
			var response = client.Execute<ZohoInventorySalesOrderResponseModel>(request);

			return response.Data;
		}

	}
}
