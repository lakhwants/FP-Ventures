using FPVenturesZohoInventory.Models;
using FPVenturesZohoInventory.Services.Interfaces;
using FPVenturesZohoInventory.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventory
{
	public class FPVenturesZohoInventoryFunction
	{
		readonly IZohoInventoryService _zohoInventoryService;
		readonly IZohoLeadsService _zohoLeadsService;
		const string AzureFunctionName = "FPVenturesZohoInventoryFunction";

		public FPVenturesZohoInventoryFunction(IZohoLeadsService zohoLeadsService, IZohoInventoryService zohoInventoryService)
		{
			_zohoInventoryService = zohoInventoryService;
			_zohoLeadsService = zohoLeadsService;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%ItemSchedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			string datetime = GetDateString(DateTime.Now.AddHours(-10));
			var logger = context.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			logger.LogInformation("Fetching Leads from ZOHO ......");

			logger.LogInformation($"{datetime}");

			var zohoLeads = _zohoLeadsService.GetZohoLeads(datetime, logger);

			if (zohoLeads == null)
			{
				logger.LogInformation("Zoho return NULL ..... Stopped");
			}

			if (!zohoLeads.Any())
			{
				logger.LogInformation("No new Lead found");
			}

			logger.LogInformation("Mapping leads to Inventory items.....");
			var zohoInventoryItems = ModelMapper.MapZohoLeadsToZohoInventoryItems(zohoLeads);

			logger.LogInformation("Adding items to inventory");

			var zohoInventoryResponseModel = _zohoInventoryService.AddLeadsToZohoInventory(zohoInventoryItems);

			if (zohoInventoryResponseModel == null)
			{
				logger.LogError("Zoho request per minute limit exceed.....");
			}
			else
			{
				LogResponseModels(logger, zohoInventoryResponseModel);
			}

			logger.LogInformation("Finished.....");
		}
		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}

		private static void LogResponseModels(ILogger logger, List<ZohoInventoryResponseModel> zohoInventoryResponseModels)
		{
			foreach (var zohoInventoryResponseModel in zohoInventoryResponseModels)
			{
				logger.LogInformation($"{nameof(zohoInventoryResponseModel.message)} = {zohoInventoryResponseModel.message}");

				if (zohoInventoryResponseModel.item == null)
					continue;

				logger.LogInformation($"{nameof(zohoInventoryResponseModel.item.sku)} = {zohoInventoryResponseModel.item.sku}, " +
					$"{nameof(zohoInventoryResponseModel.item.name)} = {zohoInventoryResponseModel.item.sku}, " +
					$"{nameof(zohoInventoryResponseModel.item.item_id)} = {zohoInventoryResponseModel.item.sku}, " +
					$"{nameof(zohoInventoryResponseModel.item.sku)} = {zohoInventoryResponseModel.item.sku}, " +
					$"{nameof(zohoInventoryResponseModel.item.sku)} = {zohoInventoryResponseModel.item.sku}, " +
					$"{nameof(zohoInventoryResponseModel.item.sku)} = {zohoInventoryResponseModel.item.sku}, " +
					$"{nameof(zohoInventoryResponseModel.item.sku)} = {zohoInventoryResponseModel.item.sku}, ");

			}
		}
	}
}
