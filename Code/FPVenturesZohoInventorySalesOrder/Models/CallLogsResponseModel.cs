using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class Record
    {
        [JsonProperty("targetGroupName")]
        public string TargetGroupName { get; set; }

		[JsonProperty("callDt")]
        public DateTime CallDt { get; set; }

        [JsonProperty("targetNumber")]
        public string TargetNumber { get; set; }

        [JsonProperty("campaignId")]
        public string CampaignId { get; set; }

        [JsonProperty("publisherId")]
        public string PublisherId { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("inboundCallId")]
        public string InboundCallId { get; set; }

        [JsonProperty("hasConnected")]
        public bool HasConnected { get; set; }

        [JsonProperty("isIncomplete")]
        public bool IsIncomplete { get; set; }

        [JsonProperty("numberId")]
        public string NumberId { get; set; }

        [JsonProperty("callCompletedDt")]
        public DateTime CallCompletedDt { get; set; }

        [JsonProperty("callConnectionDt")]
        public DateTime CallConnectionDt { get; set; }

        [JsonProperty("hasRecording")]
        public bool HasRecording { get; set; }

        [JsonProperty("hasPreviouslyConnected")]
        public bool HasPreviouslyConnected { get; set; }

        [JsonProperty("totalCost")]
        public float TotalCost { get; set; }

        [JsonProperty("telcoCost")]
        public float TelcoCost { get; set; }

        [JsonProperty("connectedCallLengthInSeconds")]
        public double ConnectedCallLengthInSeconds { get; set; }

        [JsonProperty("callLengthInSeconds")]
        public double CallLengthInSeconds { get; set; }

        [JsonProperty("numberPoolId")]
        public string NumberPoolId { get; set; }

        [JsonProperty("numberPoolName")]
        public string NumberPoolName { get; set; }

        [JsonProperty("dataEnrichmentCount")]
        public long DataEnrichmentCount { get; set; }

        [JsonProperty("icpCost")]
        public string IcpCost { get; set; }

        [JsonProperty("campaignName")]
        public string CampaignName { get; set; }

        [JsonProperty("publisherName")]
        public string PublisherName { get; set; }

        [JsonProperty("inboundPhoneNumber")]
        public string InboundPhoneNumber { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("endCallSource")]
        public string EndCallSource { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("recordingUrl")]
        public string RecordingUrl { get; set; }

        [JsonProperty("isDuplicate")]
        public bool? IsDuplicate { get; set; }

        [JsonProperty("noConversionReason")]
        public string NoConversionReason { get; set; }

        [JsonProperty("conversionAmount")]
        public double? ConversionAmount { get; set; }

        [JsonProperty("payoutAmount")]
        public double? PayoutAmount { get; set; }

        [JsonProperty("offlineConversionUploaded")]
        public bool OfflineConversionUploaded { get; set; }

        [JsonProperty("incompleteCallReason")]
        public string IncompleteCallReason { get; set; }

        [JsonProperty("ringTreeWinningBidTargetName")]
        public string RingTreeWinningBidTargetName { get; set; }

        [JsonProperty("ringTreeWinningBidTargetId")]
        public string RingTreeWinningBidTargetId { get; set; }

        [JsonProperty("ringTreeWinningBid")]
        public string RingTreeWinningBid { get; set; }

        [JsonProperty("pingSuccessCount")]
        public int PingSuccessCount { get; set; }

        [JsonProperty("ivrDepth")]
        public int IvrDepth { get; set; }

        [JsonProperty("hasPayout")]
		public bool HasPayout { get; set; }


		public string IBNIsPhoneNumberValid { get; set; }       
		public string TaggedNumber { get; set; }
		public string TaggedState { get; set; }
		public string TaggedLocationType { get; set; }
		public string TaggedNumberType { get; set; }
		public string TaggedTelco { get; set; }
        public string TaggedCity { get; set; }
        public string TaggedTimeZone { get; set; }
        public string TaggedSRC { get; set; }
    }

    public class Report
    {
        [JsonProperty("partialResult")]
        public bool PartialResult { get; set; }

        [JsonProperty("records")]
        public List<Record> Records { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }

    public class CallLogsResponseModel
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("report")]
        public Report Report { get; set; }
    }


}
