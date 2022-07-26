using FPVenturesRingbaZohoInventoryService.Models;
using FPVenturesRingbaZohoInventoryService.Services.Interfaces;
using FPVenturesRingbaZohoInventoryService.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FPVenturesRingbaZohoInventory.Services
{
    public class ZohoInventoryService : IZohoInventoryService
    {
        public string ZohoAccessToken = string.Empty;
        public RingbaZohoConfigurationSettings _ringbaZohoConfigurationSettings;

        public ZohoInventoryService(RingbaZohoConfigurationSettings ringbaZohoConfigurationSettings)
        {
            _ringbaZohoConfigurationSettings = ringbaZohoConfigurationSettings;
        }

        /// <summary>
        /// Gets Access Token for Zoho
        /// </summary>
        /// <returns></returns>
        private string GetZohoAccessTokenFromRefreshToken()
        {
            var client = new RestClient(string.Format(_ringbaZohoConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _ringbaZohoConfigurationSettings.ZohoInventoryRefreshToken, _ringbaZohoConfigurationSettings.ZohoClientId, _ringbaZohoConfigurationSettings.ZohoClientSecret));
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
                request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
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

        public List<ZohoInventoryResponseModel> AddLeadsToZohoInventory(List<List<ZohoInventoryModel>> zohoInventoryModelGroups)
        {
            List<ZohoInventoryResponseModel> zohoInventoryResponseModel = new();
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryAddItemPath);

            IRestResponse<ZohoInventoryResponseModel> response;
            foreach (var zohoInventoryModelGroup in zohoInventoryModelGroups)
            {

                var batches = Utility.BuildBatches<ZohoInventoryModel>(zohoInventoryModelGroup, 90);
                foreach (var batch in batches)
                {
                    foreach (var zohoInventoryModel in batch)
                    {
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "text/plain");
                        request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

                        var body = JsonConvert.SerializeObject(zohoInventoryModel);
                        request.AddParameter("text/plain", body, ParameterType.RequestBody);
                        request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                        response = client.Execute<ZohoInventoryResponseModel>(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
                            var retryRequest = new RestRequest(Method.POST);
                            retryRequest.AddHeader("Content-Type", "text/plain");
                            retryRequest.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

                            retryRequest.AddParameter("text/plain", body, ParameterType.RequestBody);
                            retryRequest.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                            response = client.Execute<ZohoInventoryResponseModel>(retryRequest);
                        }
                        if (response.StatusCode == 0)
                        {
                            response = client.Execute<ZohoInventoryResponseModel>(request);
                        }
                        if (response.Data.Code == 43)
                        {
                            return null;
                        }

                        zohoInventoryResponseModel.Add(response.Data);
                    }

                    Thread.Sleep(2 * 1000);
                }
            }

            return zohoInventoryResponseModel;
        }

        public ZohoInventoryItemGroupsListResponseModel GetItemGroupsList()
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            IRestResponse<ZohoInventoryItemGroupsListResponseModel> response;
            int page = 1;
            ZohoInventoryItemGroupsListResponseModel zohoInventoryItemGroupsListResponseModel = new();
            List<Itemgroup> itemGroup = new();
            do
            {

                var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryItemGroupsPath);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                request.AddParameter("page", page);

                response = client.Execute<ZohoInventoryItemGroupsListResponseModel>(request);

                if (response.Data.PageContext.HasMorePage)
                {
                    page++;
                }

                itemGroup.AddRange(response.Data.ItemGroups);

            } while (response.Data.PageContext.HasMorePage);

            zohoInventoryItemGroupsListResponseModel.ItemGroups = itemGroup;

            return zohoInventoryItemGroupsListResponseModel;
        }

        public List<ZohoInventoryItemGroupReponseModel> CreateItemGroups(List<ZohoInventoryPostItemGroupRequestModel> newGroups)
        {

            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            List<ZohoInventoryItemGroupReponseModel> zohoInventoryItemGroupReponseModels = new();
            var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryItemGroupsPath);

            foreach (var newGroup in newGroups)
            {
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                var body = JsonConvert.SerializeObject(newGroup);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                var response = client.Execute<ZohoInventoryItemGroupReponseModel>(request);

                zohoInventoryItemGroupReponseModels.Add(response.Data);

            }
            return zohoInventoryItemGroupReponseModels;
        }

        public void DeleteItem(List<ZohoInventoryItemGroupReponseModel> items)
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            foreach (var item in items)
            {
                var client = new RestClient(string.Format(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryAddItemPath + "/{0}", item.ItemGroup.Items.FirstOrDefault().ItemId));
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                request.AddHeader("Content-Type", "application/json");
                var response = client.Execute(request);
            }
        }

        public ZohoInventoryVendorsResponseModel GetVendors()
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            IRestResponse<ZohoInventoryVendorsResponseModel> response;
            int page = 1;
            ZohoInventoryVendorsResponseModel zohoInventoryItemGroupsListResponseModel = new();
            List<VendorInventory> contacts= new();

            do
            {
                var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryVendorsPath);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                request.AddParameter("filter_by", "Status.CrmVendors");
                request.AddParameter("page", page);
                response = client.Execute<ZohoInventoryVendorsResponseModel>(request);

                if (response.Data.PageContext.HasMorePage)
                {
                    page++;
                }

                contacts.AddRange(response.Data.Contacts);

            } while (response.Data.PageContext.HasMorePage);

            zohoInventoryItemGroupsListResponseModel.Contacts = contacts;

            return zohoInventoryItemGroupsListResponseModel;
        }

    }
}
