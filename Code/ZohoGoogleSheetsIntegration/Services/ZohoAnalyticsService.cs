using RestSharp;
using System;
using ZohoGoogleSheetsIntegration.Models;

namespace ZohoGoogleSheetsIntegration.Services
{
	public class ZohoAnalyticsService
	{
		public ConversionReportsModel GetReports(string dateTime)
		{
			var client = new RestClient($"https://analyticsapi.zoho.com/api/jeff@netlinkmarketing.com/fpventures_projects/Ringba Conversion Report?ZOHO_ACTION=EXPORT&ZOHO_OUTPUT_FORMAT=JSON&ZOHO_API_VERSION=1.0&ZOHO_ERROR_FORMAT=JSON&ZOHO_CRITERIA=\"Call Start Time\" >= {dateTime} ");
			client.Timeout = -1;
			var request = new RestRequest(Method.GET);
			request.AddHeader("Authorization", "Zoho-oauthtoken " + GetZohoAccessTokenFromRefreshToken());
			//request.AddHeader("Cookie", "5d7cd6c1d0=f4e96ce63c6a94369ba14a99acac3ca5; CSRF_TOKEN=b4e30a02-5337-4ac9-a279-369adb3710f4; JSESSIONID=DBF9B4C804625F370822DA467BAB6810; _zcsr_tmp=b4e30a02-5337-4ac9-a279-369adb3710f4");
			var response = client.Execute<ConversionReportsModel>(request);
			return response.Data;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient("https://accounts.zoho.com/oauth/v2/token?refresh_token=1000.699013e0b1ff0301ad72b10bcf05f2c2.e035a0a968b8389546bbadd95ab3a016&client_id=1000.KGAQ5OHG3RREM70F468UA2MI7NXTCK&client_secret=16a37f8f68fb2bfe6a7eb4fa88177184a40c9adbb5&grant_type=refresh_token")
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
