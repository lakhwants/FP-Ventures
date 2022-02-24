using FPVenturesZohoInventorySalesOrder.Models;
using FPVenturesZohoInventorySalesOrder.Services.Interfaces;
using FPVenturesZohoInventorySalesOrder.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventory
{
	public class FPVenturesZohoInventorySalesOrderFunction
	{
		readonly IZohoInventoryService _zohoInventoryService;
		readonly IZohoLeadsService _zohoLeadsService;
		const string AzureFunctionName = "FPVenturesZohoInventorySalesOrderFunction";

		public FPVenturesZohoInventorySalesOrderFunction(IZohoLeadsService zohoLeadsService, IZohoInventoryService zohoInventoryService)
		{
			_zohoInventoryService = zohoInventoryService;
			_zohoLeadsService = zohoLeadsService;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			string datetime = ModelMapper.GetDateString(DateTime.Now.AddHours(-12));
			var logger = context.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			logger.LogInformation($"Fetching Leads from ZOHO ......");

			logger.LogInformation($"{datetime}");

			var zohoLeads = _zohoLeadsService.GetZohoLeads(datetime, logger);

			if (zohoLeads == null)
			{
				logger.LogInformation($"Zoho return NULL ..... Stopped");
			}

			if (!zohoLeads.Any())
			{
				logger.LogInformation($"No new Lead found");
			}

			logger.LogInformation($"Fetching Taxes from Inventory");
			var taxes = _zohoInventoryService.GetZohoInventoryTaxes();

			logger.LogInformation($"Fetching Items Ids from Inventory");
			var inventoryItems = _zohoInventoryService.GetInventoryItems(zohoLeads);

			logger.LogInformation($"Mapping Zoho Sales order model");
			var zohoInventoryItems = ModelMapper.MapItemsForSalesOrder(inventoryItems, taxes);

			logger.LogInformation($"Adding Sales Order");
			var ZohoInventorySalesOrderResponseModel = _zohoInventoryService.AddSalesOrdertoZohoInventory(zohoInventoryItems);
			LogReponseModels(logger, ZohoInventorySalesOrderResponseModel);

			logger.LogInformation($"Finished.....");

		}

		private static void LogReponseModels(ILogger logger, ZohoInventorySalesOrderResponseModel zohoInventoryResponseModel)
		{
			logger.LogWarning($"{nameof(zohoInventoryResponseModel.message)} = {zohoInventoryResponseModel.message}, " +
				$"{nameof(zohoInventoryResponseModel.salesorder.customer_id)} = {zohoInventoryResponseModel.salesorder.customer_id}, " +
				$"{nameof(zohoInventoryResponseModel.salesorder.customer_name)} = {zohoInventoryResponseModel.salesorder.customer_name} , " +
				$"{nameof(zohoInventoryResponseModel.salesorder.date)} = {zohoInventoryResponseModel.salesorder.date}, " +
				$"{nameof(zohoInventoryResponseModel.salesorder.description)} = {zohoInventoryResponseModel.salesorder.description}, " +
				$"{nameof(zohoInventoryResponseModel.salesorder.item_id)} = {zohoInventoryResponseModel.salesorder.item_id}, " +
				$"{nameof(zohoInventoryResponseModel.salesorder.rate)} = {zohoInventoryResponseModel.salesorder.rate}");

			foreach (var item in zohoInventoryResponseModel.salesorder.line_items)
			{
				logger.LogWarning($"{nameof(item.item_id)} = {(item.item_id)}, " +
					$"{nameof(item.name)} = {(item.name), }" +
					$"{nameof(item.rate)} = {(item.rate)}, " +
					$"{nameof(item.sku)} = {(item.sku)}, " +
					$"{nameof(item.tax_id)} = {(item.tax_id)}, " +
					$"{nameof(item.description)} = {(item.description)}");
			}

		}
	}
}
