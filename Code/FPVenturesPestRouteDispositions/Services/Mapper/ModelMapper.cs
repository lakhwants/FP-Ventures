using FPVenturesFive9PestRouteDispositions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesFive9PestRouteDispositions.Services.Mapper
{
	public class ModelMapper
	{

		public static ZohoCallDispositionErrorModel MapCallDispostionsToCallDispostionsErrorModel(CallDispositionRecordModel errorModel)
		{
			ZohoCallDispositionErrorModel callDispositionErrorModel = new ZohoCallDispositionErrorModel();
			callDispositionErrorModel.CallType = errorModel.CallType;
			callDispositionErrorModel.ListName = errorModel.ListName;
			callDispositionErrorModel.IsPestRouteDisposition = errorModel.IsPestRouteDisposition;
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

		public static CallDispositionModel MapFive9ModelCallDispositionModel(List<Data> zohoLeads, List<Five9Model> five9Models)
		{
			CallDispositionModel callDispositionModel = new();
			List<CallDispositionRecordModel> callDispositionRecordModels = new();
			foreach (var five9Model in five9Models)
			{
				//foreach (var zoholead in zohoLeads.Where(lead => lead.Phone == five9Model.DNIS))
				//{
				//if (!zohoLeads.Any(x => !x.IsDuplicate))
				//	contiīnue;

				//if (!zohoLeads.Any(x => x.CallerID == five9Model.ANI))
				//	continue;

				CallDispositionRecordModel callDispositionModelRecord = new();
				callDispositionModelRecord.CustomerNumber = five9Model.DNIS;
				callDispositionModelRecord.CallID = five9Model.CallID;
				callDispositionModelRecord.ANI = five9Model.ANI;
				callDispositionModelRecord.AgentName = five9Model.AgentName;
				callDispositionModelRecord.DNIS = five9Model.DNIS;
				callDispositionModelRecord.Disposition = five9Model.Disposition;
				callDispositionModelRecord.Campaign = five9Model.Campaign;
				callDispositionModelRecord.CallType = five9Model.CallType;
				callDispositionModelRecord.ListName = five9Model.ListName;
				callDispositionModelRecord.Timestamp = Convert.ToDateTime(five9Model.Timestamp.Trim('"'));
				callDispositionModelRecord.IsPestRouteDisposition = true;

				if (zohoLeads.Any(x => x.CallerID == five9Model.ANI))
				{
					callDispositionModelRecord.LeadRecordID = zohoLeads.Any(x => x.CallerID == five9Model.ANI && !x.IsDuplicate) ? zohoLeads.Where(x => x.CallerID == five9Model.ANI && !x.IsDuplicate).FirstOrDefault().Id : null;
					callDispositionModelRecord.LeadID = zohoLeads.Any(x => x.CallerID == five9Model.ANI && !x.IsDuplicate) ? zohoLeads.Where(x => x.CallerID == five9Model.ANI && !x.IsDuplicate).FirstOrDefault().Id : null;
					callDispositionModelRecord.RingbaOrUnbounce = "Ringba";
					callDispositionRecordModels.Add(callDispositionModelRecord);
				}
				else if (zohoLeads.Any(x => x.Phone == five9Model.DNIS))
				{
					callDispositionModelRecord.LeadRecordID = zohoLeads.Any(x => x.Phone == five9Model.DNIS && !x.IsDuplicate) ? zohoLeads.Where(x => x.Phone == five9Model.DNIS && !x.IsDuplicate).FirstOrDefault().Id : null;
					callDispositionModelRecord.LeadID = zohoLeads.Any(x => x.Phone == five9Model.DNIS && !x.IsDuplicate) ? zohoLeads.Where(x => x.Phone == five9Model.DNIS && !x.IsDuplicate).FirstOrDefault().Id : null;
					callDispositionModelRecord.RingbaOrUnbounce = "Web Lead";
					callDispositionRecordModels.Add(callDispositionModelRecord);
				}
			}

			callDispositionModel.Data = callDispositionRecordModels;
			return callDispositionModel;
		}
	}
}
