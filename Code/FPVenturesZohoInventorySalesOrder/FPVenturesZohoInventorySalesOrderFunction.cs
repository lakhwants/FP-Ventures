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
        public ConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;
        const string AzureFunctionName = "FPVenturesZohoInventorySalesOrderFunction";

        public FPVenturesZohoInventorySalesOrderFunction(IZohoInventoryService zohoInventoryService, ConfigurationSettings zohoCRMAndInventoryConfiguration)
        {
            _zohoInventoryService = zohoInventoryService;
            _zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfiguration;
        }

        [Function(AzureFunctionName)]
        public async Task RunAsync([TimerTrigger("%SalesOrderSchedule%")] TimerInfo timerInfo, FunctionContext context)
        {
            string datetime = ModelMapper.GetDateString(DateTime.Now.Date.AddHours(-1));

            DateTime lastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            DateTime endDate = lastMonth.AddSeconds(-1);
            DateTime startDate = lastMonth.AddMonths(-1);

            var logger = context.GetLogger(AzureFunctionName);

            logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

            logger.LogInformation("Fetching Leads from ZOHO ......");

            logger.LogInformation($"{datetime}");

            logger.LogInformation("Fetching Items from Inventory");
            var inventoryItems = _zohoInventoryService.GetInventoryItems(startDate, endDate, logger);

            logger.LogInformation("Fetching Contacts from Inventory");

            var zohoInventoryContactsResponseModel = _zohoInventoryService.GetContactsFromZohoInventory(logger);
            var customerId = zohoInventoryContactsResponseModel.Contacts.Where(x => x.CustomerName == _zohoCRMAndInventoryConfigurationSettings.ZohoInventoryCustomerName).Select(y => y.ContactId).FirstOrDefault();

            logger.LogInformation("Mapping Zoho Sales order model");
            var zohoInventoryItems = ModelMapper.MapItemsForSalesOrder(inventoryItems, customerId, startDate, endDate);

            logger.LogInformation("Adding Sales Order");
            var zohoInventorySalesOrderResponseModel = _zohoInventoryService.PostSalesOrdertoZohoInventory(zohoInventoryItems, logger);
            LogSalesOrderReponseModels(logger, zohoInventorySalesOrderResponseModel);

            logger.LogInformation("Confirming Sales order");

            var salesOrderConfirmationResponse = _zohoInventoryService.ConfirmSalesOrder(zohoInventorySalesOrderResponseModel.SalesOrder.salesorder_id, logger);

            logger.LogInformation($"Sales Order status - {salesOrderConfirmationResponse.Message}");

            logger.LogInformation("Generating Invoice");
            var invoiceFromSalesOrderResponseModel = _zohoInventoryService.CreateInvoiceFromSalesOrder(zohoInventorySalesOrderResponseModel, logger);

            LogInvoiceReponseModels(logger, invoiceFromSalesOrderResponseModel);

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
                logger.LogWarning($"{nameof(item.Name)} = {(item.Name)}, " +
                    $"{nameof(item.Rate)} = {(item.Rate)}, " +
                    $"{nameof(item.TaxId)} = {(item.TaxId)} ");
            }
        }

        private static void LogSalesOrderReponseModels(ILogger logger, ZohoInventorySalesOrderResponseModel zohoInventoryResponseModel)
        {
            logger.LogWarning($"{nameof(zohoInventoryResponseModel.Message)} = {zohoInventoryResponseModel.Message}, " +
                $"{nameof(zohoInventoryResponseModel.SalesOrder.CustomerId)} = {zohoInventoryResponseModel.SalesOrder.CustomerId}, " +
                $"{nameof(zohoInventoryResponseModel.SalesOrder.CustomerName)} = {zohoInventoryResponseModel.SalesOrder.CustomerName} , " +
                $"{nameof(zohoInventoryResponseModel.SalesOrder.Date)} = {zohoInventoryResponseModel.SalesOrder.Date}, ");

            foreach (var item in zohoInventoryResponseModel.SalesOrder.LineItems)
            {
                logger.LogWarning($"{nameof(item.Name)} = {(item.Name)}, " +
                                  $"{nameof(item.Rate)} = {(item.Rate)}, " +
                                  $"{nameof(item.TaxId)} = {(item.TaxId)} ");
            }

        }
    }
}
