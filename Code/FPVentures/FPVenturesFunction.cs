using FPVentures.Models;
using FPVentures.Services.Interfaces;
using FPVentures.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPVentures
{
	public class FPVenturesFunction
	{
		readonly IRingbaService _ringbaService;
		readonly IZohoLeadsService _zohoLeadsService;
		const string AzureFunctionName = "FPVenturesFunction";

		public FPVenturesFunction(IRingbaService ringbaService, IZohoLeadsService zohoLeadsService)
		{
			_ringbaService = ringbaService;
			_zohoLeadsService = zohoLeadsService;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			DateTime endDate = DateTime.Now;
			DateTime startDate = endDate.AddHours(-6);
			var logger = context.GetLogger(AzureFunctionName);

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

			logger.LogInformation($"Checking for duplicate call logs in Zoho ......");
			var inboundIds = _zohoLeadsService.DuplicateZohoLeads(callLogs);
			if (inboundIds == null)
			{
				logger.LogError($"Duplicate Inbound Ids are NULL ....... Stopped");
				return;
			}
			logger.LogInformation($"Duplicate call logs found = {inboundIds.Count}");


			var newCallLogs = callLogs.Where(x => !inboundIds.Any(y => y == x.InboundCallId)).ToList();
			logger.LogInformation($"New call logs to add in Zoho = {newCallLogs.Count}");

			logger.LogInformation($"Fetching details for call logs ......");
			newCallLogs = _ringbaService.GetCallLogDetails(newCallLogs);
			if (newCallLogs == null)
			{
				logger.LogError($"Call Log details are NULL ....... Stopped");
				return;
			}

			var zohoLeadsModel = ModelMapper.MapCallLogsToLeads(newCallLogs);

			logger.LogInformation($"Adding call logs to Zoho ......");
			var (successModels, errorModels) = _zohoLeadsService.AddCallLogsToZoho(zohoLeadsModel);
			logger.LogInformation($"Call logs added successfully = {successModels.Count}");
			logger.LogInformation($"Call logs failed to add = {errorModels.Count}");

			LogErrorModels(logger, errorModels);
			logger.LogInformation($"Finish.....");

		}

		private static void LogErrorModels(ILogger logger, List<ZohoErrorModel> errorModels)
		{
			foreach (var errorModel in errorModels)
			{
				logger.LogWarning($"{nameof(errorModel.ApiName)} : {errorModel.ApiName}," +
						$"{nameof(errorModel.Message)} = {errorModel.Message}," +
						$"{nameof(errorModel.Status)} = {errorModel.Status}," +
						$"{nameof(errorModel.PublisherName)} = {errorModel.PublisherName} ," +
						$"{nameof(errorModel.LastName)} = {errorModel.LastName}," +
						$"{nameof(errorModel.LeadSource)} = {errorModel.PublisherName}," +
						$"{nameof(errorModel.Company)} = {errorModel.TargetName}," +
						$"{nameof(errorModel.Mobile)} = {errorModel.Mobile}," +
						$"{nameof(errorModel.Phone)} = {errorModel.Phone}," +
						$"{nameof(errorModel.CallDateTime)} = {errorModel.CallDateTime}," +
						$"{nameof(errorModel.TargetNumber)} = {errorModel.TargetNumber}," +
						$"{nameof(errorModel.CampaignID)} = {errorModel.CampaignID}," +
						$"{nameof(errorModel.PublisherID)} = {errorModel.PublisherID}," +
						$"{nameof(errorModel.PublisherName)} = {errorModel.PublisherName}," +
						$"{nameof(errorModel.TargetID)} = {errorModel.TargetID}," +
						$"{nameof(errorModel.InboundCallID)} = {errorModel.InboundCallID}," +
						$"{nameof(errorModel.ConnectedCall)} = {errorModel.ConnectedCall}," +
						$"{nameof(errorModel.NumberID)} = {errorModel.NumberID}," +
						$"{nameof(errorModel.CallCompleteTimestamp)} = {errorModel.CallCompleteTimestamp}," +
						$"{nameof(errorModel.CallConnectedTimestamp)} = {errorModel.CallConnectedTimestamp}," +
						$"{nameof(errorModel.IncompleteCall)} = {errorModel.IncompleteCall}," +
						$"{nameof(errorModel.HasRecording)} = {errorModel.HasRecording}," +
						$"{nameof(errorModel.NoConversionReason)} = {errorModel.NoConversionReason}," +
						$"{nameof(errorModel.IncompleteCallReason)} = {errorModel.IncompleteCallReason}," +
						$"{nameof(errorModel.PreviouslyConnected)} = {errorModel.PreviouslyConnected}," +
						$"{nameof(errorModel.TotalCost)} = {errorModel.TotalCost}," +
						$"{nameof(errorModel.TelcoCost)} = {errorModel.TelcoCost}," +
						$"{nameof(errorModel.ConnectedCallLength)} = {errorModel.ConnectedCallLength}," +
						$"{nameof(errorModel.NumberPoolID)} = {errorModel.NumberPoolID}," +
						$"{nameof(errorModel.NumberPool)} = {errorModel.NumberPool}," +
						$"{nameof(errorModel.ICPCount)} = {errorModel.ICPCount}," +
						$"{nameof(errorModel.ICPCost)} = {errorModel.ICPCost}," +
						$"{nameof(errorModel.WinningBidTargetName)} = {errorModel.WinningBidTargetName}," +
						$"{nameof(errorModel.WinningBidTargetId)} = {errorModel.WinningBidTargetId}," +
						$"{nameof(errorModel.WinningBid)} = {errorModel.WinningBid}," +
						$"{nameof(errorModel.RingSuccesses)} = {errorModel.RingSuccesses}," +
						$"{nameof(errorModel.ValidNumber)} = {errorModel.ValidNumber}," +
						$"{nameof(errorModel.TaggedNumber)} = {errorModel.TaggedNumber}," +
						$"{nameof(errorModel.TaggedState)} = {errorModel.TaggedState}," +
						$"{nameof(errorModel.TaggedLocationType)} = {errorModel.TaggedLocationType}," +
						$"{nameof(errorModel.TaggedNumberType)} = {errorModel.TaggedNumberType}," +
						$"{nameof(errorModel.TaggedPhoneProvider)} = {errorModel.TaggedPhoneProvider}," +
						$"{nameof(errorModel.TaggedCity)} = {errorModel.TaggedCity}," +
						$"{nameof(errorModel.SRC)} = {errorModel.SRC}," +
						$"{nameof(errorModel.TaggedTimeZone)} = {errorModel.TaggedTimeZone}," +
						$"{nameof(errorModel.IVRDepth)} = {errorModel.IVRDepth}," +
						$"{nameof(errorModel.CampaignName)} = {errorModel.CampaignName}," +
						$"{nameof(errorModel.CallerID)} = {errorModel.CallerID}," +
						$"{nameof(errorModel.TaggedNumber)} = {errorModel.TaggedNumber}," +
						$"{nameof(errorModel.IsDuplicate)} = {errorModel.IsDuplicate}," +
						$"{nameof(errorModel.EndCallSource)} = {errorModel.EndCallSource}," +
						$"{nameof(errorModel.TargetName)} = {errorModel.TargetName}," +
						$"{nameof(errorModel.Revenue)} = {errorModel.Revenue}," +
						$"{nameof(errorModel.Payout)} = {errorModel.Payout}," +
						$"{nameof(errorModel.Duration)} = {errorModel.Duration}," +
						$"{nameof(errorModel.Recording)} = {errorModel.Recording}");
			}
		}
	}
}
