using FPVenturesZohoInventorySalesOrder.Models;
using FPVenturesZohoInventorySalesOrder.Services.Interfaces;
using FPVenturesZohoInventorySalesOrder.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventorySalesOrder
{
	public class FPVenturesZohoInventorySalesOrderFunction
	{
		readonly IZohoInventoryService _zohoInventoryService;
		readonly IZohoLeadsService _zohoLeadsService;
		public ZohoCRMAndInventoryConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;
		const string AzureFunctionName = "FPVenturesZohoInventorySalesOrderFunction";

		public FPVenturesZohoInventorySalesOrderFunction(IZohoLeadsService zohoLeadsService, IZohoInventoryService zohoInventoryService, ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfiguration)
		{
			_zohoInventoryService = zohoInventoryService;
			_zohoLeadsService = zohoLeadsService;
			_zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfiguration;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%SalesOrderSchedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			string datetime = ModelMapper.GetDateString(DateTime.Now.Date.AddHours(-1));
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

			logger.LogInformation("Fetching Taxes from Inventory");
			var taxes = _zohoInventoryService.GetZohoInventoryTaxes();

			logger.LogInformation("Fetching Items Ids from Inventory");
			var inventoryItems = _zohoInventoryService.GetInventoryItems(zohoLeads);

			logger.LogInformation("Fetching Contacts from Inventory");

			var zohoInventoryContactsResponseModel = _zohoInventoryService.GetContactsFromZohoInventory();
			var customerId = zohoInventoryContactsResponseModel.Contacts.Where(x => x.CustomerName == _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryCustomerName).Select(y => y.ContactId).FirstOrDefault();

			logger.LogInformation("Mapping Zoho Sales order model");
			var zohoInventoryItems = ModelMapper.MapItemsForSalesOrder(inventoryItems, taxes, customerId);

			logger.LogInformation("Adding Sales Order");
			var ZohoInventorySalesOrderResponseModel = _zohoInventoryService.PostSalesOrdertoZohoInventory(zohoInventoryItems);
			LogReponseModels(logger, ZohoInventorySalesOrderResponseModel);

			logger.LogInformation("Confirming Sales order");

			var salesOrderConfirmationResponse = _zohoInventoryService.ConfirmSalesOrder(ZohoInventorySalesOrderResponseModel.SalesOrder.salesorder_id);

			logger.LogInformation($"Sales Order status - {salesOrderConfirmationResponse.Message}");

			logger.LogInformation("Fetching contacts from inventory");

			logger.LogInformation("Fetching contact details from inventory");

			var contactPeople = _zohoInventoryService.GetContactPersonFromZohoInventory(ZohoInventorySalesOrderResponseModel.SalesOrder.CustomerId);

			logger.LogInformation("Creating Invoice model");

			var invoiceRequestModel = ModelMapper.MapSalesOrderToInvoiceModel(ZohoInventorySalesOrderResponseModel, contactPeople);

			logger.LogInformation("Adding Invoice to inventory");
			var invoiceResponseModel = _zohoInventoryService.PostInvoice(invoiceRequestModel);

			LogInvoiceReponseModels(logger, invoiceResponseModel);

			logger.LogInformation("Finished.....");

		}

		private static void LogInvoiceReponseModels(ILogger logger, ZohoInventoryInvoiceResponseModel zohoInventoryInvoiceResponseModel1)
		{
			logger.LogWarning($"{nameof(zohoInventoryInvoiceResponseModel1.Invoice.InvoiceId)} = {zohoInventoryInvoiceResponseModel1.Invoice.InvoiceId}, " +
				$"{nameof(zohoInventoryInvoiceResponseModel1.Invoice.CustomerId)} = {zohoInventoryInvoiceResponseModel1.Invoice.CustomerId}, " +
				$"{nameof(zohoInventoryInvoiceResponseModel1.Invoice.SalesorderId)} = {zohoInventoryInvoiceResponseModel1.Invoice.SalesorderId} , " +
				$"{nameof(zohoInventoryInvoiceResponseModel1.Invoice.CustomerName)} = {zohoInventoryInvoiceResponseModel1.Invoice.CustomerName}, ");

			foreach (var item in zohoInventoryInvoiceResponseModel1.Invoice.LineItems)
			{
				logger.LogWarning($"{nameof(item.ItemId)} = {(item.ItemId)}, " +
					$"{nameof(item.Name)} = {(item.Name)}, " +
					$"{nameof(item.Rate)} = {(item.Rate)}, " +
					$"{nameof(item.SKU)} = {(item.SKU)}, " +
					$"{nameof(item.TaxId)} = {(item.TaxId)}, " +
					$"{nameof(item.Description)} = {(item.Description)}");
			}

		}

		private static void LogReponseModels(ILogger logger, ZohoInventorySalesOrderResponseModel zohoInventoryResponseModel)
		{
			logger.LogWarning($"{nameof(zohoInventoryResponseModel.Message)} = {zohoInventoryResponseModel.Message}, " +
				$"{nameof(zohoInventoryResponseModel.SalesOrder.CustomerId)} = {zohoInventoryResponseModel.SalesOrder.CustomerId}, " +
				$"{nameof(zohoInventoryResponseModel.SalesOrder.CustomerName)} = {zohoInventoryResponseModel.SalesOrder.CustomerName} , " +
				$"{nameof(zohoInventoryResponseModel.SalesOrder.Date)} = {zohoInventoryResponseModel.SalesOrder.Date}, ");

			foreach (var item in zohoInventoryResponseModel.SalesOrder.LineItems)
			{
				logger.LogWarning($"{nameof(item.ItemId)} = {(item.ItemId)}, " +
					$"{nameof(item.Name)} = {(item.Name)}, " +
					$"{nameof(item.Rate)} = {(item.Rate)}, " +
					$"{nameof(item.SKU)} = {(item.SKU)}, " +
					$"{nameof(item.TaxId)} = {(item.TaxId)}, " +
					$"{nameof(item.Description)} = {(item.Description)}");
			}

		}
	}
}
