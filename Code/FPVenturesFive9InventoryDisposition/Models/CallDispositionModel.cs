using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Models
{
    public class CallDispositionModel
    {
		[JsonProperty("data")]
		public List<CallDispositionRecordModel> Data { get; set; }
    }

    public class CallDispositionRecordModel
	{
		[JsonProperty("LeadRecord_ID")]
		public string LeadRecordID { get; set; }

		[JsonProperty("Ringba_or_Unbounce")]
		public string RingbaOrUnbounce { get; set; }
		
		public string ItemID { get; set; }

		[JsonProperty("Name")]
		public string CustomerNumber { get; set; }

		[JsonProperty("Lead_ID")]
		public string LeadID { get; set; }

		[JsonProperty("Call_ID")]
		public string CallID { get; set; }

		public string Conferences { get; set; }

		[JsonProperty("Park_Time")]
		public string ParkTime { get; set; }

		[JsonProperty("After_Call_WorkTime")]
		public string AfterCallWorkTime { get; set; }

		[JsonProperty("Hold_Time")]
		public string HoldTime { get; set; }

		public string Recordings { get; set; }

		[JsonProperty("Talk_Time")]
		public string TalkTime { get; set; }

		public string Holds { get; set; }

		[JsonProperty("Ring_Time")]
		public string RingTime { get; set; }

		public string Abandoned { get; set; }

		[JsonProperty("Queue_Wait_Time")]
		public string QueueWaitTime { get; set; }

		public string Transfers { get; set; }

		[JsonProperty("Bill_Time")]
		public string BillTime { get; set; }

		[JsonProperty("IVR_Path")]
		public string IVRPath { get; set; }

		[JsonProperty("Call_Time")]
		public string CallTime { get; set; }

		[JsonProperty("IVR_Time")]
		public string IVRTime { get; set; }

		public string ANI { get; set; }

		[JsonProperty("Customer_Name")]
		public string CustomerName { get; set; }

		[JsonProperty("Agent_Name")]
		public string AgentName { get; set; }

		public string DNIS { get; set; }

		public string Date { get; set; }

		public string Disposition { get; set; }

		public string Campaign { get; set; }

		public string Agent { get; set; }

		public DateTime Timestamp { get; set; }

		public string Skill { get; set; }

	}
}
