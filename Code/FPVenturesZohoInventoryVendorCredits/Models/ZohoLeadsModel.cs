using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FPVenturesZohoInventoryVendorCredits.Models
{
	public class ZohoLeadsModel
	{
		[JsonProperty("data")]
		public List<Data> Data { get; set; }
	}

	public class Data
	{
		[JsonProperty("Last_Name")]
		public string LastName { get; set; }

		[JsonProperty("Lead_Source")]
		public string LeadSource { get; set; }

		public string Company { get; set; }

		[JsonProperty("Mobile")]
		public string Mobile { get; set; }

		[JsonProperty("Phone")]
		public string Phone { get; set; }

		[JsonProperty("Call_Date_Time")]
		public string CallDateTime { get; set; }

		[JsonProperty("Target_Number")]
		public string TargetNumber { get; set; }

		[JsonProperty("Campaign_ID")]
		public string CampaignID { get; set; }

		[JsonProperty("Publisher_ID")]
		public string PublisherID { get; set; }

		[JsonProperty("Target_ID")]
		public string TargetID { get; set; }

		[JsonProperty("Inbound_Call_ID")]
		public string InboundCallID { get; set; }

		[JsonProperty("Connected_Call")]
		public bool ConnectedCall { get; set; }

		[JsonProperty("Number_ID")]
		public string NumberID { get; set; }

		[JsonProperty("Call_Complete_Timestamp")]
		public string CallCompleteTimestamp { get; set; }

		[JsonProperty("Call_Connected_Timestamp")]
		public string CallConnectedTimestamp { get; set; }

		[JsonProperty("Incomplete_Call")]
		public bool IncompleteCall { get; set; }

		[JsonProperty("Has_Recording")]
		public bool HasRecording { get; set; }

		[JsonProperty("No_Conversion_Reason")]
		public string NoConversionReason { get; set; }

		[JsonProperty("Incomplete_Call_Reason")]
		public string IncompleteCallReason { get; set; }

		[JsonProperty("Offline_Conversion_Uploaded")]
		public bool OfflineConversionUploaded { get; set; }

		[JsonProperty("Previously_Connected")]
		public bool PreviouslyConnected { get; set; }

		[JsonProperty("Total_Cost")]
		[MaxLength(16)]
		public string TotalCost { get; set; }

		[JsonProperty("Telco_Cost")]
		[MaxLength(16)]
		public string TelcoCost { get; set; }

		[JsonProperty("Connected_Call_Length")]
		public string ConnectedCallLength { get; set; }

		[JsonProperty("Number_Pool_ID")]
		public string NumberPoolID { get; set; }

		[JsonProperty("Number_Pool")]
		public string NumberPool { get; set; }

		[JsonProperty("ICP_Count")]
		public bool ICPCount { get; set; }

		[JsonProperty("ICP_Cost")]
		public string ICPCost { get; set; }

		[JsonProperty("Winning_Bid_Target_Name")]
		public string WinningBidTargetName { get; set; }

		[JsonProperty("Winning_Bid_Target_ID")]
		public string WinningBidTargetId { get; set; }

		[JsonProperty("Winning_Bid")]
		public string WinningBid { get; set; }

		[JsonProperty("Ring_Successes")]
		public int RingSuccesses { get; set; }

		[JsonProperty("Valid_Number")]
		public bool ValidNumber { get; set; }

		[JsonProperty("Tagged_Number")]
		public string TaggedNumber { get; set; }

		[JsonProperty("Tagged_State")]
		public string TaggedState { get; set; }

		[JsonProperty("Tagged_Location_Type")]
		public string TaggedLocationType { get; set; }

		[JsonProperty("Tagged_Number_Type")]
		public string TaggedNumberType { get; set; }

		[JsonProperty("Tagged_Phone_Provider")]
		public string TaggedPhoneProvider { get; set; }

		[JsonProperty("Tagged_City")]
		public string TaggedCity { get; set; }

		[JsonProperty("Tagged_Time_Zone")]
		public string TaggedTimeZone { get; set; }

		[JsonProperty("SRC")]
		public string SRC { get; set; }

		[JsonProperty("IVR_Depth")]
		public int IVRDepth { get; set; }

		[JsonProperty("Campaign_Name")]
		public string CampaignName { get; set; }

		[JsonProperty("Publisher_Name")]
		public string PublisherName { get; set; }

		[JsonProperty("Caller_ID")]
		public string CallerID { get; set; }

		[JsonProperty("Dialed_Number")]
		public string DialedNumber { get; set; }

		[JsonProperty("Is_Duplicate")]
		public bool IsDuplicate { get; set; }

		[JsonProperty("End_Call_Source")]
		public string EndCallSource { get; set; }

		[JsonProperty("Target_Name")]
		public string TargetName { get; set; }

		[JsonProperty("Revenue")]
		public string Revenue { get; set; }

		[JsonProperty("Payout")]
		public string Payout { get; set; }

		[JsonProperty("Duration")]
		public string Duration { get; set; }

		[JsonProperty("Recording")]
		public string Recording { get; set; }

		[JsonProperty("IsUnbounceRecord")]
		public bool IsUnbounceRecord { get; set; }

		[JsonProperty("Has_Payout")]
		public bool HasPayout { get; set; }
	}
}
