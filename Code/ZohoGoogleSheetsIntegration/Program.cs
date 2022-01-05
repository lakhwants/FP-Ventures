using System;
using ZohoGoogleSheetsIntegration.Helper;
using ZohoGoogleSheetsIntegration.Models;
using ZohoGoogleSheetsIntegration.Services;

namespace ZohoGoogleSheetsIntegration
{
	class Program
	{
		static void Main(string[] args)
		{
			ZohoAnalyticsService zohoAnalyticsService = new ZohoAnalyticsService();

			var dateTime = DateTime.Now.Date;
			var z = zohoAnalyticsService.GetReports(dateTime.ToString("yyyy-MM-dd"));
			var s= ModelToCSV.ToCSV(z);

		}
	}
}
