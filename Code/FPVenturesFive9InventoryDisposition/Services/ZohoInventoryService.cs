using FPVenturesFive9InventoryDisposition.Models;
using FPVenturesFive9InventoryDisposition.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesFive9InventoryDisposition.Services
{
    public class ZohoInventoryService : IZohoInventoryService
    {
        public string ZohoAccessToken = string.Empty;
        public Five9ZohoInventoryConfigurationSettings _five9ZohoInventoryConfigurationSettings;

        public ZohoInventoryService(Five9ZohoInventoryConfigurationSettings five9ZohoInventoryConfigurationSettings)
        {
            _five9ZohoInventoryConfigurationSettings = five9ZohoInventoryConfigurationSettings;
        }

        /// <summary>
        /// Gets Access Token for Zoho
        /// </summary>
        /// <returns></returns>
        private string GetZohoAccessTokenFromRefreshToken()
        {
            var client = new RestClient(string.Format(_five9ZohoInventoryConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _five9ZohoInventoryConfigurationSettings.ZohoInventoryRefreshToken, _five9ZohoInventoryConfigurationSettings.ZohoClientId, _five9ZohoInventoryConfigurationSettings.ZohoClientSecret));
            var request = new RestRequest(Method.POST);
            var response = client.Execute<ZohoAccessTokenModel>(request);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Data == null || response.Data.AccessToken == null)
                return null;

            return response.Data.AccessToken;
        }

        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate)
        {
            List<InventoryItem> inventoryItems = new();
            int page = 1;
            IRestResponse<ZohoInventoryItemModel> response;
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            var client = new RestClient("https://inventory.zoho.com/api/v1/items");
            client.Timeout = -1;
            do
            {
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _five9ZohoInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                request.AddParameter("page", page);
                request.AddParameter("custom_field_2762310000006617897_start", startDate.ToString("yyyy-MM-dd"));
                request.AddParameter("custom_field_2762310000006617897_end", endDate.ToString("yyyy-MM-dd"));
                response = client.Execute<ZohoInventoryItemModel>(request);

                if (response.StatusCode == 0)
                {
                    response = client.Execute<ZohoInventoryItemModel>(request);
                }

                if (response != null || response.Data != null || response.Data.Items.Any())
                    inventoryItems.AddRange(response.Data.Items);


                if (response.Data.page_context.HasMorePage)
                    page++;

            } while (response.Data.page_context.HasMorePage);


            return inventoryItems;
        }

    }
}
