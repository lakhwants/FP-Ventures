using System.Collections.Generic;

namespace FPVenturesRingbaInventoryUpdateService.Constants
{
	public static class RingbaColumns
	{
		public static List<string> Columns = new()
		{
			"targetGroupName",
			"callDt",
			"targetNumber",
			"campaignId",
			"publisherId",
			"targetId",
			"inboundCallId",
			"hasConnected",
			"noConversionReason",
			"hasRecording",
			"isIncomplete",
			"callConnectionDt",
			"callCompletedDt",
			"numberId",
			"connectedCallLengthInSeconds",
			"callLengthInSeconds",
			"telcoCost",
			"totalCost",
			"hasPreviouslyConnected",
			"offlineConversionUploaded",
			"incompleteCallReason",
			"numberPoolId",
			"numberPoolName",
			"publisherName",
			"campaignName",
			"ivrDepth",
			"pingSuccessCount",
			"ringTreeWinningBid",
			"ringTreeWinningBidTargetId",
			"ringTreeWinningBidTargetName",
			"icpCost",
			"dataEnrichmentCount",
			"recordingUrl",
			//	"buyerCallLengthInSeconds",
			"payoutAmount",
			"conversionAmount",
			"targetName",
			"endCallSource",
			"isDuplicate",
			"number",
			"inboundPhoneNumber",
			"hasPayout"
		};
	}
}
