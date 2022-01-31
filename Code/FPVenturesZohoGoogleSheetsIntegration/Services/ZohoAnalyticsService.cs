using FPVenturesZohoGoogleSheetsIntegration.Models;
using FPVenturesZohoGoogleSheetsIntegration.Services.Interfaces;
using RestSharp;
using System;
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
		public ConversionReportsModel GetReports(string dateTime, string reportName)
		{
			UriBuilder uBuild = new UriBuilder("https://analyticsapi.zoho.com/api/jeff@netlinkmarketing.com/fpventures_projects/");
			uBuild.Path = "api/jeff@netlinkmarketing.com/fpventures_projects/" +reportName;
			uBuild.Query = $"ZOHO_ACTION=EXPORT&ZOHO_OUTPUT_FORMAT=JSON&ZOHO_API_VERSION=1.0&ZOHO_ERROR_FORMAT=JSON&ZOHO_CRITERIA=\"Conversion Time\" >='{dateTime}'";

			var client = new RestClient(uBuild.Uri);
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
