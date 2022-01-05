using FPVentures.Constants;
using FPVentures.Models;
using System;
using System.Collections.Generic;

namespace FPVentures.Services.Mapper
{
	public class ModelMapper
	{

		public static ZohoLeadsModel MapCallLogsToLeads(List<Record> callLogs)
		{
			ZohoLeadsModel zohoLeadsModel = new();

			List<Data> dataList = new();
			foreach (var callLog in callLogs)
			{
				DateTime defaultDateTime = new(1900, 1, 1, 0, 0, 0);
				Data data = new()
				{
					LeadSource = callLog.PublisherName,
					Company = callLog.TargetName,
					Mobile = RemoveCountryCode(callLog.Number),
					Phone = RemoveCountryCode(callLog.Number),
					LastName = string.IsNullOrEmpty(callLog.TargetName) ? (string.IsNullOrEmpty(callLog.TargetGroupName) ? callLog.CampaignName : callLog.TargetGroupName) : callLog.TargetName,
					CallDateTime = GetDateString(callLog.CallDt),
					TargetNumber = RemoveCountryCode(callLog.TargetNumber),
					CampaignID = callLog.CampaignId,
					PublisherID = callLog.PublisherId,
					PublisherName = callLog.PublisherName,
					TargetID = callLog.TargetId,
					InboundCallID = callLog.InboundCallId,
					ConnectedCall = callLog.HasConnected,
					NumberID = callLog.NumberId,
					CallCompleteTimestamp = callLog.HasConnected ? GetDateString(callLog.CallCompletedDt) : GetDateString(defaultDateTime),
					CallConnectedTimestamp = callLog.HasConnected ? GetDateString(callLog.CallConnectionDt) : GetDateString(defaultDateTime),
					IncompleteCall = callLog.IsIncomplete,
					HasRecording = callLog.HasRecording,
					NoConversionReason = callLog.NoConversionReason,
					IncompleteCallReason = callLog.IncompleteCallReason,
					PreviouslyConnected = callLog.HasPreviouslyConnected,
					TotalCost = Convert.ToString(callLog.TotalCost),
					TelcoCost = Convert.ToString(callLog.TelcoCost),
					ConnectedCallLength = Convert.ToString(TimeSpan.FromSeconds(callLog.ConnectedCallLengthInSeconds)),
					NumberPoolID = callLog.NumberPoolId,
					NumberPool = callLog.NumberPoolName,
					ICPCount = Convert.ToBoolean(callLog.DataEnrichmentCount),
					ICPCost = callLog.IcpCost,
					WinningBidTargetName = callLog.RingTreeWinningBidTargetName,
					WinningBid = callLog.RingTreeWinningBid,
					RingSuccesses = callLog.PingSuccessCount,
					ValidNumber = callLog.IBNIsPhoneNumberValid == "yes",
					HasPayout = callLog.HasPayout,
					TaggedNumber = RemoveCountryCode(callLog.TaggedNumber),
					TaggedState = callLog.TaggedState,
					TaggedLocationType = callLog.TaggedLocationType,
					TaggedNumberType = callLog.TaggedNumberType,
					TaggedPhoneProvider = callLog.TaggedTelco,
					TaggedCity = callLog.TaggedCity,
					SRC = callLog.TaggedSRC,
					TaggedTimeZone = callLog.TaggedTimeZone,
					IVRDepth = callLog.IvrDepth,
					CampaignName = callLog.CampaignName,
					IsUnbounceRecord = false   //By default true for unbounce record
				};
				data.WinningBidTargetId = callLog.RingTreeWinningBidTargetId;
				data.PublisherName = callLog.PublisherName;
				data.CallerID = RemoveCountryCode(callLog.InboundPhoneNumber);
				data.DialedNumber = RemoveCountryCode(callLog.Number);
				data.IsDuplicate = Convert.ToBoolean(callLog.IsDuplicate);
				data.EndCallSource = callLog.EndCallSource;
				data.TargetName = callLog.TargetName;
				data.Revenue = Convert.ToString(callLog.ConversionAmount);
				data.Payout = Convert.ToString(callLog.PayoutAmount);
				data.Duration = Convert.ToString(TimeSpan.FromSeconds(callLog.CallLengthInSeconds));
				data.Recording = callLog.RecordingUrl;

				dataList.Add(data);
			}
			zohoLeadsModel.Data = dataList;
			return zohoLeadsModel;
		}

		public static string RemoveCountryCode(string phone)
		{
			if (string.IsNullOrEmpty(phone))
				return null;

			foreach (var country in CountryCodes.PhoneCountryCodes)
			{
				phone = phone.Replace(country, "");
			}
			return phone;
		}

		public static ZohoErrorModel MapLeadToZohoErrorModel(Data errorlist)
		{
			DateTime defaultDateTime = new(1900, 1, 1, 0, 0, 0);

			ZohoErrorModel zohoErrorModel = new()
			{
				LastName = errorlist.LastName,
				LeadSource = errorlist.PublisherName,
				Company = errorlist.TargetName,
				Mobile = errorlist.Mobile,
				Phone = errorlist.Phone,
				CallDateTime = errorlist.CallDateTime,
				TargetNumber = errorlist.TargetNumber,
				CampaignID = errorlist.CampaignID,
				PublisherID = errorlist.PublisherID,
				PublisherName = errorlist.PublisherName,
				TargetID = errorlist.TargetID,
				InboundCallID = errorlist.InboundCallID,
				ConnectedCall = errorlist.ConnectedCall,
				NumberID = errorlist.NumberID,
				CallCompleteTimestamp = errorlist.ConnectedCall ? errorlist.CallCompleteTimestamp : defaultDateTime.ToString(),
				CallConnectedTimestamp = errorlist.ConnectedCall ? errorlist.CallConnectedTimestamp : defaultDateTime.ToString(),
				IncompleteCall = errorlist.IncompleteCall,
				HasRecording = errorlist.HasRecording,
				NoConversionReason = errorlist.NoConversionReason,
				IncompleteCallReason = errorlist.IncompleteCallReason,
				PreviouslyConnected = errorlist.PreviouslyConnected,
				TotalCost = Convert.ToString(errorlist.TotalCost),
				TelcoCost = Convert.ToString(errorlist.TelcoCost),
				ConnectedCallLength = errorlist.ConnectedCallLength,
				NumberPoolID = errorlist.NumberPoolID,
				NumberPool = errorlist.NumberPool,
				ICPCount = errorlist.ICPCount,
				ICPCost = errorlist.ICPCost,
				WinningBidTargetName = errorlist.WinningBidTargetName,
				WinningBidTargetId = errorlist.WinningBidTargetId,
				WinningBid = errorlist.WinningBid,
				RingSuccesses = errorlist.RingSuccesses,
				ValidNumber = errorlist.ValidNumber,
				TaggedNumber = errorlist.TaggedNumber,
				TaggedState = errorlist.TaggedState,
				TaggedLocationType = errorlist.TaggedLocationType,
				TaggedNumberType = errorlist.TaggedNumberType,
				TaggedPhoneProvider = errorlist.TaggedPhoneProvider,
				TaggedCity = errorlist.TaggedCity,
				SRC = errorlist.SRC,
				TaggedTimeZone = errorlist.TaggedTimeZone,
				IVRDepth = errorlist.IVRDepth,
				CampaignName = errorlist.CampaignName,
				CallerID = errorlist.CallerID,
				DialedNumber = errorlist.TaggedNumber,
				IsDuplicate = Convert.ToBoolean(errorlist.IsDuplicate),
				EndCallSource = errorlist.EndCallSource,
				TargetName = errorlist.TargetName,
				Revenue = Convert.ToString(errorlist.Revenue),
				Payout = Convert.ToString(errorlist.Payout),
				Duration = errorlist.Duration,
				Recording = errorlist.Recording
			};

			return zohoErrorModel;
		}

		static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}

	}
}
