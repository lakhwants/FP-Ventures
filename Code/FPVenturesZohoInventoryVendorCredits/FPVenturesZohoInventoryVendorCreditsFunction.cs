using FPVenturesZohoInventoryVendorCredits.Models;
using FPVenturesZohoInventoryVendorCredits.Services.Interfaces;
using FPVenturesZohoInventoryVendorCredits.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventoryVendorCredits
{
    public class FPVenturesZohoInventoryVendorCreditsFunction
    {
        readonly IZohoInventoryService _zohoInventoryService;
        readonly IZohoLeadsService _zohoLeadsService;
        public ConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;
        const string AzureFunctionName = "FPVenturesZohoInventoryVendorCreditsFunction";

        public FPVenturesZohoInventoryVendorCreditsFunction(IZohoLeadsService zohoLeadsService, IZohoInventoryService zohoInventoryService, ConfigurationSettings zohoCRMAndInventoryConfiguration)
        {
            _zohoInventoryService = zohoInventoryService;
            _zohoLeadsService = zohoLeadsService;
            _zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfiguration;
        }

        [Function(AzureFunctionName)]
        public async Task RunAsync([TimerTrigger("%VendorCreditsSchedule%")] TimerInfo timerInfo, FunctionContext context)
        {

            List<string> removeVendors = new()
            {
               "Nationwide",
               "Google"
            };

            var logger = context.GetLogger(AzureFunctionName);

            //DateTime startDate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.Date.DayOfWeek - 8);
            //DateTime endDate = startDate.AddDays(7).AddSeconds(-1);

            DateTime endDate = new DateTime(2022, 8, 7).AddSeconds(-1);
            DateTime startDate = new(2022, 7, 31);

            logger.LogInformation($"Start Date - {startDate}");
            logger.LogInformation($"End Date - {endDate}");

            logger.LogInformation("Fetching Dispositions");
            var dispositions = _zohoLeadsService.GetZohoDispositions(startDate, endDate, logger);
            logger.LogInformation($"Total dispositions - {dispositions.Count}");

            logger.LogInformation("Fetching CRM Vendors");
            var vendorsCRM = _zohoLeadsService.GetVendors(logger);
            logger.LogInformation($"CRM Vendors fetched - {vendorsCRM.Data.Count}");

            logger.LogInformation("Fetching inventory Vendors");
            var vendorsInventory = _zohoInventoryService.GetVendors(logger);
            logger.LogInformation($"InventoryVendors fetched - {vendorsInventory.Contacts.Count}");

            logger.LogInformation("Fetching inventory items");
            var inventoryItems = _zohoInventoryService.GetInventoryItems(startDate, endDate, logger);
            logger.LogInformation($"CRM Vendors fetched - {inventoryItems.Count}");

            logger.LogInformation("Mapping vendor credits");
            var vendorCreditModels = ModelMapper.MapVendorCreditsModel(inventoryItems, vendorsCRM.Data.Where(x=> !removeVendors.Contains(x.VendorName)).ToList(), vendorsInventory.Contacts.Where(x=> !removeVendors.Contains(x.VendorName)).ToList(), dispositions, startDate, endDate,logger);

            logger.LogInformation("Posting vendor credits");
            var vendorCreditResponseModel = _zohoInventoryService.PostVendorCredits(vendorCreditModels, logger);

            LogVendorCreditResponseModel(vendorCreditResponseModel, logger);
            logger.LogInformation("Finished.....");

        }

        private static void LogVendorCreditResponseModel(List<ZohoInventoryVendorCreditResponseModel> vendorCreditResponseModels, ILogger logger)
        {
            foreach (var vendorCreditResponseModel in vendorCreditResponseModels)
            {
                logger.LogInformation($"{nameof(vendorCreditResponseModel.Message)} = {vendorCreditResponseModel.Message}");

                if (vendorCreditResponseModel == null)
                    continue;

                logger.LogInformation($"{nameof(vendorCreditResponseModel.VendorCredit.VendorName)} = {vendorCreditResponseModel.VendorCredit.VendorName}, " +
                        $"{nameof(vendorCreditResponseModel.VendorCredit.Status)} = {vendorCreditResponseModel.VendorCredit.Status}, " +
                        $"{nameof(vendorCreditResponseModel.VendorCredit.VendorCreditId)} = {vendorCreditResponseModel.VendorCredit.VendorCreditId} ");
            }
        }

    }
}
