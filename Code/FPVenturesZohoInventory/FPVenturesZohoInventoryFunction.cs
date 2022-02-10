using FPVenturesZohoInventory.Models;
using FPVenturesZohoInventory.Services.Interfaces;
using FPVenturesZohoInventory.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPVenturesZohoInventory
{
	public class FPVenturesZohoInventoryFunction
	{
		readonly IZohoInventoryService _zohoInventoryService;
		readonly IZohoLeadsService _zohoLeadsService;
		const string AzureFunctionName = "FPVenturesZohoInventoryFunction";

		public FPVenturesZohoInventoryFunction(IZohoLeadsService zohoLeadsService, IZohoInventoryService zohoInventoryService)
		{
			_zohoInventoryService = zohoInventoryService;
			_zohoLeadsService = zohoLeadsService;
		}

		[Function(AzureFunctionName)]
		public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			string datetime = GetDateString(DateTime.Now.AddHours(-4));
			var logger = context.GetLogger(AzureFunctionName);

			logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

			logger.LogInformation($"Fetching Leads from ZOHO ......");

			logger.LogInformation($"{datetime}");

			var zohoLeads = _zohoLeadsService.GetZohoLeads(datetime, logger);

			if (zohoLeads == null)
			{
				logger.LogInformation($"Zoho return NULL ..... Stopped");
			}

			if (!zohoLeads.Any())
			{
				logger.LogInformation($"No new Lead found");
			}

			var zohoInventoryItems = ModelMapper.MapZohoLeadsToZohoInventoryItems(zohoLeads);

			var zohoInventoryResponseModel = _zohoInventoryService.AddLeadsToZohoInventory(zohoInventoryItems);
			logger.LogInformation($"Leads fetched : {zohoLeads.Count}");

			logger.LogInformation($"Fetching duplicate leads from HAWX's ZOHO .....");


		}
		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
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
