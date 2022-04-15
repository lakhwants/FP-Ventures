using FPVentureFacebookLeadsToHAWX.Models;
using FPVentureFacebookLeadsToHAWX.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FPVentureFacebookLeadsToHAWX.Services
{
	public class ZohoLeadsService : IZohoLeadsService
	{
		public string ZohoAccessToken = string.Empty;
		public ZohoHAWXConfigurationSettings _zohoHAWXConfigurationSettings;

		public ZohoLeadsService(ZohoHAWXConfigurationSettings zohoHAWXConfigurationSettings)
		{
			_zohoHAWXConfigurationSettings = zohoHAWXConfigurationSettings;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_zohoHAWXConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _zohoHAWXConfigurationSettings.ZohoRefreshToken, _zohoHAWXConfigurationSettings.ZohoClientId, _zohoHAWXConfigurationSettings.ZohoClientSecret))
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK || response.Data == null || response.Data.AccessToken == null)
				return null;

			return response.Data.AccessToken;
		}


		/// <summary>
		/// Get zoho leads 
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public List<Data> GetZohoLeads(ILogger logger)
		 {

			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			if (ZohoAccessToken == null)
				return null;

			List<Data> zohoRecords = new();
			ZohoCOQLModel zohoCOQLModel = new();
			zohoCOQLModel.Query = string.Format(_zohoHAWXConfigurationSettings.COQLQuery);

			try
			{
				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Add("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

					var json = JsonConvert.SerializeObject(zohoCOQLModel);
					var data = new StringContent(json, Encoding.UTF8, "application/json");
					client.BaseAddress = new Uri(_zohoHAWXConfigurationSettings.ZohoLeadsBaseUrl);
					var response = client.PostAsync(_zohoHAWXConfigurationSettings.ZohoCOQLPath, data).Result;

					var result = response.Content.ReadAsStringAsync().Result;

					var model = JsonConvert.DeserializeObject<ZohoLeadsModel>(result);

					zohoRecords.AddRange(model.Data);

					if (!response.IsSuccessStatusCode)
						return null;
				}
			}
			catch (Exception ex)
			{
				logger.LogWarning(ex.Message);
				logger.LogWarning(ex.InnerException.ToString());
			}

			return zohoRecords;

		}

	}
}
