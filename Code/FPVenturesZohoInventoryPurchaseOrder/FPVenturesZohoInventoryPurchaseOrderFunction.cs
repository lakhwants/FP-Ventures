using FPVenturesZohoInventoryPurchaseOrder.Models;
using FPVenturesZohoInventoryPurchaseOrder.Services.Interfaces;
using FPVenturesZohoInventoryPurchaseOrder.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventoryPurchaseOrder
{
	public class FPVenturesZohoInventoryPurchaseOrderFunction
	{
		readonly IZohoInventoryService _zohoInventoryService;
		readonly IZohoLeadsService _zohoLeadsService;
		public ZohoCRMAndInventoryConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;
		const string AzureFunctionName = "FPVenturesZohoInventoryPurchaseOrderFunction";

		public FPVenturesZohoInventoryPurchaseOrderFunction(IZohoLeadsService zohoLeadsService, IZohoInventoryService zohoInventoryService, ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfiguration)
		{
			_zohoInventoryService = zohoInventoryService;
			_zohoLeadsService = zohoLeadsService;
			_zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfiguration;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%PurchaseOrderSchedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			string datetime = ModelMapper.GetDateString(DateTime.Now.Date.AddDays(-1));
			var logger = context.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			logger.LogInformation("Fetching Leads from ZOHO ......");

			logger.LogInformation($"{datetime}");

			var zohoLeads = _zohoLeadsService.GetZohoLeads(datetime, logger);

			if (zohoLeads == null)
			{
				logger.LogInformation("Zoho return NULL ..... Stopped");
				return;
			}

			if (!zohoLeads.Any())
			{
				logger.LogInformation("No new Lead found");
				return;
			}

			logger.LogInformation("Fetching Items Ids from Inventory");
			var inventoryItems = _zohoInventoryService.GetInventoryItems(zohoLeads);

			logger.LogInformation("Fetching Contacts from Inventory");

			var purchaseOrder = ModelMapper.MapItemsForPurchaseOrder(inventoryItems);
			logger.LogInformation("Adding Purchase Order");
			var zohoInventoryPurchaseOrderResponseModel = _zohoInventoryService.PostPurchaseOrdertoZohoInventory(purchaseOrder);
		}
	}
}
