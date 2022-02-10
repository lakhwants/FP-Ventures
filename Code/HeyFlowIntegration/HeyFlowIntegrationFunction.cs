using HeyFlowIntegration.Models;
using HeyFlowIntegration.Services.Interfaces;
using HeyFlowIntegration.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HeyFlowIntegration
{
	public class HeyFlowIntegrationFunction
	{
		readonly IZohoLeadsService _zohoLeadsService;
		private readonly HeyFlowZohoConfigurationSettings _heyFlowZohoConfigurationSettings;
		const string AzureFunctionName = "HeyFlowIntegrationFunction";

		public HeyFlowIntegrationFunction(IZohoLeadsService zohoLeadsService, HeyFlowZohoConfigurationSettings heyFlowZohoConfigurationSettings)
		{
			_zohoLeadsService = zohoLeadsService;
			_heyFlowZohoConfigurationSettings = heyFlowZohoConfigurationSettings;

		}

		[Function(AzureFunctionName)]
		public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
			FunctionContext executionContext)
		{
			var response = req.CreateResponse(HttpStatusCode.OK);
			response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

			response.WriteString("Done");
			var logger = executionContext.GetLogger(AzureFunctionName);

			StreamReader reader = new(req.Body);
			string body = reader.ReadToEnd();
			if (body == "{\"message\":\"Heyflow Webhook API successfully initialized\"}")
			{
				return response;
			}
			
			var heyFlowReponseModel = ModelMapper.MapHeyFlowToZohoLead(JsonConvert.DeserializeObject<HeyFlowReponseModel>(body));

			logger.LogInformation($"Function Started");
			var (zohoSuccessModels, zohoErrorModels) = _zohoLeadsService.AddHeyFlowLeads(heyFlowReponseModel,
																						_heyFlowZohoConfigurationSettings.ZohoRefreshToken,
																						_heyFlowZohoConfigurationSettings.ZohoClientId,
																						_heyFlowZohoConfigurationSettings.ZohoClientSecret,
																						"ZOHO");

			logger.LogInformation($"Zoho records added");
			if (zohoErrorModels != null)
			{
				LogErrorModels(logger, zohoErrorModels);
			}

			//var (hawxSuccessModels, hawxErrorModels) = _zohoLeadsService.AddHeyFlowLeads(heyFlowReponseModel,
			//																			_heyFlowZohoConfigurationSettings.HawxZohoRefreshToken,
			//																			_heyFlowZohoConfigurationSettings.HawxZohoClientId,
			//																			_heyFlowZohoConfigurationSettings.HawxZohoClientSecret,
			//																			"HAWX");
			//if (hawxErrorModels != null)
			//{
			//	LogErrorModels(logger, hawxErrorModels);
			//}

			//logger.LogInformation($"HAWX records added");

			logger.LogInformation($"Finished");
			return response;
		}

		private static void LogErrorModels(ILogger logger, List<ZohoErrorModel> errorModels)
		{
			foreach (var errorModel in errorModels)
			{
				logger.LogWarning($"{nameof(errorModel.ApiName)} : {errorModel.ApiName}," +
						$"{nameof(errorModel.Message)} = {errorModel.Message}," +
						$"{nameof(errorModel.Status)} = {errorModel.Status}," +
						$"{nameof(errorModel.LastName)} = {errorModel.LastName}," +
						$"{nameof(errorModel.Mobile)} = {errorModel.Mobile}," +
						$"{nameof(errorModel.FirstName)} = {errorModel.FirstName}," +
						$"{nameof(errorModel.Email)} = {errorModel.Email}," +
						$"{nameof(errorModel.ZipCode)} = {errorModel.ZipCode}," +
						$"{nameof(errorModel.PestIssueType)} = {errorModel.PestIssueType}," +
						$"{nameof(errorModel.ServiceType)} = {errorModel.ServiceType}");
			}
		}
	}
}
