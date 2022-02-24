using FPVenturesHawx.Models;
using FPVenturesHawx.Services.Interfaces;
using FPVenturesHawx.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesHawx
{
	public class FPVenturesHawxFunction
	{
		readonly IHAWXZohoLeadsService _hawxZohoLeadsService;
		readonly IZohoLeadsService _zohoLeadsService;
		const string AzureFunctionName = "FPVenturesHawxFunction";

		public FPVenturesHawxFunction(IHAWXZohoLeadsService hawxZohoLeadsService, IZohoLeadsService zohoLeadsService)
		{
			_hawxZohoLeadsService = hawxZohoLeadsService;
			_zohoLeadsService = zohoLeadsService;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			string datetime = ModelMapper.GetDateString(DateTime.Now.Date.AddHours(-3));
			var logger = context.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Timer - {timerInfo.ScheduleStatus.Next}");
			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			logger.LogInformation($"Fetching Leads from ZOHO ......");

			logger.LogInformation($"{datetime}");

			var zohoLeads = _zohoLeadsService.GetZohoLeads(datetime, logger);

			if (zohoLeads == null)
			{
				logger.LogInformation($"Zoho return NULL ..... Stopped");
				return;
			}

			if (!zohoLeads.Any())
			{
				logger.LogInformation($"No new Lead found");
				return;
			}

			logger.LogInformation($"Leads fetched : {zohoLeads.Count}");

			logger.LogInformation($"Fetching duplicate leads from HAWX's ZOHO .....");

			var hawxZohoLeads = _hawxZohoLeadsService.DuplicateZohoHAWXLeads(zohoLeads);

			logger.LogInformation($"Duplicate leads fetched : {hawxZohoLeads.Count}");

			var newLeads = zohoLeads.Where(x => !hawxZohoLeads.Any(y => y.Email == x.Email)).ToList();

			if (!newLeads.Any())
			{
				logger.LogInformation($"No new Lead to add");
				return;
			}

			logger.LogInformation($"New leads to add : {newLeads.Count}");

			logger.LogInformation($"Adding new leads to Hawx's ZOHO .....");

			var (successModels, errorModels) = _hawxZohoLeadsService.AddZohoLeadsToHawx(ModelMapper.MapZohoLeadsToHawxLeads(newLeads));
			logger.LogInformation($"Leads added successfully = {successModels.Count}");
			logger.LogInformation($"Leads failed to add = {errorModels.Count}");

			LogErrorModels(logger, errorModels);
			logger.LogInformation($"Finish.....");

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
