using FPVenturesZohoInventoryVendorCredits.Models;
using FPVenturesZohoInventoryVendorCredits.Services.Interfaces;
using FPVenturesZohoInventoryVendorCredits.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FPVenturesZohoInventoryVendorCredits
{
    public class FPVenturesZohoInventoryVendorCreditsFunction
	{
		readonly IZohoInventoryService _zohoInventoryService;
		readonly IZohoLeadsService _zohoLeadsService;
		public ZohoCRMAndInventoryConfigurationSettings _zohoCRMAndInventoryConfigurationSettings;
		const string AzureFunctionName = "FPVenturesZohoInventoryVendorCreditsFunction";

		public FPVenturesZohoInventoryVendorCreditsFunction(IZohoLeadsService zohoLeadsService, IZohoInventoryService zohoInventoryService, ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfiguration)
		{
			_zohoInventoryService = zohoInventoryService;
			_zohoLeadsService = zohoLeadsService;
			_zohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfiguration;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%VendorCreditsSchedule%")] TimerInfo timerInfo, FunctionContext context)
		{

			var logger = context.GetLogger(AzureFunctionName);

			DateTime lastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToLocalTime();

            DateTime endDate = lastMonth.AddDays(-1);
            DateTime startDate = lastMonth.AddMonths(-1);

			var dispositions = _zohoLeadsService.GetZohoDispositions(startDate,endDate,logger);
			var vendorsCRM = _zohoLeadsService.GetVendors();
			var vendorsInventory = _zohoInventoryService.GetVendors();
			var inventoryItems = _zohoInventoryService.GetInventoryItems(startDate,endDate);

			var vendorCreditModels = ModelMapper.MapVendorCreditsModel(inventoryItems,vendorsCRM.Data,vendorsInventory.Contacts,dispositions);

			_zohoInventoryService.PostVendorCredits(vendorCreditModels);
        }

		//private static void LogReponseModels(ILogger logger, ZohoInventorySalesOrderResponseModel zohoInventoryResponseModel)
		//{
		//	logger.LogWarning($"{nameof(zohoInventoryResponseModel.Message)} = {zohoInventoryResponseModel.Message}, " +
		//		$"{nameof(zohoInventoryResponseModel.SalesOrder.CustomerId)} = {zohoInventoryResponseModel.SalesOrder.CustomerId}, " +
		//		$"{nameof(zohoInventoryResponseModel.SalesOrder.CustomerName)} = {zohoInventoryResponseModel.SalesOrder.CustomerName} , " +
		//		$"{nameof(zohoInventoryResponseModel.SalesOrder.Date)} = {zohoInventoryResponseModel.SalesOrder.Date}, ");

		//	foreach (var item in zohoInventoryResponseModel.SalesOrder.LineItems)
		//	{
		//		logger.LogWarning($"{nameof(item.Name)} = {(item.Name)}, " +
		//	$"{nameof(item.Rate)} = {(item.Rate)}, " +
		//	$"{nameof(item.TaxId)} = {(item.TaxId)} ");
		//	}

		//}
	}
}
