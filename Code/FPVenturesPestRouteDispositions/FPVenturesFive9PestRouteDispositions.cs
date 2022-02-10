using FPVenturesFive9PestRouteDispositions.Models;
using FPVenturesFive9PestRouteDispositions.Services.Interfaces;
using FPVenturesFive9PestRouteDispositions.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesFive9PestRouteDispositions
{
	public class FPVenturesFive9PestRouteDispositions
	{
		public readonly IZohoService _zohoService;
		public readonly IFive9Service _five9Service;
		const string AzureFunctionName = "FPVenturesFive9PestRouteDispositions";

		public FPVenturesFive9PestRouteDispositions(IZohoService zohoService, IFive9Service five9Service)
		{
			_zohoService = zohoService;
			_five9Service = five9Service;
		}

		[Function(AzureFunctionName)]
		public void RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
		{

			var logger = context.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Timer - {timerInfo.ScheduleStatus.Next}");
			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			DateTime endDate = DateTime.Now;
			DateTime startDate = endDate.AddHours(-1);


			var five9Records = _five9Service.CallWebService(startDate, endDate);
			if (five9Records == null || !five9Records.Any())
			{
				logger.LogInformation($"No records to add.....");
				logger.LogInformation($"Finished.....");
				return;
			}

			logger.LogInformation($"Records Five9 = {five9Records.Count}");

			var duplicateDispositionRecords = _zohoService.DuplicateZohoDispositions(five9Records);

			if (duplicateDispositionRecords == null)
				return;

			logger.LogInformation($"Duplicate Call Disposition records from ZOHO = {duplicateDispositionRecords.Count}");

			var newDispositionsRecords = five9Records.Where(x => !duplicateDispositionRecords.Any(y => y.CallID == x.CallID)).ToList();

			var dispositionRecordModels = ModelMapper.MapFive9ModelCallDispositionModel(newDispositionsRecords);
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
