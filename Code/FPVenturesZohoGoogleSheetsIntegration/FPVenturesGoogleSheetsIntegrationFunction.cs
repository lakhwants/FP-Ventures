using FPVenturesZohoGoogleSheetsIntegration.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using ZohoGoogleSheetsIntegration.Helper;

namespace FPVenturesZohoGoogleSheetsIntegration
{
	public  class FPVenturesGoogleSheetsIntegrationFunction
    {
        readonly IZohoAnalyticsService _zohoAnalyticsService;
        const string AzureFunctionName = "FPVenturesGoogleSheetsIntegrationFunction";
        public FPVenturesGoogleSheetsIntegrationFunction(IZohoAnalyticsService zohoAnalyticsService)
		{
            _zohoAnalyticsService = zohoAnalyticsService;

        }
        [Function(AzureFunctionName)]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
          FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger(AzureFunctionName);

            logger.LogInformation($"{AzureFunctionName}");
            logger.LogInformation($"{AzureFunctionName} Function called on {DateTime.Now}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            string csvString;
            var dateTime = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");

            logger.LogInformation($"Date {dateTime}");

            logger.LogInformation("Getting report from Zoho analytics");

            var conversionReportsModel = _zohoAnalyticsService.GetReports(dateTime);

           // logger.LogInformation($"Total records in report = {conversionReportsModel.response.result.rows.Count }");

            if (conversionReportsModel==null)
			{
                logger.LogInformation("Report is Empty");
                return response;
            }

            logger.LogInformation("Report recieved");

            logger.LogInformation("Generating CSV String");

            csvString = ModelToCSV.ToCSV(conversionReportsModel);

            // logger.LogInformation($"CSV String : {csvString}");
            ;
            csvString=csvString.Replace('$', ' ');
            response.WriteString(csvString.Replace("Callers","Caller's"));

            logger.LogInformation($"Execution Finished");

            return response;
        }
    }
}
