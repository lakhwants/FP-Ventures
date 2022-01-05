using FPVentures.Models;
using System;
using System.Collections.Generic;

namespace FPVentures.Services.Interfaces
{
	public interface IRingbaService
	{
		List<Record> GetCallLogDetails(List<Record> callLogs);
		List<Record> GetCallLogs(DateTime startDate, DateTime endDate);
		string GetRingbaAccount();
	}
}
