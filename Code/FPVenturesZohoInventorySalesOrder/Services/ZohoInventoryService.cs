using FPVenturesZohoInventorySalesOrder.Helpers;
using FPVenturesZohoInventorySalesOrder.Models;
using FPVenturesZohoInventorySalesOrder.Services.Interfaces;
using Microsoft.Extensions.Logging;
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
        public ConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;

        public ZohoInventoryService(ConfigurationSettings zohoCRMAndInventoryConfiguration)
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

        public InventoryResponse ConfirmSalesOrder(string salesOrderId, ILogger logger)
        {
            IRestResponse<InventoryResponse> response = null;
            try
            {
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + String.Format(_zohoCRMAndInventoryConfigurationSettings.ZohoInventorySalesOrderConfirmPath, salesOrderId));
                var request = new RestRequest(Method.POST);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                response = client.Execute<InventoryResponse>(request);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }
            return response.Data;

        }

        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate, ILogger logger)
        {
            List<InventoryItem> inventoryItems = new();
            int page = 1;
            IRestResponse<ZohoInventoryItemModel> response;
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            try
            {
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryItemPath);
                do
                {
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                    request.AddParameter("page", page);
                    request.AddParameter("custom_field_2762310000006617897_start", startDate.ToInventoryDate());
                    request.AddParameter("custom_field_2762310000006617897_end", endDate.ToInventoryDate());
                    response = client.Execute<ZohoInventoryItemModel>(request);

                    if (response != null || response.Data != null || response.Data.Items.Any())
                        inventoryItems.AddRange(response.Data.Items);

                    if (response.Data.page_context.HasMorePage)
                        page++;

                } while (response.Data.page_context.HasMorePage);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }

            return inventoryItems;
        }

        public ZohoInventorySalesOrderResponseModel PostSalesOrdertoZohoInventory(ZohoInventorySalesOrderModel zohoInventorySalesOrderModel, ILogger logger)
        {
            IRestResponse<ZohoInventorySalesOrderResponseModel> response = null;
            try
            {
                ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryAddSalesOrderPath);

                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/plain");
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

                var body = JsonConvert.SerializeObject(zohoInventorySalesOrderModel);
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                response = client.Execute<ZohoInventorySalesOrderResponseModel>(request);

            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }
            return response.Data;
        }

        public ZohoInventoryInvoiceResponseModel CreateInvoiceFromSalesOrder(ZohoInventorySalesOrderResponseModel zohoInventorySalesOrderResponseModel, ILogger logger)
        {
            IRestResponse<ZohoInventoryInvoiceResponseModel> response = null;
            try
            {
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + String.Format(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryInvoiceFromSalesOrderPath, zohoInventorySalesOrderResponseModel.SalesOrder.salesorder_id));
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                response = client.Execute<ZohoInventoryInvoiceResponseModel>(request);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }
            return response.Data;
        }

        public ZohoInventoryInvoiceResponseModel PostInvoice(ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel, ILogger logger)
        {
            IRestResponse<ZohoInventoryInvoiceResponseModel> response = null;
            try
            {
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryInvoicePath);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/plain");
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                var body = JsonConvert.SerializeObject(zohoInventoryInvoiceRequestModel);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                response = client.Execute<ZohoInventoryInvoiceResponseModel>(request);

            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }
            return response.Data;
        }

        public ZohoInventoryContactsResponseModel GetContactsFromZohoInventory(ILogger logger)
        {
            IRestResponse<ZohoInventoryContactsResponseModel> response = null;
            try
            {
                ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryContactsPath);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);

                response = client.Execute<ZohoInventoryContactsResponseModel>(request);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }
            return response.Data;
        }

        public ZohoInventoryContactPersonResponseModel GetContactPersonFromZohoInventory(string customerId, ILogger logger)
        {
            IRestResponse<ZohoInventoryContactPersonResponseModel> response = null;
            try
            {
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + String.Format(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryContactPersonPath, customerId));
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
                response = client.Execute<ZohoInventoryContactPersonResponseModel>(request);

            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }
            return response.Data;

        }

    }
}
