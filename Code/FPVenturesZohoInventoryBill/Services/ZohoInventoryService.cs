using FPVenturesZohoInventoryBill.Helpers;
using FPVenturesZohoInventoryBill.Models;
using FPVenturesZohoInventoryBill.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryBill.Services
{
    public class ZohoInventoryService : IZohoInventoryService
    {
        public string ZohoAccessToken = string.Empty;
        public ConfigurationSettings _configurationSettings;

        public ZohoInventoryService(ConfigurationSettings configurationSettings)
        {
            _configurationSettings = configurationSettings;
        }

        /// <summary>
        /// Gets Access Token for Zoho
        /// </summary>
        /// <returns></returns>
        private string GetZohoAccessTokenFromRefreshToken()
        {
            var client = new RestClient(string.Format(_configurationSettings.ZohoAccessTokenFromRefreshTokenPath, _configurationSettings.ZohoInventoryRefreshToken, _configurationSettings.ZohoClientId, _configurationSettings.ZohoClientSecret));
            var request = new RestRequest(Method.POST);
            var response = client.Execute<ZohoAccessTokenModel>(request);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Data == null || response.Data.AccessToken == null)
                return null;

            return response.Data.AccessToken;
        }

        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate, ILogger logger)
        {
            List<InventoryItem> inventoryItems = new();
            int page = 1;
            IRestResponse<ZohoInventoryItemModel> response = null;
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            var client = new RestClient(_configurationSettings.ZohoInventoryBaseUrl + _configurationSettings.ZohoInventoryItemPath);
            do
            {
                try
                {
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddParameter("organization_id", _configurationSettings.ZohoInventoryOrganizationId);
                    request.AddParameter("page", page);
                    request.AddParameter("custom_field_2762310000006617897_start", startDate.ToInventoryDate());
                    request.AddParameter("custom_field_2762310000006617897_end", endDate.ToInventoryDate());
                    response = client.Execute<ZohoInventoryItemModel>(request);

                    if (response != null || response.Data != null || response.Data.Items.Any())
                        inventoryItems.AddRange(response.Data.Items);

                    if (response.Data.page_context.HasMorePage)
                        page++;
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                    logger.LogWarning("Method Name -" + ex.GetMethodName());
                    logger.LogWarning(ex.InnerException.ToString());
                }

            } while (response.Data.page_context.HasMorePage);


            return inventoryItems;
        }

        public ZohoInventoryVendorsResponseModel GetVendors(ILogger logger)
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            IRestResponse<ZohoInventoryVendorsResponseModel> response = null;
            int page = 1;
            ZohoInventoryVendorsResponseModel zohoInventoryItemGroupsListResponseModel = new();
            List<VendorInventory> contacts = new();

            do
            {
                try
                {
                    var client = new RestClient(_configurationSettings.ZohoInventoryBaseUrl + _configurationSettings.ZohoInventoryVendorsPath);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddParameter("organization_id", _configurationSettings.ZohoInventoryOrganizationId);
                    request.AddParameter("filter_by", "Status.CrmVendors");
                    request.AddParameter("page", page);
                    response = client.Execute<ZohoInventoryVendorsResponseModel>(request);

                    if (response.Data.PageContext.HasMorePage)
                    {
                        page++;
                    }

                    contacts.AddRange(response.Data.Contacts);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                    logger.LogWarning("Method Name -" + ex.GetMethodName());
                    logger.LogWarning(ex.InnerException.ToString());
                }

            } while (response.Data.PageContext.HasMorePage);

            zohoInventoryItemGroupsListResponseModel.Contacts = contacts;

            return zohoInventoryItemGroupsListResponseModel;
        }

        public List<ZohoInventoryBillsResponseModel> PostBills(List<ZohoInventoryBillsModel> zohoInventorySalesOrderModels, ILogger logger)
        {
            List<ZohoInventoryBillsResponseModel> zohoInventoryBillsResponseModels = new();

            try
            {
                ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
                foreach (var zohoInventorySalesOrderModel in zohoInventorySalesOrderModels)
                {
                    var client = new RestClient(_configurationSettings.ZohoInventoryBaseUrl + _configurationSettings.ZohoInventoryBillsPath);

                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "text/plain");
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

                    var body = JsonConvert.SerializeObject(zohoInventorySalesOrderModel);
                    request.AddParameter("text/plain", body, ParameterType.RequestBody);
                    request.AddParameter("organization_id", _configurationSettings.ZohoInventoryOrganizationId);
                    var response = client.Execute<ZohoInventoryBillsResponseModel>(request);

                    zohoInventoryBillsResponseModels.Add(response.Data);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }
            return zohoInventoryBillsResponseModels;
        }

    }
}
