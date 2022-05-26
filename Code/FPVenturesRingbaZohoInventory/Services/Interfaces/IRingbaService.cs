using FPVenturesRingbaZohoInventory.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventory.Services.Interfaces
{
    public interface IRingbaService
	{
		List<Record> GetCallLogDetails(List<Record> callLogs);
		List<Record> GetCallLogs(DateTime startDate, DateTime endDate);
		string GetRingbaAccount();
	}
}
