using FPVenturesZohoInventoryBills.Services.Interfaces;
using FPVenturesZohoInventoryBills.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FPVenturesZohoInventoryBills
{
    public class FPVenturesZohoInventoryBillsFunction
    {
        readonly IZohoInventoryService _zohoInventoryService;
        readonly IZohoLeadsService _zohoLeadsService;
        const string AzureFunctionName = "FPVenturesZohoInventoryBillsFunction";

        public FPVenturesZohoInventoryBillsFunction( IZohoInventoryService zohoInventoryService, IZohoLeadsService zohoLeadsService)
        {
            _zohoInventoryService = zohoInventoryService;
            _zohoLeadsService = zohoLeadsService;
        }

        [Function(AzureFunctionName)]
        public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddHours(-10);
            var logger = context.GetLogger(AzureFunctionName);

            logger.LogInformation($"StartDate - {startDate}");
            logger.LogInformation($"EndDate - {endDate}");

            var inventoryItems = _zohoInventoryService.GetInventoryItems(startDate,endDate);

            var inventoryVendors = _zohoInventoryService.GetVendors();

            var crmVendors = _zohoLeadsService.GetVendors();

            logger.LogInformation($"{AzureFunctionName} Timer - {timerInfo.ScheduleStatus.Next}");
            logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

            logger.LogInformation("Mapping items to the Item Groups");
            var inventoryBills = ModelMapper.MapBillsModel(inventoryItems,crmVendors.Data,inventoryVendors.Contacts,startDate,endDate);
            
            //if (!inventoryItems.Any())
            //    logger.LogInformation("No new calls found");

            //logger.LogInformation("Adding items to inventory item groups");

            //var zohoInventoryResponseModel = _zohoInventoryService.AddLeadsToZohoInventory(inventoryItems);

            //if (zohoInventoryResponseModel == null)
            //    logger.LogError("Zoho request per minute limit exceed.....");
            //else
            //    LogResponseModels(logger, zohoInventoryResponseModel);

            //logger.LogInformation("Deleting the Demo items");
            //_zohoInventoryService.DeleteItem(addedGroups);
            //logger.LogInformation("Finished.....");
        }

        //private static void LogResponseModels(ILogger logger, List<ZohoInventoryResponseModel> zohoInventoryResponseModels)
        //{
        //    foreach (var zohoInventoryResponseModel in zohoInventoryResponseModels)
        //    {
        //        logger.LogInformation($"{nameof(zohoInventoryResponseModel.Message)} = {zohoInventoryResponseModel.Message}");

        //        if (zohoInventoryResponseModel.Item == null)
        //            continue;

        //        logger.LogInformation($"{nameof(zohoInventoryResponseModel.Item.SKU)} = {zohoInventoryResponseModel.Item.SKU}, " +
        //            $"{nameof(zohoInventoryResponseModel.Item.Name)} = {zohoInventoryResponseModel.Item.Name}, " +
        //            $"{nameof(zohoInventoryResponseModel.Item.ItemId)} = {zohoInventoryResponseModel.Item.ItemId}, " +
        //            $"{nameof(zohoInventoryResponseModel.Item.SKU)} = {zohoInventoryResponseModel.Item.SKU}, ");
        //    }
        //}
    }
}
