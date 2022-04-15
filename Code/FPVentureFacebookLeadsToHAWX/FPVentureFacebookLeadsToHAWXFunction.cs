using FPVentureFacebookLeadsToHAWX.Models;
using FPVentureFacebookLeadsToHAWX.Services.Interfaces;
using FPVentureFacebookLeadsToHAWX.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace FPVentureFacebookLeadsToHAWX
{
	public class FPVentureFacebookLeadsToHAWXFunction
    {
		readonly IHAWXZohoLeadsService _hawxZohoLeadsService;
		readonly IZohoLeadsService _zohoLeadsService;
		const string AzureFunctionName = "FPVentureFacebookLeadsToHAWXFunction";

		public FPVentureFacebookLeadsToHAWXFunction(IHAWXZohoLeadsService hawxZohoLeadsService, IZohoLeadsService zohoLeadsService)
		{
			_hawxZohoLeadsService = hawxZohoLeadsService;
			_zohoLeadsService = zohoLeadsService;
		}

		[Function(AzureFunctionName)]
		public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
			FunctionContext executionContext)
		{
			var response = req.CreateResponse(HttpStatusCode.OK);
			var logger = executionContext.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			logger.LogInformation($"Fetching Leads from ZOHO ......");


			var zohoLeads = _zohoLeadsService.GetZohoLeads( logger);

			if (zohoLeads == null)
			{
				logger.LogInformation($"Zoho return NULL ..... Stopped");
				response.WriteString("Zoho return NULL ..... Stopped");
				return response;
			}

			if (!zohoLeads.Any())
			{
				logger.LogInformation($"No new Lead found");
				response.WriteString("No new Lead found");
				return response;
			}

			logger.LogInformation($"Leads fetched : {zohoLeads.Count}");

			logger.LogInformation($"Fetching duplicate leads from HAWX's ZOHO .....");

			var hawxZohoLeads = _hawxZohoLeadsService.DuplicateZohoHAWXLeads(zohoLeads);

			logger.LogInformation($"Duplicate leads fetched : {hawxZohoLeads.Count}");

			var newLeads = zohoLeads.Where(x =>x.IsFacebookLead == false).ToList();

			if (!newLeads.Any())
			{
				logger.LogInformation($"No new Lead to add");
				response.WriteString("No new Lead to add");
				return response;
			}

			logger.LogInformation($"New leads to add : {newLeads.Count}");

			logger.LogInformation($"Adding new leads to Hawx's ZOHO .....");

			var (successModels, errorModels) = _hawxZohoLeadsService.AddZohoLeadsToHawx(ModelMapper.MapZohoLeadsToHawxLeads(newLeads));
			logger.LogInformation($"Leads added successfully = {successModels.Count}");
			logger.LogInformation($"Leads failed to add = {errorModels.Count}");

			LogErrorModels(logger, errorModels);
			logger.LogInformation($"Finish.....");
			response.WriteString("Finished");
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
						$"{nameof(errorModel.Company)} = {errorModel.Company}," +
						$"{nameof(errorModel.Mobile)} = {errorModel.Mobile}," +
						$"{nameof(errorModel.Phone)} = {errorModel.Phone}");
			}
		}
	}
}