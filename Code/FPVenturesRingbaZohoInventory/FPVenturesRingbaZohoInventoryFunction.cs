using FPVenturesRingbaZohoInventory.Models;
using FPVenturesRingbaZohoInventory.Services.Interfaces;
using FPVenturesRingbaZohoInventory.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventory
{
    public class FPVenturesRingbaZohoInventoryFunction
    {
        readonly IZohoInventoryService _zohoInventoryService;
        readonly IRingbaService _ringbaService;
        readonly IZohoLeadsService _zohoLeadsService;
        const string AzureFunctionName = "FPVenturesRingbaZohoInventoryFunction";

        public FPVenturesRingbaZohoInventoryFunction(IRingbaService ringbaService, IZohoInventoryService zohoInventoryService, IZohoLeadsService zohoLeadsService)
        {
            _zohoInventoryService = zohoInventoryService;
            _ringbaService = ringbaService;
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

            logger.LogInformation($"{AzureFunctionName} Timer - {timerInfo.ScheduleStatus.Next}");
            logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

            List<Record> callLogs;
            logger.LogInformation($"Fetching call logs from Ringba ......");
            callLogs = _ringbaService.GetCallLogs(startDate, endDate);
            if (callLogs == null)
            {
                logger.LogError($"Call logs are NULL ....... Stopped");
                return;
            }
            logger.LogInformation($"Call logs fetched from Ringba = {callLogs.Count}");

            logger.LogInformation($"Fetching details for call logs ......");
            var detailedCallLogs = _ringbaService.GetCallLogDetails(callLogs);

            logger.LogInformation("Grouping calls by publishers");

            var callLogGroupsByPublishers = detailedCallLogs.GroupBy(x => x.PublisherName).ToList();

            logger.LogInformation("Fetching Item groups");

            var itemGroupList = _zohoInventoryService.GetItemGroupsList();

            logger.LogInformation($"Item groups - {itemGroupList.ItemGroups.Count}");

            var groupsToAdd = callLogGroupsByPublishers.Where(x => !itemGroupList.ItemGroups.Any(y => y.GroupName == x.Key)).ToList();

            logger.LogInformation($"Item groups to add- {groupsToAdd.Count}");

            logger.LogInformation("Mapping item groups");
            var newGroups = ModelMapper.MapNewItemGroups(groupsToAdd);

            logger.LogInformation("Posting Item groups");
            var addedGroups = _zohoInventoryService.CreateItemGroups(newGroups);

            logger.LogInformation("Fetching CRM vendors");
            var vendorsCRM = _zohoLeadsService.GetVendors();

            logger.LogInformation($"CRM Vendors - {vendorsCRM.Data.Count}");

            logger.LogInformation("Fetching Inventory vendors");
            var vendorInventory = _zohoInventoryService.GetVendors();

            logger.LogInformation($"Inventory Vendors - {vendorInventory.Contacts.Count}");

            logger.LogInformation("Mapping items to the Item Groups");
            var inventoryItems = ModelMapper.MapRingbaCallsToZohoInventoryItems(callLogGroupsByPublishers, _zohoInventoryService.GetItemGroupsList(), vendorInventory, vendorsCRM);

            
            if (!inventoryItems.Any())
            {
                logger.LogInformation("No new calls found");
            }

            logger.LogInformation("Adding items to inventory item groups");

            var zohoInventoryResponseModel = _zohoInventoryService.AddLeadsToZohoInventory(inventoryItems);

            if (zohoInventoryResponseModel == null)
            {
                logger.LogError("Zoho request per minute limit exceed.....");
            }
            else
            {
                LogResponseModels(logger, zohoInventoryResponseModel);
            }

            logger.LogInformation("Deleting the Demo items");
            _zohoInventoryService.DeleteItem(addedGroups);
            logger.LogInformation("Finished.....");
        }

        private static void LogResponseModels(ILogger logger, List<ZohoInventoryResponseModel> zohoInventoryResponseModels)
        {
            foreach (var zohoInventoryResponseModel in zohoInventoryResponseModels)
            {
                logger.LogInformation($"{nameof(zohoInventoryResponseModel.Message)} = {zohoInventoryResponseModel.Message}");

                if (zohoInventoryResponseModel.Item == null)
                    continue;

                logger.LogInformation($"{nameof(zohoInventoryResponseModel.Item.SKU)} = {zohoInventoryResponseModel.Item.SKU}, " +
                    $"{nameof(zohoInventoryResponseModel.Item.Name)} = {zohoInventoryResponseModel.Item.Name}, " +
                    $"{nameof(zohoInventoryResponseModel.Item.ItemId)} = {zohoInventoryResponseModel.Item.ItemId}, " +
                    $"{nameof(zohoInventoryResponseModel.Item.SKU)} = {zohoInventoryResponseModel.Item.SKU}, ");
            }
        }
    }
}
