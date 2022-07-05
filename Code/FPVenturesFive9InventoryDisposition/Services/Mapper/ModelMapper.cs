using FPVenturesFive9InventoryDisposition.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesFive9InventoryDisposition.Services.Mapper
{
    public class ModelMapper
	{

		public static ZohoCallDispositionErrorModel MapCallDispostionsToCallDispostionsErrorModel(CallDispositionRecordModel errorModel)
		{
			ZohoCallDispositionErrorModel callDispositionErrorModel = new ZohoCallDispositionErrorModel();
			callDispositionErrorModel.CustomerNumber = errorModel.ANI;
			callDispositionErrorModel.LeadID = errorModel.LeadID;
			callDispositionErrorModel.CallID = errorModel.CallID;
			callDispositionErrorModel.Conferences = errorModel.Conferences;
			callDispositionErrorModel.ParkTime = errorModel.ParkTime;
			callDispositionErrorModel.AfterCallWorkTime = errorModel.AfterCallWorkTime;
			callDispositionErrorModel.HoldTime = errorModel.HoldTime;
			callDispositionErrorModel.Recordings = errorModel.Recordings;
			callDispositionErrorModel.TalkTime = errorModel.TalkTime;
			callDispositionErrorModel.Holds = errorModel.Holds;
			callDispositionErrorModel.RingTime = errorModel.RingTime;
			callDispositionErrorModel.Abandoned = errorModel.Abandoned;
			callDispositionErrorModel.QueueWaitTime = errorModel.QueueWaitTime;
			callDispositionErrorModel.Transfers = errorModel.Transfers;
			callDispositionErrorModel.BillTime = errorModel.BillTime;
			callDispositionErrorModel.IVRPath = errorModel.IVRPath;
			callDispositionErrorModel.CallTime = errorModel.CallTime;
			callDispositionErrorModel.IVRTime = errorModel.IVRTime;
			callDispositionErrorModel.ANI = errorModel.ANI;
			callDispositionErrorModel.CustomerName = errorModel.CustomerName;
			callDispositionErrorModel.AgentName = errorModel.AgentName;
			callDispositionErrorModel.DNIS = errorModel.DNIS;
			callDispositionErrorModel.Date = errorModel.Date;
			callDispositionErrorModel.Disposition = errorModel.Disposition;
			callDispositionErrorModel.Campaign = errorModel.Campaign;
			callDispositionErrorModel.Agent = errorModel.Agent;
			callDispositionErrorModel.Timestamp = errorModel.Timestamp;
			callDispositionErrorModel.Skill = errorModel.Skill;

			return callDispositionErrorModel;
		}

		//public static CallDispositionModel MapFive9ModelCallDispositionModel(List<Data> zohoLeads, List<Five9Model> five9Models)
		//{
		//	CallDispositionModel callDispositionModel = new();
		//	List<CallDispositionRecordModel> callDispositionRecordModels = new();
		//	//	var s = JsonConvert.SerializeObject(zohoLeads);
		//	foreach (var five9Model in five9Models)
		//	{
		//			CallDispositionRecordModel callDispositionModelRecord = new();
		//			callDispositionModelRecord.CustomerNumber = five9Model.ANI;
		//			callDispositionModelRecord.CallID = five9Model.CallID;
		//			callDispositionModelRecord.Conferences = five9Model.Conferences;
		//			callDispositionModelRecord.ParkTime = five9Model.ParkTime;
		//			callDispositionModelRecord.AfterCallWorkTime = five9Model.AfterCallWorkTime;
		//			callDispositionModelRecord.HoldTime = five9Model.HoldTime;
		//			callDispositionModelRecord.Recordings = five9Model.Recordings;
		//			callDispositionModelRecord.TalkTime = five9Model.TalkTime;
		//			callDispositionModelRecord.Holds = five9Model.Holds;
		//			callDispositionModelRecord.RingTime = five9Model.RingTime;
		//			callDispositionModelRecord.Abandoned = five9Model.Abandoned;
		//			callDispositionModelRecord.QueueWaitTime = five9Model.QueueWaitTime;
		//			callDispositionModelRecord.Transfers = five9Model.Transfers;
		//			callDispositionModelRecord.BillTime = five9Model.BillTime;
		//			callDispositionModelRecord.IVRPath = five9Model.IVRPath;
		//			callDispositionModelRecord.CallTime = five9Model.CallTime;
		//			callDispositionModelRecord.IVRTime = five9Model.IVRTime;
		//			callDispositionModelRecord.ANI = five9Model.ANI;
		//			callDispositionModelRecord.CustomerName = five9Model.CustomerName;
		//			callDispositionModelRecord.AgentName = five9Model.AgentName;
		//			callDispositionModelRecord.DNIS = five9Model.DNIS;
		//			callDispositionModelRecord.Date = five9Model.Date;
		//			callDispositionModelRecord.Disposition = five9Model.Disposition;
		//			callDispositionModelRecord.Campaign = five9Model.Campaign;
		//			callDispositionModelRecord.Agent = five9Model.Agent;
		//			callDispositionModelRecord.Timestamp = Convert.ToDateTime(five9Model.Timestamp.Trim('"'));
		//			callDispositionModelRecord.Skill = five9Model.Skill;
					

		//		if (!zohoLeads.Any(x => !x.IsDuplicate))
		//			continue;

		//		if (zohoLeads.Any(x => x.CallerID == five9Model.ANI))
		//		{
		//			callDispositionModelRecord.LeadRecordID = zohoLeads.Any(x => x.CallerID == five9Model.ANI && !x.IsDuplicate) ? zohoLeads.Where(x => x.CallerID == five9Model.ANI && !x.IsDuplicate).FirstOrDefault().Id : null;
		//			callDispositionModelRecord.LeadID = zohoLeads.Any(x => x.CallerID == five9Model.ANI && !x.IsDuplicate) ? zohoLeads.Where(x => x.CallerID == five9Model.ANI && !x.IsDuplicate).FirstOrDefault().Id : null;
		//			callDispositionModelRecord.RingbaOrUnbounce = "Ringba";
		//			callDispositionRecordModels.Add(callDispositionModelRecord);
		//		}
		//		else if(zohoLeads.Any(x => x.Phone == five9Model.DNIS))
		//		{
		//			callDispositionModelRecord.LeadRecordID = zohoLeads.Any(x => x.Phone == five9Model.DNIS && !x.IsDuplicate) ? zohoLeads.Where(x => x.Phone == five9Model.DNIS && !x.IsDuplicate).FirstOrDefault().Id : null;
		//			callDispositionModelRecord.LeadID = zohoLeads.Any(x => x.Phone == five9Model.DNIS && !x.IsDuplicate) ? zohoLeads.Where(x => x.Phone == five9Model.DNIS && !x.IsDuplicate).FirstOrDefault().Id : null;
		//			callDispositionModelRecord.RingbaOrUnbounce = "Web Lead";
		//			callDispositionRecordModels.Add(callDispositionModelRecord);
		//		}

		//	}

		//	callDispositionModel.Data = callDispositionRecordModels;
		//	return callDispositionModel;
		//}

		public static CallDispositionModel MapFive9ModelCallDispositionModel(List<InventoryItem> inventoryItems, List<Five9Model> five9Models)
		{
			CallDispositionModel callDispositionModel = new();
			List<CallDispositionRecordModel> callDispositionRecordModels = new();

			foreach (var five9Model in five9Models)
			{
				CallDispositionRecordModel callDispositionModelRecord = new();
				callDispositionModelRecord.CustomerNumber = five9Model.ANI;
				callDispositionModelRecord.CallID = five9Model.CallID;
				callDispositionModelRecord.Conferences = five9Model.Conferences;
				callDispositionModelRecord.ParkTime = five9Model.ParkTime;
				callDispositionModelRecord.AfterCallWorkTime = five9Model.AfterCallWorkTime;
				callDispositionModelRecord.HoldTime = five9Model.HoldTime;
				callDispositionModelRecord.Recordings = five9Model.Recordings;
				callDispositionModelRecord.TalkTime = five9Model.TalkTime;
				callDispositionModelRecord.Holds = five9Model.Holds;
				callDispositionModelRecord.RingTime = five9Model.RingTime;
				callDispositionModelRecord.Abandoned = five9Model.Abandoned;
				callDispositionModelRecord.QueueWaitTime = five9Model.QueueWaitTime;
				callDispositionModelRecord.Transfers = five9Model.Transfers;
				callDispositionModelRecord.BillTime = five9Model.BillTime;
				callDispositionModelRecord.IVRPath = five9Model.IVRPath;
				callDispositionModelRecord.CallTime = five9Model.CallTime;
				callDispositionModelRecord.IVRTime = five9Model.IVRTime;
				callDispositionModelRecord.ANI = five9Model.ANI;
				callDispositionModelRecord.CustomerName = five9Model.CustomerName;
				callDispositionModelRecord.AgentName = five9Model.AgentName;
				callDispositionModelRecord.DNIS = five9Model.DNIS;
				callDispositionModelRecord.Date = five9Model.Date;
				callDispositionModelRecord.Disposition = five9Model.Disposition;
				callDispositionModelRecord.Campaign = five9Model.Campaign;
				callDispositionModelRecord.Agent = five9Model.Agent;
				callDispositionModelRecord.Timestamp = Convert.ToDateTime(five9Model.Timestamp.Trim('"'));
				callDispositionModelRecord.Skill = five9Model.Skill;


				if (inventoryItems.Any(x => x.CfCallerId == five9Model.ANI))
				{
					callDispositionModelRecord.ItemID = inventoryItems.Any(x => x.CfCallerId == five9Model.ANI) ? inventoryItems.Where(x => x.CfCallerId == five9Model.ANI).FirstOrDefault().ItemId : null;
					callDispositionModelRecord.RingbaOrUnbounce = "Ringba";
					callDispositionRecordModels.Add(callDispositionModelRecord);
				}
				else if (inventoryItems.Any(x => x.CfPhone== five9Model.DNIS))
				{
					callDispositionModelRecord.ItemID = inventoryItems.Any(x => x.CfPhone == five9Model.DNIS) ? inventoryItems.Where(x => x.CfPhone == five9Model.DNIS).FirstOrDefault().ItemId : null;
					callDispositionModelRecord.RingbaOrUnbounce = "Web Lead";
					callDispositionRecordModels.Add(callDispositionModelRecord);
				}

			}

			callDispositionModel.Data = callDispositionRecordModels;
			return callDispositionModel;
		}
	}
}
