using FPVenturesRingbaZohoInventoryService.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventoryService.Services.Interfaces
{
    public interface IRingbaService
	{
		List<Record> GetCallLogDetails(List<Record> callLogs);
		List<Record> GetCallLogs(DateTime startDate, DateTime endDate);
		string GetRingbaAccount();
	}
}
