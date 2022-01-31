using ZohoGoogleSheetsIntegration.Models;

namespace FPVenturesZohoGoogleSheetsIntegration.Services.Interfaces
{
	public interface IZohoAnalyticsService
	{
		public ConversionReportsModel GetReports(string dateTime,string reportName);
	}
}
