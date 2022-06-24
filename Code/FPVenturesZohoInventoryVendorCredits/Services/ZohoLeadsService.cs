using FPVenturesZohoInventoryVendorCredits.Models;
using FPVenturesZohoInventoryVendorCredits.Services.Interfaces;
using FPVenturesZohoInventoryVendorCredits.Services.Mapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryVendorCredits.Services
{
    public class ZohoLeadsService : IZohoLeadsService
    {
        public string ZohoAccessToken = string.Empty;
        public ZohoCRMAndInventoryConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;
        private IRestResponse<ZohoCRMDispositionResponseModel> response;

        public ZohoLeadsService(ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfigurationSettings)
        {
            _zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfigurationSettings;
        }

        /// <summary>
        /// Gets Access Token for Zoho
        /// </summary>
        /// <returns></returns>
        private string GetZohoAccessTokenFromRefreshToken()
        {
            var client = new RestClient(string.Format(_zohoCRMAndInventoryConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _zohoCRMAndInventoryConfigurationSettings.ZohoCRMRefreshToken, _zohoCRMAndInventoryConfigurationSettings.ZohoClientId, _zohoCRMAndInventoryConfigurationSettings.ZohoClientSecret))
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

                var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoCRMBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoCRMVendorsPath);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                request.AddParameter("page", page);
                response = client.Execute<ZohoCRMVendorsResponseModel>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var retryClient = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoCRMBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoCRMVendorsPath);
                    var retryRequest = new RestRequest(Method.GET);
                    retryRequest.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    retryRequest.AddParameter("page", page);
                    response = retryClient.Execute<ZohoCRMVendorsResponseModel>(request);
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

        public List<DispositionModel> GetZohoDispositions(DateTime startDate, DateTime endDate, ILogger logger)
        {

            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            if (ZohoAccessToken == null)
                return null;

            List<DispositionModel> dispositions = new();
            ZohoCOQLModel zohoCOQLModel = new();

            try
            {
                do
                {
                    zohoCOQLModel.Query = string.Format(_zohoCRMAndInventoryConfigurationSettings.DispositionCOQLQuery, ModelMapper.GetDateString(startDate), ModelMapper.GetDateString(endDate));

                    var client = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoCRMBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoCOQLPath);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddHeader("Content-Type", "application/json");
                    var body = JsonConvert.SerializeObject(zohoCOQLModel);
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    response = client.Execute<ZohoCRMDispositionResponseModel>(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        var retryClient = new RestClient(_zohoCRMAndInventoryConfigurationSettings.ZohoCRMBaseUrl + _zohoCRMAndInventoryConfigurationSettings.ZohoCRMVendorsPath);
                        var retryRequest = new RestRequest(Method.GET);
                        retryRequest.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                        response = retryClient.Execute<ZohoCRMDispositionResponseModel>(request);
                    }


                    if (response.Data.info.MoreRecords)
                        startDate = response.Data.data.LastOrDefault().Timestamp;

                    dispositions.AddRange(response.Data.data);

                } while (response.Data.info.MoreRecords);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning(ex.InnerException.ToString());
            }

            return dispositions;
        }
    }
}
