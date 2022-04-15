using FPVenturesZohoInventory.Models;
using FPVenturesZohoInventory.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace FPVenturesZohoInventory.Services
{
	public class ZohoLeadsService : IZohoLeadsService
	{
		public string ZohoAccessToken = string.Empty;
		public ZohoCRMAndInventoryConfigurationSettings _ZohoCRMAndInventoryConfigurationSettings;

		public ZohoLeadsService(ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfigurationSettings											)
		{
			_ZohoCRMAndInventoryConfigurationSettings = zohoCRMAndInventoryConfigurationSettings;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_ZohoCRMAndInventoryConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _ZohoCRMAndInventoryConfigurationSettings.ZohoRefreshToken, _ZohoCRMAndInventoryConfigurationSettings.ZohoClientId, _ZohoCRMAndInventoryConfigurationSettings.ZohoClientSecret));
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
		public List<Data> GetZohoLeads(string dateTime, ILogger logger)
		{

			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			if (ZohoAccessToken == null)
				return null;

			List<Data> zohoRecords = new();
			ZohoCOQLModel zohoCOQLModel = new();
			ZohoLeadsModel zohoLeadsModel;

			try
			{
				zohoCOQLModel.Query = string.Format(_ZohoCRMAndInventoryConfigurationSettings.COQLQuery, dateTime);
				do
				{
					using (var client = new HttpClient())
					{
						client.DefaultRequestHeaders.Add("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

						var json = JsonConvert.SerializeObject(zohoCOQLModel);
						var data = new StringContent(json, Encoding.UTF8, "application/json");
						client.BaseAddress = new Uri(_ZohoCRMAndInventoryConfigurationSettings.ZohoLeadsBaseUrl);
						var response = client.PostAsync(_ZohoCRMAndInventoryConfigurationSettings.ZohoCOQLPath, data).Result;

						var result = response.Content.ReadAsStringAsync().Result;

						if (string.IsNullOrEmpty(result))
							return new List<Data>();

						zohoLeadsModel = JsonConvert.DeserializeObject<ZohoLeadsModel>(result);

						zohoRecords.AddRange(zohoLeadsModel.Data);

						if (zohoLeadsModel.info.more_records)
						{
							zohoCOQLModel.Query = string.Format(_ZohoCRMAndInventoryConfigurationSettings.COQLQuery, !string.IsNullOrEmpty(zohoLeadsModel.Data.LastOrDefault().CallDateTime) ? zohoLeadsModel.Data.LastOrDefault().CallDateTime : zohoLeadsModel.Data.LastOrDefault().LeadsDateTime);
						}
						if (!response.IsSuccessStatusCode)
							return null;

					}
				} while (zohoLeadsModel.info.more_records);
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
