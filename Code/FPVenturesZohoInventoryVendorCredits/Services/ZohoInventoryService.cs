using FPVenturesZohoInventoryVendorCredits.Models;
using FPVenturesZohoInventoryVendorCredits.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryVendorCredits.Services
{
    public class ZohoInventoryService : IZohoInventoryService
    {
        public string ZohoAccessToken = string.Empty;
        public ZohoCRMAndInventoryConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;

        public ZohoInventoryService(ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfigurationSettings)
        {
            _zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfigurationSettings;
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
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
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

        //public void PostVendorCredits(List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels)
        //{
        //    var client = new RestClient("https://inventory.zoho.com/api/v1/vendorcredits?organization_id=758026604");
        //    client.Timeout = -1;

        //    foreach (var zohoInventoryPostVendorCreditModel in zohoInventoryPostVendorCreditModels)
        //    {
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
        //        request.AddHeader("Content-Type", "application/json");
        //        var body = zohoInventoryPostVendorCreditModel;
        //        request.AddParameter("application/json", body, ParameterType.RequestBody);
        //        IRestResponse response = client.Execute(request);
        //        Console.WriteLine(response.Content);
        //    }
        //}

        public ZohoInventoryVendorsResponseModel GetVendors()
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();

            IRestResponse<ZohoInventoryVendorsResponseModel> response;
            int page = 1;
            ZohoInventoryVendorsResponseModel zohoInventoryItemGroupsListResponseModel = new();
            List<VendorInventory> contacts = new();

            do
            {
                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoInventoryBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryVendorsPath);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("organization_id", _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryOrganizationId);
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

        public void PostVendorCredits(List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels)
        {

            var client = new RestClient("https://inventory.zoho.com/api/v1/vendorcredits?organization_id=758026604");

            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            foreach (var zohoInventoryPostVendorCreditModel in zohoInventoryPostVendorCreditModels)
            {

                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(zohoInventoryPostVendorCreditModel);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
            }
        }

    }
}
