using FPVenturesRingbaZohoInventory.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventory.Services.Interfaces
{
    public interface IRingbaService
	{
		/// <summary>
		/// Fetches details for the call from Ringba
		/// </summary>
		/// <param name="callLogs">List of calls from Ringba</param>
		/// <returns>Returns list of calls from ringba with their details</returns>
		List<Record> GetCallLogDetails(List<Record> callLogs);

		/// <summary>
		/// Gets all the calls from Ringba in a given time frame
		/// </summary>
		/// <param name="startDate">Start date</param>
		/// <param name="endDate">End date</param>
		/// <returns>Returns list of calls from Ringba</returns>
		List<Record> GetCallLogs(DateTime startDate, DateTime endDate);

		/// <summary>
		/// Gets Ringba account
		/// </summary>
		/// <returns>Returns Ringba account string</returns>
		string GetRingbaAccount();
	}
}
