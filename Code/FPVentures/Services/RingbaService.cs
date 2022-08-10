using FPVentures.Constants;
using FPVentures.Models;
using FPVentures.Services.Interfaces;
using FPVentures.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using static FPVentures.Shared.Enums;

namespace FPVentures.Services
{
    public class RingbaService : IRingbaService
	{

		public RingbaZohoConfigurationSettings _ringbaZohoConfigurationSettings;

		public RingbaService(RingbaZohoConfigurationSettings ringbaZohoConfigurationSettings)
		{
			_ringbaZohoConfigurationSettings = ringbaZohoConfigurationSettings;
		}

		/// <summary>
		/// Get details of Call Logs
		/// </summary>
		/// <param name="callLogs"></param>
		/// <returns></returns>
		public List<Record> GetCallLogDetails(List<Record> callLogs)
		{

			List<CallLogDetail> callLogDetailsList = new();
			var client = new RestClient(_ringbaZohoConfigurationSettings.RingbaBaseUrl + string.Format(_ringbaZohoConfigurationSettings.RingbaCallLogDetailsPath, GetRingbaAccount()))
			{
				Timeout = -1
			};

			var batches = Utility.BuildBatches(callLogs.Select(x => x.InboundCallId).ToList(), 10);

			foreach (var batch in batches)
			{
				CallLogDetailsInboundCallIdsModel batchForApiCall = new CallLogDetailsInboundCallIdsModel
				{
					InboundCallIds = batch
				};

				var body = JsonConvert.SerializeObject(batchForApiCall);

				var request = new RestRequest(Method.POST);
				request.AddHeader("Authorization", _ringbaZohoConfigurationSettings.RingbaAccessToken);
				request.AddHeader("Content-Type", "application/json");
				request.AddParameter("application/json", body, ParameterType.RequestBody);
				var response = client.Execute<CallLogDetailsResponseModel>(request);

				if (response == null)
					return null;

				if (response.StatusCode != System.Net.HttpStatusCode.OK)
					return null;

				callLogDetailsList.AddRange(response.Data.Report.Records);
			}

			foreach (var record in callLogs)
			{
				var callLogRecords = callLogDetailsList.Where(x => x.InboundCallId == record.InboundCallId).ToList();
				foreach (var callRecord in callLogRecords)
				{
					record.IBNIsPhoneNumberValid = GetMessageTagValue(callRecord, RingbaCallLogDetails.IsPhoneNumberValid);
					record.TaggedNumber = GetMessageTagValue(callRecord, RingbaCallLogDetails.Number);
					record.TaggedState = GetMessageTagValue(callRecord, RingbaCallLogDetails.State);
					record.TaggedLocationType = GetMessageTagValue(callRecord, RingbaCallLogDetails.LocationType);
					record.TaggedNumberType = GetMessageTagValue(callRecord, RingbaCallLogDetails.NumberType);
					record.TaggedTelco = GetMessageTagValue(callRecord, RingbaCallLogDetails.Telco);
					record.TaggedCity = GetMessageTagValue(callRecord, RingbaCallLogDetails.City);
					record.TaggedTimeZone = GetMessageTagValue(callRecord, RingbaCallLogDetails.TimeZone);
					record.TaggedSRC = GetMessageTagValue(callRecord, RingbaCallLogDetails.SRC);
				}
			}

			return callLogs;
		}

		/// <summary>
		/// Get all the Call logs in a given time interval
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public List<Record> GetCallLogs(DateTime startDate, DateTime endDate)
		{
			List<Record> records = new();
			var defaultDateTime = new DateTime();
			var accountId = GetRingbaAccount();
			if (String.IsNullOrEmpty(accountId))
				return null;

			var client = new RestClient(_ringbaZohoConfigurationSettings.RingbaBaseUrl + string.Format(_ringbaZohoConfigurationSettings.RingbaCallLogsPath, accountId))
			{
				Timeout = -1
			};

			CallLogsColumnModel callLogsColumnModel = new()
			{
				ReportStart = startDate,
				ReportEnd = endDate
			};

			List<ValueColumn> valueColumns = new();
			List<OrderByColumn> orderByColumns = new();

			foreach (var column in RingbaColumns.Columns)
			{

				ValueColumn valueColumn = new()
				{
					Column = column
				};

				if (column == "callDt")
				{
					OrderByColumn orderByColumn = new()
					{
						Column = column,
						Direction = OrderBy.asc.ToString()
					};
					orderByColumns.Add(orderByColumn);

				}
				valueColumns.Add(valueColumn);
			}

			callLogsColumnModel.OrderByColumns = orderByColumns;
			callLogsColumnModel.ValueColumns = valueColumns;
			callLogsColumnModel.Size = 1000;

			do
			{
				var request = new RestRequest(Method.POST);
				request.AddHeader("Authorization", _ringbaZohoConfigurationSettings.RingbaAccessToken);
				request.AddHeader("Content-Type", "application/json");
				var body = JsonConvert.SerializeObject(callLogsColumnModel);
				request.AddParameter("application/json", body, ParameterType.RequestBody);
				var response = client.Execute<CallLogsResponseModel>(request);

				if (response == null || response.Data.Report == null)
					return null;

				if (!response.Data.Report.Records.Any())
					break;

				records.AddRange(response.Data.Report.Records);

				callLogsColumnModel.ReportStart = response.Data.Report.Records.LastOrDefault().CallDt;

				if (response.Data.Report.Records.Count < 1000)
					break;

			} while (callLogsColumnModel.ReportStart <= endDate);

			return records.Where(cl => cl.CallCompletedDt != defaultDateTime).GroupBy(p => p.InboundCallId).Select(x => x.FirstOrDefault()).ToList();
		}


		/// <summary>
		/// Get Ringba Account
		/// </summary>
		/// <returns></returns>
		public string GetRingbaAccount()
		{
			var client = new RestClient(_ringbaZohoConfigurationSettings.RingbaBaseUrl + _ringbaZohoConfigurationSettings.RingbaAccountsPath)
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.GET);
			request.AddHeader("Authorization", _ringbaZohoConfigurationSettings.RingbaAccessToken);
			var response = client.Execute<RingbaAccountModel>(request);

			if (response == null)
				return null;
			
			return response.Data.Account.FirstOrDefault().AccountId;
		}


		/// <summary>
		/// Get Tagged value from detailed Call Logs
		/// </summary>
		/// <param name="messageTag"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		private string GetMessageTagValue(CallLogDetail messageTag, string key)
		{
			return messageTag.MessageTags.FirstOrDefault(x => x.Name == key)?.Value;
		}
	}
}
