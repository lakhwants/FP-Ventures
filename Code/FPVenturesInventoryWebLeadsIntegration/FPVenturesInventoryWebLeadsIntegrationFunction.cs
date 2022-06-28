using FPVenturesInventoryWebLeadsIntegration.Models;
using FPVenturesInventoryWebLeadsIntegration.Services.Interfaces;
using FPVenturesInventoryWebLeadsIntegration.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace FPVenturesInventoryWebLeadsIntegration
{
    public class FPVenturesInventoryWebLeadsIntegrationFunction
    {
        readonly IHAWXZohoLeadsService _hawxZohoLeadsService;
        const string AzureFunctionName = "FPVenturesInventoryWebLeadsIntegrationFunction";

        public FPVenturesInventoryWebLeadsIntegrationFunction(IHAWXZohoLeadsService hawxZohoLeadsService)
        {
            _hawxZohoLeadsService = hawxZohoLeadsService;
        }

        [Function(AzureFunctionName)]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger(AzureFunctionName);
            logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");
            HttpResponseData response;
            response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var body = new StreamReader(req.Body).ReadToEnd();

            if (body == null)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
                logger.LogInformation($"Body is null");
                return response;
            }

            var record = JsonConvert.DeserializeObject<ZohoInventoryRecordModel>(body);

            var hawxRecordResponse = _hawxZohoLeadsService.AddZohoLeadsToHawx(ModelMapper.MapZohoLeadsToHawxLeads(record));

            if (hawxRecordResponse.StatusCode == HttpStatusCode.Created)
                response = req.CreateResponse(HttpStatusCode.OK);

            LogReponse(logger, hawxRecordResponse.Data);
            return response;
        }

        private static void LogReponse(ILogger logger, ZohoResponseModel responseModel)
        {
            foreach (var errorModel in responseModel.Data)
            {
                logger.LogWarning($"{nameof(errorModel.Details.ApiName)} : {errorModel.Details.ApiName}," +
                        $"{nameof(errorModel.Message)} = {errorModel.Message}," +
                        $"{nameof(errorModel.Status)} = {errorModel.Status}");
            }
        }
    }
}
