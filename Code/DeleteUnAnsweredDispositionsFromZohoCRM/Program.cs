using DeleteUnAnsweredDispositionsFromZohoCRM.Models;
using DeleteUnAnsweredDispositionsFromZohoCRM.Services;
using System;

namespace DeleteUnAnsweredDispositionsFromZohoCRM
{
	public class Program
	{
		public static void Main()
		{
			ZohoCRMConfigurationSettings zohoCRMConfigurationSettings = GetConfigurationSettings();

			ZohoLeadsService zohoLeadsService = new ZohoLeadsService(zohoCRMConfigurationSettings);
			string datetime = GetDateString(DateTime.Now.AddHours(-48));
			var zohoLeads = zohoLeadsService.GetZohoDispositions();
			zohoLeadsService.DeleteFromCRM(zohoLeads);
		}

		private static ZohoCRMConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = "https://accounts.zoho.com/oauth/v2/token?refresh_token={0}&client_id={1}&client_secret={2}&grant_type=refresh_token",
				ZohoLeadsBaseUrl = "https://www.zohoapis.com/crm/v2/",
				ZohoClientId = "1000.KGAQ5OHG3RREM70F468UA2MI7NXTCK",
				ZohoClientSecret = "16a37f8f68fb2bfe6a7eb4fa88177184a40c9adbb5",
				ZohoRefreshToken = "1000.3022959d47d1a763b20418547dfc858d.0477c63799d714855b82eca55a0c9550",
				COQLQuery = "select id, Disposition from Dispositions where id > '{0}' and Disposition in ('No Answer','Answering Machine','Did Not Answer','Did Not Connect','Did not Sell')  Order by id, Disposition ASC",
				ZohoCOQLPath = "coql",
				ZohoInventoryBaseUrl = Environment.GetEnvironmentVariable("ZohoInventoryBaseUrl") ?? string.Empty,
				ZohoInventoryAddItemPath = Environment.GetEnvironmentVariable("ZohoInventoryAddItemPath") ?? string.Empty,
				ZohoInventoryRefreshToken = Environment.GetEnvironmentVariable("ZohoInventoryRefreshToken") ?? string.Empty,
				ZohoDispositionsPath = "Dispositions",
				ZohoLeadsPath = "Leads"
			};
		}
		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}