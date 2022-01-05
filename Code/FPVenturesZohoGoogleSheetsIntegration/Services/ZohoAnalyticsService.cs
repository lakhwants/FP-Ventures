using FPVenturesZohoGoogleSheetsIntegration.Models;
using FPVenturesZohoGoogleSheetsIntegration.Services.Interfaces;
using RestSharp;
using ZohoGoogleSheetsIntegration.Models;

namespace ZohoGoogleSheetsIntegration.Services
{
	public class ZohoAnalyticsService : IZohoAnalyticsService
	{
		public ZohoGoogleSheetsConfigurationSettings _zohoGoogleSheetsConfigurationSettings;
		public ZohoAnalyticsService(ZohoGoogleSheetsConfigurationSettings zohoGoogleSheetsConfigurationSettings)
		{
			_zohoGoogleSheetsConfigurationSettings = zohoGoogleSheetsConfigurationSettings;
		}

		/// <summary>
		/// Get reports from ZOHO analytics
		/// </summary>
		/// <returns> ConversionReportsModel </returns>
		public ConversionReportsModel GetReports(string dateTime)
		{
			var client = new RestClient($"https://analyticsapi.zoho.com/api/jeff@netlinkmarketing.com/fpventures_projects/Ringba Conversion Report?ZOHO_ACTION=EXPORT&ZOHO_OUTPUT_FORMAT=JSON&ZOHO_API_VERSION=1.0&ZOHO_ERROR_FORMAT=JSON&ZOHO_CRITERIA=\"Call Start Time\" >='{dateTime}'");
			var request = new RestRequest(Method.GET);
			request.AddHeader("Authorization", "Zoho-oauthtoken " + GetZohoAccessTokenFromRefreshToken());
			var response = client.Execute<ConversionReportsModel>(request);
			return response.Data;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_zohoGoogleSheetsConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _zohoGoogleSheetsConfigurationSettings.ZohoRefreshToken, _zohoGoogleSheetsConfigurationSettings.ZohoClientId, _zohoGoogleSheetsConfigurationSettings.ZohoClientSecret))
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
				return null;

			return response.Data.AccessToken;
		}

	}
}
