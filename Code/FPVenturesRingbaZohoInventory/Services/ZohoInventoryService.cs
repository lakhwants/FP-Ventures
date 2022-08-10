using FPVenturesRingbaZohoInventory.Helpers;
using FPVenturesRingbaZohoInventory.Models;
using FPVenturesRingbaZohoInventory.Services.Interfaces;
using FPVenturesRingbaZohoInventory.Shared;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public List<ZohoInventoryResponseModel> AddLeadsToZohoInventory(List<List<ZohoInventoryModel>> zohoInventoryModelGroups, ILogger logger)
        {
            List<ZohoInventoryResponseModel> zohoInventoryResponseModel = new();
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryAddItemPath);

            foreach (var zohoInventoryModelGroup in zohoInventoryModelGroups)
            {
                // After every 100 items we have to wait for 1 minute
                // For safe side, We are making groups of 90 items only
                var batches = Utility.BuildBatches<ZohoInventoryModel>(zohoInventoryModelGroup, 90);
                foreach (var batch in batches)
                {
                    foreach (var zohoInventoryModel in batch)
                    {

                        try
                        {
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "text/plain");
                            request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

                            var body = JsonConvert.SerializeObject(zohoInventoryModel);
                            request.AddParameter("text/plain", body, ParameterType.RequestBody);
                            request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                            var response = client.Execute<ZohoInventoryResponseModel>(request);

                            if (response.Data.Code == 43)
                                return null;

                            zohoInventoryResponseModel.Add(response.Data);
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex.Message);
                            logger.LogWarning("Method Name -" + ex.GetMethodName());
                            logger.LogWarning(ex.InnerException.ToString());
                        }
                    }

                    // After every 100 items we need to wait for approx. 1 minute
                    // For safe side, We are waiting for 2 minutes 
                    Thread.Sleep(2 * 1000);
                }
            }

            return zohoInventoryResponseModel;
        }

        public ZohoInventoryItemGroupsListResponseModel GetItemGroupsList(ILogger logger)
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            IRestResponse<ZohoInventoryItemGroupsListResponseModel> response = null;
            int page = 1;
            ZohoInventoryItemGroupsListResponseModel zohoInventoryItemGroupsListResponseModel = new();
            List<Itemgroup> itemGroup = new();
            do
            {
                try
                {
                    var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryItemGroupsPath);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                    request.AddParameter("page", page);

                    response = client.Execute<ZohoInventoryItemGroupsListResponseModel>(request);

                    if (response.Data.PageContext.HasMorePage)
                        page++;

                    itemGroup.AddRange(response.Data.ItemGroups);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                    logger.LogWarning("Method Name -" + ex.GetMethodName());
                    logger.LogWarning(ex.InnerException.ToString());
                }

            } while (response.Data.PageContext.HasMorePage);

            zohoInventoryItemGroupsListResponseModel.ItemGroups = itemGroup;

            return zohoInventoryItemGroupsListResponseModel;
        }

        public List<ZohoInventoryItemGroupReponseModel> CreateItemGroups(List<ZohoInventoryPostItemGroupRequestModel> newGroups, ILogger logger)
        {

            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            List<ZohoInventoryItemGroupReponseModel> zohoInventoryItemGroupReponseModels = new();
            var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryItemGroupsPath);

            foreach (var newGroup in newGroups)
            {
                try
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
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                    logger.LogWarning("Method Name -" + ex.GetMethodName());
                    logger.LogWarning(ex.InnerException.ToString());
                }

            }
            return zohoInventoryItemGroupReponseModels;
        }

        public void DeleteItem(List<ZohoInventoryItemGroupReponseModel> items, ILogger logger)
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            foreach (var item in items)
            {
                try
                {
                    var client = new RestClient(string.Format(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryAddItemPath + "/{0}", item.ItemGroup.Items.FirstOrDefault().ItemId));
                    var request = new RestRequest(Method.DELETE);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                    request.AddHeader("Content-Type", "application/json");
                    var response = client.Execute(request);
                }
                catch (Exception ex)
                {

                    logger.LogWarning(ex.Message);
                    logger.LogWarning("Method Name -" + ex.GetMethodName());
                    logger.LogWarning(ex.InnerException.ToString());
                }
            }
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
                    var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoInventoryBaseUrl + _ringbaZohoConfigurationSettings.ZohoInventoryVendorsPath);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddParameter("organization_id", _ringbaZohoConfigurationSettings.ZohoInventoryOrganizationId);
                    request.AddParameter("filter_by", "Status.CrmVendors");
                    request.AddParameter("page", page);
                    response = client.Execute<ZohoInventoryVendorsResponseModel>(request);

                    if (response.Data.PageContext.HasMorePage)
                        page++;

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

    }
}
