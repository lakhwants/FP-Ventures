using FPVenturesZohoInventorySalesOrder.Models;
using FPVenturesZohoInventorySalesOrder.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
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

        public InventoryResponse ConfirmSalesOrder(string salesOrderId)
        {
            var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + String.Format(_zohoCRMAndInventoryConfigurationSettings.ZohoInventorySalesOrderConfirmPath, salesOrderId));
            var request = new RestRequest(Method.POST);
            request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
            request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
            var response = client.Execute<InventoryResponse>(request);
            return response.Data;
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

        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate)
        {
            List<InventoryItem> inventoryItems = new();
            int page = 1;
            IRestResponse<ZohoInventoryItemModel> response;
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            var client = new RestClient("https://inventory.zoho.com/api/v1/items");
            do
            {
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                request.AddParameter("page", page);
                request.AddParameter("custom_field_2762310000006617897_start", startDate.ToString("yyyy-MM-dd"));
                request.AddParameter("custom_field_2762310000006617897_end", endDate.ToString("yyyy-MM-dd"));
                response = client.Execute<ZohoInventoryItemModel>(request);

                if (response != null || response.Data != null || response.Data.Items.Any())
                    inventoryItems.AddRange(response.Data.Items);

                if (response.Data.page_context.HasMorePage)
                    page++;

            } while (response.Data.page_context.HasMorePage);


            return inventoryItems;
        }

        public ZohoInventorySalesOrderResponseModel PostSalesOrdertoZohoInventory(ZohoInventorySalesOrderModel zohoInventorySalesOrderModel)
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryAddSalesOrderPath);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

            var body = JsonConvert.SerializeObject(zohoInventorySalesOrderModel);
            request.AddParameter("text/plain", body, ParameterType.RequestBody);
            request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
            var response = client.Execute<ZohoInventorySalesOrderResponseModel>(request);

            return response.Data;
        }

        public ZohoInventoryInvoiceResponseModel PostInvoice(ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel)
        {
            var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryInvoicePath);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
            var body = JsonConvert.SerializeObject(zohoInventoryInvoiceRequestModel);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
            var response = client.Execute<ZohoInventoryInvoiceResponseModel>(request);

            return response.Data;
        }

        public ZohoInventoryContactsResponseModel GetContactsFromZohoInventory()
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryContactsPath);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
            request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);

            var response = client.Execute<ZohoInventoryContactsResponseModel>(request);
            return response.Data;
        }

        public ZohoInventoryContactPersonResponseModel GetContactPersonFromZohoInventory(string customerId)
        {
            var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + String.Format(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryContactPersonPath, customerId));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
            request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
            var response = client.Execute<ZohoInventoryContactPersonResponseModel>(request);

            return response.Data;

        }

    }
}
