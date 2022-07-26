using FPVenturesFive9InventoryDisposition.Models;
using FPVenturesFive9InventoryDisposition.Services.Interfaces;
using FPVenturesFive9InventoryDisposition.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesFive9InventoryDisposition
{
    public class FPVenturesFive9InventoryDisposition
	{
		public readonly IZohoInventoryService _zohoInventoryService;
		public readonly IFive9Service _five9Service;
		public readonly IZohoService _zohoService;
		const string AzureFunctionName = "FPVenturesFive9InventoryDisposition";

		public FPVenturesFive9InventoryDisposition(IZohoInventoryService zohoInventoryService, IFive9Service five9Service, IZohoService zohoService)
		{
			_zohoInventoryService = zohoInventoryService;
			_five9Service = five9Service;
			_zohoService = zohoService;
		}

		[Function(AzureFunctionName)]
		public void RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			List<string> removeDispositions = new()
			{
				"Did not Sell",
				"No Answer",
				"Answering Machine",
				"Did Not Connect",
				"Did Not Answer",
				"Caller Disconnected"
			};
			var logger = context.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Timer - {timerInfo.ScheduleStatus.Next}");
			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			DateTime endDate = DateTime.Now;
			DateTime startDate = endDate.AddDays(-4);

			var five9Records = _five9Service.CallWebService(startDate, endDate);
			var five9FilteredRecords = five9Records.Where(x => !removeDispositions.Any(y => y == x.Disposition)).ToList();
			if (five9FilteredRecords == null || !five9FilteredRecords.Any())
			{
				logger.LogInformation($"No records to add.....");
				logger.LogInformation($"Finished.....");
				return;
			}

			logger.LogInformation($"Records Five9 = {five9FilteredRecords.Count}");

			var duplicateDispositionRecords = _zohoService.DuplicateZohoDispositions(five9FilteredRecords);

			if (duplicateDispositionRecords == null)
				return;

			logger.LogInformation($"Duplicate Call Disposition records from ZOHO = {duplicateDispositionRecords.Count}");

			var newDispositionsRecords = five9FilteredRecords.Where(x => !duplicateDispositionRecords.Any(y => y.CallID == x.CallID)).ToList();
			//var newDispositionsRecords = filteredDispositionsRecords.Where(x => x.Disposition != "No Answer" || x.Disposition != "Answering Machine" || x.Disposition != "Did Not Answer" || x.Disposition != "Did Not Connect" || x.Disposition != "Did not Sell").ToList();

			var inventoryItems = _zohoInventoryService.GetInventoryItems(startDate, endDate);
			if (inventoryItems == null)
				return;

			logger.LogInformation($"Zoho leads records = {inventoryItems.Count}");
			var dispositionRecordModels = ModelMapper.MapFive9ModelCallDispositionModel(inventoryItems, newDispositionsRecords);
			logger.LogInformation($"Disposition records to add = {dispositionRecordModels.Data.Count}");


			var (successModels, errorModels) = _zohoService.PostZohoDispositions(dispositionRecordModels);
			logger.LogInformation($"Disposition records added successfully = {successModels.Count}");
			logger.LogInformation($"Disposition records failed = {errorModels.Count}");

			LogErrorModels(logger, errorModels);
			logger.LogInformation($"Finished.....");

		}

		private static void LogErrorModels(ILogger logger, List<ZohoCallDispositionErrorModel> errorModels)
		{
			foreach (var errorModel in errorModels)
			{
				logger.LogWarning($"{nameof(errorModel.ApiName)} : {errorModel.ApiName}," +
						$"{nameof(errorModel.Message)} = {errorModel.Message}," +
						$"{nameof(errorModel.Status)} = {errorModel.Status}," +
						$"{nameof(errorModel.CustomerNumber)} = {errorModel.CustomerNumber} ," +
						$"{nameof(errorModel.LeadID)} = {errorModel.LeadID}," +
						$"{nameof(errorModel.CallID)} = {errorModel.CallID}," +
						$"{nameof(errorModel.Conferences)} = {errorModel.Conferences}," +
						$"{nameof(errorModel.ParkTime)} = {errorModel.ParkTime}," +
						$"{nameof(errorModel.AfterCallWorkTime)} = {errorModel.AfterCallWorkTime}," +
						$"{nameof(errorModel.HoldTime)} = {errorModel.HoldTime}," +
						$"{nameof(errorModel.Recordings)} = {errorModel.Recordings}," +
						$"{nameof(errorModel.TalkTime)} = {errorModel.TalkTime}," +
						$"{nameof(errorModel.Holds)} = {errorModel.Holds}," +
						$"{nameof(errorModel.RingTime)} = {errorModel.RingTime}," +
						$"{nameof(errorModel.Abandoned)} = {errorModel.Abandoned}," +
						$"{nameof(errorModel.QueueWaitTime)} = {errorModel.QueueWaitTime}," +
						$"{nameof(errorModel.Transfers)} = {errorModel.Transfers}," +
						$"{nameof(errorModel.BillTime)} = {errorModel.BillTime}," +
						$"{nameof(errorModel.IVRPath)} = {errorModel.IVRPath}," +
						$"{nameof(errorModel.CallTime)} = {errorModel.CallTime}," +
						$"{nameof(errorModel.IVRTime)} = {errorModel.IVRTime}," +
						$"{nameof(errorModel.ANI)} = {errorModel.ANI}," +
						$"{nameof(errorModel.CustomerName)} = {errorModel.CustomerName}," +
						$"{nameof(errorModel.AgentName)} = {errorModel.AgentName}," +
						$"{nameof(errorModel.DNIS)} = {errorModel.DNIS}," +
						$"{nameof(errorModel.Date)} = {errorModel.Date}," +
						$"{nameof(errorModel.Disposition)} = {errorModel.Disposition}," +
						$"{nameof(errorModel.Campaign)} = {errorModel.Campaign}," +
						$"{nameof(errorModel.Agent)} = {errorModel.Agent}," +
						$"{nameof(errorModel.Timestamp)} = {errorModel.Timestamp}," +
						$"{nameof(errorModel.Skill)} = {errorModel.Skill}");
			}
		}
	}
}
