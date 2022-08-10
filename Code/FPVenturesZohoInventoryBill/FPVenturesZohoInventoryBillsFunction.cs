using FPVenturesZohoInventoryBill.Models;
using FPVenturesZohoInventoryBill.Services.Interfaces;
using FPVenturesZohoInventoryBill.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventoryBills
{
    public class FPVenturesZohoInventoryBillsFunction
    {
        readonly IZohoInventoryService _zohoInventoryService;
        readonly IZohoLeadsService _zohoLeadsService;
        ConfigurationSettings _configurationSettings;
        const string AzureFunctionName = "FPVenturesZohoInventoryBillsFunction";

        public FPVenturesZohoInventoryBillsFunction(IZohoInventoryService zohoInventoryService, IZohoLeadsService zohoLeadsService, ConfigurationSettings configurationSettings)
        {
            _zohoInventoryService = zohoInventoryService;
            _zohoLeadsService = zohoLeadsService;
            _configurationSettings = configurationSettings;
        }

        [Function(AzureFunctionName)]
        public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
        {
            //Need to remove them from the Bills as we do not buy them from the vendors
            List<string> removeVendors = new()
            {
                "Nationwide",
                "Google"
            };

            //DateTime startDate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.Date.DayOfWeek - 8);
            //DateTime endDate = startDate.AddDays(7).AddSeconds(-1);

            DateTime endDate = new DateTime(2022, 7, 31).AddSeconds(-1);
            DateTime startDate = new(2022, 7, 24);

            var logger = context.GetLogger(AzureFunctionName);

            logger.LogInformation($"StartDate - {startDate}");
            logger.LogInformation($"EndDate - {endDate}");

            logger.LogInformation("Fetching Inventory Items");
            var inventoryItems = _zohoInventoryService.GetInventoryItems(startDate, endDate, logger);
            logger.LogInformation($"Inventory Items - {inventoryItems.Count}");

            logger.LogInformation("Fetching Inventory Vendors");
            var inventoryVendors = _zohoInventoryService.GetVendors(logger);
            logger.LogInformation($"Inventory Vendors - {inventoryVendors.Contacts.Count}");

            logger.LogInformation("Fetching CRM Vendors");
            var crmVendors = _zohoLeadsService.GetVendors();
            logger.LogInformation($"Inventory Vendors - {crmVendors.Data.Count}");

            logger.LogInformation("Mapping items to the Item Groups");
            var inventoryBills = ModelMapper.MapBillsModel(inventoryItems.Where(x => x.CfPublisherName != null).ToList(), crmVendors.Data.Where(x => !removeVendors.Contains(x.VendorName)).ToList(), inventoryVendors.Contacts.Where(x => !removeVendors.Contains(x.VendorName)).ToList(), _configurationSettings.ZohoInventoryBillsAccountId, startDate, endDate, logger);

            logger.LogInformation("Posting Bills to Inventory");
            var billsResponseModel = _zohoInventoryService.PostBills(inventoryBills, logger);

            LogResponseModels(logger, billsResponseModel);
        }

        /// <summary>
        /// Logs the response model of the Zoho Inventory Bills
        /// </summary>
        /// <param name="logger">Ilogger object</param>
        /// <param name="zohoInventoryBillsResponseModels">Response model of Zoho inventory bills</param>
        private static void LogResponseModels(ILogger logger, List<ZohoInventoryBillsResponseModel> zohoInventoryBillsResponseModels)
        {
            foreach (var zohoInventoryBillsResponseModel in zohoInventoryBillsResponseModels)
            {

                logger.LogInformation($"{nameof(zohoInventoryBillsResponseModel.Message)} = {zohoInventoryBillsResponseModel.Message}, " +
                    $"{nameof(zohoInventoryBillsResponseModel.Bill.BillId)} = {zohoInventoryBillsResponseModel.Bill.BillId}, " +
                    $"{nameof(zohoInventoryBillsResponseModel.Bill.VendorId)} = {zohoInventoryBillsResponseModel.Bill.VendorId}, " +
                    $"{nameof(zohoInventoryBillsResponseModel.Bill.VendorName)} = {zohoInventoryBillsResponseModel.Bill.VendorName}");
            }
        }
    }
}
