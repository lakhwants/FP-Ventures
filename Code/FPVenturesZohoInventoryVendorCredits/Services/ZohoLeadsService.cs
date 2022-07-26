using FPVenturesZohoInventoryVendorCredits.Constants;
using FPVenturesZohoInventoryVendorCredits.Models;
using FPVenturesZohoInventoryVendorCredits.Services.Interfaces;
using FPVenturesZohoInventoryVendorCredits.Services.Mapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FPVenturesZohoInventoryVendorCredits.Services
{
    public class ZohoLeadsService : IZohoLeadsService
    {
        public string ZohoAccessToken = string.Empty;
        public ConfigurationSettings _configurationSettings;
        private IRestResponse<ZohoCRMDispositionResponseModel> response;

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

        public ZohoCRMVendorsResponseModel GetVendors(ILogger logger)
        {
            ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
            IRestResponse<ZohoCRMVendorsResponseModel> response;
            int page = 1;
            ZohoCRMVendorsResponseModel zohoCRMVendorsResponseModel = new();
            List<VendorCRM> vendors = new();

            try
            {
                do
                {

                    var client = new RestClient(_configurationSettings.ZohoCRMBaseUrl + _configurationSettings.ZohoCRMVendorsPath);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                    request.AddParameter("page", page);
                    response = client.Execute<ZohoCRMVendorsResponseModel>(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        var retryClient = new RestClient(_configurationSettings.ZohoCRMBaseUrl + _configurationSettings.ZohoCRMVendorsPath);
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
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + new StackTrace(ex).GetFrame(0).GetMethod().Name);
                logger.LogWarning(ex.InnerException.ToString());
            }

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

                foreach (var refundDisposition in ZohoInventoryRefundDispositions.RefundDispositions)
                {
                    var coqlStartDate = startDate;
                    var coqlEndDate = endDate;
                    do
                    {
                        zohoCOQLModel.Query = string.Format(_configurationSettings.DispositionCOQLQuery, refundDisposition, ModelMapper.GetDateString(coqlStartDate), ModelMapper.GetDateString(coqlEndDate));

                        var client = new RestClient(_configurationSettings.ZohoCRMBaseUrl + _configurationSettings.ZohoCOQLPath);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                        request.AddHeader("Content-Type", "application/json");
                        var body = JsonConvert.SerializeObject(zohoCOQLModel);
                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                        response = client.Execute<ZohoCRMDispositionResponseModel>(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            var retryClient = new RestClient(_configurationSettings.ZohoCRMBaseUrl + _configurationSettings.ZohoCRMVendorsPath);
                            var retryRequest = new RestRequest(Method.GET);
                            retryRequest.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
                            response = retryClient.Execute<ZohoCRMDispositionResponseModel>(retryRequest);
                        }

                        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                            break;

                        if (response.Data.info.MoreRecords)
                            coqlStartDate = response.Data.data.LastOrDefault().Timestamp;

                        dispositions.AddRange(response.Data.data);

                    } while (response.Data.info.MoreRecords);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
               logger.LogWarning("Method Name -"+ new StackTrace(ex).GetFrame(0).GetMethod().Name);
                logger.LogWarning(ex.InnerException.ToString());
            }

            return dispositions;
        }
    }
}
