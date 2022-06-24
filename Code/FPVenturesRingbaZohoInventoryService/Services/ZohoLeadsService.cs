using FPVenturesRingbaZohoInventoryService.Models;
using FPVenturesRingbaZohoInventoryService.Services.Interfaces;
using RestSharp;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventory.Services
{
    public class ZohoLeadsService : IZohoLeadsService
    {
        public string ZohoAccessToken = string.Empty;
        public RingbaZohoConfigurationSettings _ringbaZohoConfigurationSettings;

        public ZohoLeadsService(RingbaZohoConfigurationSettings ringbaZohoConfigurationSettings)
        {
            _ringbaZohoConfigurationSettings = ringbaZohoConfigurationSettings;
        }

        /// <summary>
        /// Gets Access Token for Zoho
        /// </summary>
        /// <returns></returns>
        private string GetZohoAccessTokenFromRefreshToken()
        {
            var client = new RestClient(string.Format(_ringbaZohoConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _ringbaZohoConfigurationSettings.ZohoCRMRefreshToken, _ringbaZohoConfigurationSettings.ZohoClientId, _ringbaZohoConfigurationSettings.ZohoClientSecret))
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            var response = client.Execute<ZohoAccessTokenModel>(request);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;

            return response.Data.AccessToken;
        }

        public ZohoCRMVendorsResponseModel GetVendors()
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            IRestResponse<ZohoCRMVendorsResponseModel> response;
            int page = 1;
            ZohoCRMVendorsResponseModel zohoCRMVendorsResponseModel = new();
            List<VendorCRM> vendors = new();

            do
            {

                var client = new RestClient(_ringbaZohoConfigurationSettings.ZohoCRMBaseUrl + _ringbaZohoConfigurationSettings.ZohoCRMVendorsPath);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("page", page);
                response = client.Execute<ZohoCRMVendorsResponseModel>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
                    var retryClient = new RestClient(_ringbaZohoConfigurationSettings.ZohoCRMBaseUrl + _ringbaZohoConfigurationSettings.ZohoCRMVendorsPath);
                    var retryRequest = new RestRequest(Method.GET);
                    retryRequest.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    retryRequest.AddParameter("page", page);
                    response = retryClient.Execute<ZohoCRMVendorsResponseModel>(retryRequest);
                }

                if (response.Data.Info.MoreRecords)
                {
                    page++;
                }

                vendors.AddRange(response.Data.Data);

            } while (response.Data.Info.MoreRecords);

            zohoCRMVendorsResponseModel.Data = vendors;

            return zohoCRMVendorsResponseModel;
        }
    }
}
