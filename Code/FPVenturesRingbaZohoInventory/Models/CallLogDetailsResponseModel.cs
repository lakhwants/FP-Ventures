using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventory.Models
{
	public class Event
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ineligibleTargets")]
        public string IneligibleTargets { get; set; }

        [JsonProperty("eligibleOrderedTargets")]
        public string EligibleOrderedTargets { get; set; }

        [JsonProperty("dtStamp")]
        public DateTime DtStamp { get; set; }

        [JsonProperty("targetBuyerSubId")]
        public string TargetBuyerSubId { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("targetBuyer")]
        public string TargetBuyer { get; set; }

        [JsonProperty("targetBuyerId")]
        public string TargetBuyerId { get; set; }

        [JsonProperty("totalAmount")]
        public double? TotalAmount { get; set; }

        [JsonProperty("targetNumber")]
        public string TargetNumber { get; set; }

        [JsonProperty("targetSubId")]
        public string TargetSubId { get; set; }

        [JsonProperty("timeToConnect")]
        public int? TimeToConnect { get; set; }

        [JsonProperty("callConnectionDt")]
        public long? CallConnectionDt { get; set; }

        [JsonProperty("callLengthInSeconds")]
        public int? CallLengthInSeconds { get; set; }

        [JsonProperty("callCompletedDt")]
        public long? CallCompletedDt { get; set; }

        [JsonProperty("recordingUrl")]
        public string RecordingUrl { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("failReason")]
        public string FailReason { get; set; }

        [JsonProperty("lastCallDT")]
        public long? LastCallDT { get; set; }
    }

    public class MessageTag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("value-num")]
        public object ValueNum { get; set; }
    }

    public class CallLogDetail
    {
        [JsonProperty("campaignName")]
        public string CampaignName { get; set; }

        [JsonProperty("publisherName")]
        public string PublisherName { get; set; }

        [JsonProperty("targetName")]
        public string TargetName { get; set; }

        [JsonProperty("targetNumber")]
        public string TargetNumber { get; set; }

        [JsonProperty("campaignId")]
        public string CampaignId { get; set; }

        [JsonProperty("publisherId")]
        public string PublisherId { get; set; }

        [JsonProperty("publisherSubId")]
        public string PublisherSubId { get; set; }

        [JsonProperty("targetId")]
        public string TargetId { get; set; }

        [JsonProperty("inboundCallId")]
        public string InboundCallId { get; set; }

        [JsonProperty("callDt")]
        public DateTime CallDt { get; set; }

        [JsonProperty("inboundPhoneNumber")]
        public string InboundPhoneNumber { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("numberId")]
        public string NumberId { get; set; }

        [JsonProperty("isFromNumberPool")]
        public bool IsFromNumberPool { get; set; }

        [JsonProperty("numberPoolId")]
        public string NumberPoolId { get; set; }

        [JsonProperty("numberPoolName")]
        public string NumberPoolName { get; set; }

        [JsonProperty("timeToCallInSeconds")]
        public int TimeToCallInSeconds { get; set; }

        [JsonProperty("callCompletedDt")]
        public DateTime CallCompletedDt { get; set; }

        [JsonProperty("callConnectionDt")]
        public DateTime CallConnectionDt { get; set; }

        [JsonProperty("callLengthInSeconds")]
        public int CallLengthInSeconds { get; set; }

        [JsonProperty("connectedCallLengthInSeconds")]
        public double ConnectedCallLengthInSeconds { get; set; }

        [JsonProperty("endCallSource")]
        public string EndCallSource { get; set; }

        [JsonProperty("hasConnected")]
        public bool HasConnected { get; set; }

        [JsonProperty("hasPreviouslyConnected")]
        public bool HasPreviouslyConnected { get; set; }

        [JsonProperty("hasRecording")]
        public bool HasRecording { get; set; }

        [JsonProperty("isLive")]
        public bool IsLive { get; set; }

        [JsonProperty("profitNet")]
        public double ProfitNet { get; set; }

        [JsonProperty("totalCost")]
        public double TotalCost { get; set; }

        [JsonProperty("telcoCost")]
        public double TelcoCost { get; set; }

        [JsonProperty("recordingUrl")]
        public string RecordingUrl { get; set; }

        [JsonProperty("timeToConnectInSeconds")]
        public double TimeToConnectInSeconds { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        [JsonProperty("message-tags")]
        public List<MessageTag> MessageTags { get; set; }

        [JsonProperty("isDuplicate")]
        public bool? IsDuplicate { get; set; }
    }

    public class CallLogDetailsReport
    {
        [JsonProperty("records")]
        public List<CallLogDetail> Records { get; set; }
    }

    public class CallLogDetailsResponseModel
    {
        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("report")]
        public CallLogDetailsReport Report { get; set; }
    }


}
