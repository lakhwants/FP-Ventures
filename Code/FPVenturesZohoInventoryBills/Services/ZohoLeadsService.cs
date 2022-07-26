using FPVenturesZohoInventoryBills.Models;
using FPVenturesZohoInventoryBills.Services.Interfaces;
using RestSharp;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBills.Services
{
    public class ZohoLeadsService : IZohoLeadsService
    {
        public string ZohoAccessToken = string.Empty;
        public ConfigurationSettings _configurationSettings;

        public ZohoLeadsService(ConfigurationSettings configurationSettings)
        {
            _configurationSettings = configurationSettings;
        }

        /// <summary>
        /// Gets Access Token for Zoho
        /// </summary>
        /// <returns></returns>
        private string GetZohoAccessTokenFromRefreshToken()
        {
            var client = new RestClient(string.Format(_configurationSettings.ZohoAccessTokenFromRefreshTokenPath, _configurationSettings.ZohoCRMRefreshToken, _configurationSettings.ZohoClientId, _configurationSettings.ZohoClientSecret))
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

                var client = new RestClient(_configurationSettings.ZohoCRMBaseUrl + _configurationSettings.ZohoCRMVendorsPath);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("page", page);
                response = client.Execute<ZohoCRMVendorsResponseModel>(request);

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
