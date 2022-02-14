using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesHawx.Models
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

		[JsonProperty("First_Name")]
		public string FirstName { get; set; }

		[JsonProperty("Email")]
		public string Email { get; set; }

		[JsonProperty("State")]
		public string State { get; set; }

		//[JsonProperty("Lead_Source")]
		//public string LeadSource { get; set; }

		public string Company { get; set; }

		[JsonProperty("Mobile")]
		public string Mobile { get; set; }

		[JsonProperty("Phone")]
		public string Phone { get; set; }

		//[JsonProperty("Call_Date_Time")]
		//public string CallDateTime { get; set; }

		//[JsonProperty("Target_Number")]
		//public string TargetNumber { get; set; }

		//[JsonProperty("Campaign_ID")]
		//public string CampaignID { get; set; }

		//[JsonProperty("Publisher_ID")]
		//public string PublisherID { get; set; }

		//[JsonProperty("Target_ID")]
		//public string TargetID { get; set; }

		//[JsonProperty("Inbound_Call_ID")]
		//public string InboundCallID { get; set; }

		//[JsonProperty("Connected_Call")]
		//public bool ConnectedCall { get; set; }

		//[JsonProperty("Number_ID")]
		//public string NumberID { get; set; }

		//[JsonProperty("Call_Complete_Timestamp")]
		//public string CallCompleteTimestamp { get; set; }

		//[JsonProperty("Call_Connected_Timestamp")]
		//public string CallConnectedTimestamp { get; set; }

		//[JsonProperty("Incomplete_Call")]
		//public bool IncompleteCall { get; set; }

		//[JsonProperty("Has_Recording")]
		//public bool HasRecording { get; set; }

		//[JsonProperty("No_Conversion_Reason")]
		//public string NoConversionReason { get; set; }

		//[JsonProperty("Incomplete_Call_Reason")]
		//public string IncompleteCallReason { get; set; }

		//[JsonProperty("Offline_Conversion_Uploaded")]
		//public bool OfflineConversionUploaded { get; set; }

		//[JsonProperty("Previously_Connected")]
		//public bool PreviouslyConnected { get; set; }

		//[JsonProperty("Total_Cost")]
		//[MaxLength(16)]
		//public string TotalCost { get; set; }

		//[JsonProperty("Telco_Cost")]
		//[MaxLength(16)]
		//public string TelcoCost { get; set; }

		//[JsonProperty("Connected_Call_Length")]
		//public string ConnectedCallLength { get; set; }

		//[JsonProperty("Number_Pool_ID")]
		//public string NumberPoolID { get; set; }

		//[JsonProperty("Number_Pool")]
		//public string NumberPool { get; set; }

		//[JsonProperty("ICP_Count")]
		//public bool ICPCount { get; set; }

		//[JsonProperty("ICP_Cost")]
		//public string ICPCost { get; set; }

		//[JsonProperty("Winning_Bid_Target_Name")]
		//public string WinningBidTargetName { get; set; }

		//[JsonProperty("Winning_Bid_Target_ID")]
		//public string WinningBidTargetId { get; set; }

		//[JsonProperty("Winning_Bid")]
		//public string WinningBid { get; set; }

		//[JsonProperty("Ring_Successes")]
		//public int RingSuccesses { get; set; }

		//[JsonProperty("Valid_Number")]
		//public bool ValidNumber { get; set; }

		//[JsonProperty("Tagged_Number")]
		//public string TaggedNumber { get; set; }

		//[JsonProperty("Tagged_State")]
		//public string TaggedState { get; set; }

		//[JsonProperty("Tagged_Location_Type")]
		//public string TaggedLocationType { get; set; }

		//[JsonProperty("Tagged_Number_Type")]
		//public string TaggedNumberType { get; set; }

		//[JsonProperty("Tagged_Phone_Provider")]
		//public string TaggedPhoneProvider { get; set; }

		//[JsonProperty("Tagged_City")]
		//public string TaggedCity { get; set; }

		//[JsonProperty("Tagged_Time_Zone")]
		//public string TaggedTimeZone { get; set; }

		//[JsonProperty("SRC")]
		//public string SRC { get; set; }

		//[JsonProperty("IVR_Depth")]
		//public int IVRDepth { get; set; }

		//[JsonProperty("Campaign_Name")]
		//public string CampaignName { get; set; }

		//[JsonProperty("Publisher_Name")]
		//public string PublisherName { get; set; }

		//[JsonProperty("Caller_ID")]
		//public string CallerID { get; set; }

		//[JsonProperty("Dialed_Number")]
		//public string DialedNumber { get; set; }

		//[JsonProperty("Is_Duplicate")]
		//public bool IsDuplicate { get; set; }

		//[JsonProperty("End_Call_Source")]
		//public string EndCallSource { get; set; }

		//[JsonProperty("Target_Name")]
		//public string TargetName { get; set; }

		//[JsonProperty("Revenue")]
		//public string Revenue { get; set; }

		//[JsonProperty("Payout")]
		//public string Payout { get; set; }

		//[JsonProperty("Duration")]
		//public string Duration { get; set; }

		//[JsonProperty("Recording")]
		//public string Recording { get; set; }

		//[JsonProperty("IsUnbounceRecord")]
		//public bool IsUnbounceRecord { get; set; }

		//[JsonProperty("Has_Payout")]
		//public bool HasPayout { get; set; }

		[JsonProperty("Unbounce_Page_Name")]
		public bool UnbouncePageName { get; set; }
	}

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
	//public class Owner
	//{
	//    public string name { get; set; }
	//    public string id { get; set; }
	//    public string email { get; set; }
	//}

	//public class Approval
	//{
	//    public bool @delegate { get; set; }
	//    public bool approve { get; set; }
	//    public bool reject { get; set; }
	//    public bool resubmit { get; set; }
	//}

	//public class CreatedBy
	//{
	//    public string name { get; set; }
	//    public string id { get; set; }
	//    public string email { get; set; }
	//}

	//public class ReviewProcess
	//{
	//    public bool approve { get; set; }
	//    public bool reject { get; set; }
	//    public bool resubmit { get; set; }
	//}

	//public class ModifiedBy
	//{
	//    public string name { get; set; }
	//    public string id { get; set; }
	//    public string email { get; set; }
	//}

	//public class ConvertedDetail
	//{
	//}

	//public class Data
	//{
	//    public Owner Owner { get; set; }
	//    public object Number_Pool { get; set; }
	//    public object Dialed_Number { get; set; }
	//    public object Call_Connected_Timestamp { get; set; }
	//    public object IP_Address { get; set; }

	//    [JsonProperty("$state")]
	//    public string State { get; set; }

	//    [JsonProperty("$process_flow")]
	//    public bool ProcessFlow { get; set; }
	//    public object No_Conversion_Reason { get; set; }
	//    public string Currency { get; set; }
	//    public object Campaign_ID { get; set; }
	//    public object Street { get; set; }
	//    public object Tagged_Number_Type { get; set; }
	//    public string id { get; set; }

	//    [JsonProperty("$approval")]
	//    public Approval Approval { get; set; }
	//    public DateTime Created_Time { get; set; }
	//    public object No_of_Employees { get; set; }
	//    public bool IsUnbounceRecord { get; set; }
	//    public List<object> Variant { get; set; }
	//    public object Country { get; set; }
	//    public object Revenue { get; set; }
	//    public CreatedBy Created_By { get; set; }
	//    public object Annual_Revenue { get; set; }
	//    public object Description { get; set; }
	//    public object ICP_Cost { get; set; }
	//    public object Caller_ID { get; set; }
	//    public object Campaign_Name { get; set; }

	//    [JsonProperty("$review_process")]
	//    public ReviewProcess ReviewProcess { get; set; }
	//    public object Website { get; set; }
	//    public object Salutation { get; set; }
	//    public object Tagged_State { get; set; }
	//    public string Full_Name { get; set; }
	//    public object Lead_Status { get; set; }
	//    public object Record_Image { get; set; }
	//    public object Target_Number { get; set; }
	//    public bool Incomplete_Call { get; set; }
	//    public object Skype_ID { get; set; }
	//    public object Publisher_ID { get; set; }
	//    public object IVR_Depth { get; set; }
	//    public bool Email_Opt_Out { get; set; }
	//    public object Telco_Cost { get; set; }
	//    public object Designation { get; set; }
	//    public object Target_Name { get; set; }
	//    public object Call_Complete_Timestamp { get; set; }
	//    public bool Has_Payout { get; set; }
	//    public object Connected_Call_Length { get; set; }
	//    public string Mobile { get; set; }

	//    [JsonProperty("$orchestration")]
	//    public bool Orchestration { get; set; }
	//    public object Google_Click_ID { get; set; }
	//    public object Winning_Bid { get; set; }
	//    public object Lead_Source { get; set; }
	//    public List<object> Tag { get; set; }
	//    public object Number_Pool_ID { get; set; }
	//    public object Number_ID { get; set; }
	//    public object Recording { get; set; }
	//    public object Tagged_Number { get; set; }
	//    public bool Is_Duplicate { get; set; }
	//    public object Company { get; set; }
	//    public string Email { get; set; }
	//    public bool ICP_Count { get; set; }

	//    [JsonProperty("$currency_symbol")]
	//    public string CurrencySymbol { get; set; }
	//    public object Tagged_City { get; set; }
	//    public DateTime Last_Activity_Time { get; set; }
	//    public object Industry { get; set; }
	//    public object Unsubscribed_Mode { get; set; }

	//    [JsonProperty("$converted")]
	//    public bool Converted { get; set; }
	//    public int Exchange_Rate { get; set; }
	//    public object Tagged_Phone_Provider { get; set; }
	//    public object Page_Id { get; set; }
	//    public object Total_Cost { get; set; }
	//    public string Zip_Code { get; set; }
	//    public object Incomplete_Call_Reason { get; set; }
	//    public object Ring_Successes { get; set; }
	//    public bool Has_Recording { get; set; }

	//    [JsonProperty("$approved")]
	//    public bool Approved { get; set; }
	//    public object Pest_Issue_Pick { get; set; }
	//    public bool Valid_Number { get; set; }

	//    [JsonProperty("$editable")]
	//    public bool Editable { get; set; }
	//    public object Duration { get; set; }
	//    public object City { get; set; }
	//    public object Tagged_Location_Type { get; set; }
	//    public string Unbounce_Page_Name { get; set; }
	//    public object Winning_Bid_Target_ID { get; set; }
	//    public string Secondary_Email { get; set; }
	//    public object Target_ID { get; set; }
	//    public object SRC { get; set; }
	//    public object Microsoft_Click_ID { get; set; }
	//    public object Rating { get; set; }
	//    public object Twitter { get; set; }
	//    public object Inbound_Call_ID { get; set; }
	//    public DateTime? Leads_Date_Time { get; set; }
	//    public string First_Name { get; set; }
	//    public object Call_Date_Time { get; set; }
	//    public ModifiedBy Modified_By { get; set; }

	//    [JsonProperty("$review")]
	//    public object Review { get; set; }
	//    public object Payout { get; set; }
	//    public string Phone { get; set; }
	//    public bool Connected_Call { get; set; }
	//    public object Tagged_Time_Zone { get; set; }
	//    public string Pest_Issue_Type { get; set; }
	//    public DateTime Modified_Time { get; set; }
	//    public object End_Call_Source { get; set; }

	//    [JsonProperty("$converted_detail")]
	//    public ConvertedDetail ConvertedDetail { get; set; }
	//    public object Unsubscribed_Time { get; set; }
	//    public bool Offline_Conversion_Uploaded { get; set; }
	//    public object Publisher_Name { get; set; }
	//    public string Last_Name { get; set; }

	//    [JsonProperty("$in_merge")]
	//    public bool InMerge { get; set; }
	//    public object Keyword_Search { get; set; }
	//    public object Winning_Bid_Target_Name { get; set; }
	//    public bool Previously_Connected { get; set; }
	//    public string Fax { get; set; }

	//    [JsonProperty("$approval_state")]
	//    public string ApprovalState { get; set; }
	//}

	//public class Info
	//{
	//    public int per_page { get; set; }
	//    public int count { get; set; }
	//    public int page { get; set; }
	//    public bool more_records { get; set; }
	//}

	////public class Root
	////{
	////    public List<Datum> data { get; set; }
	////    public Info info { get; set; }
	////}


}
