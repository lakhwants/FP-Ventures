using DeleteUnAnsweredDispositionsFromZohoCRM.Models;
using DeleteUnAnsweredDispositionsFromZohoCRM.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DeleteUnAnsweredDispositionsFromZohoCRM.Services
{
	public class ZohoLeadsService
	{
		public string ZohoAccessToken = string.Empty;
		public ZohoCRMConfigurationSettings _zohoCRMConfigurationSettings;

		public ZohoLeadsService(ZohoCRMConfigurationSettings zohoCRMConfigurationSettings)
		{
			_zohoCRMConfigurationSettings = zohoCRMConfigurationSettings;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_zohoCRMConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, _zohoCRMConfigurationSettings.ZohoRefreshToken, _zohoCRMConfigurationSettings.ZohoClientId, _zohoCRMConfigurationSettings.ZohoClientSecret));
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
		public List<ZohoDispositions> GetZohoDispositions()
		{
			bool unauthorized =false;
			long dispositionId = 0;
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			if (ZohoAccessToken == null)
				return null;

			List<ZohoDispositions> zohoDispositions = new();
			ZohoCOQLModel zohoCOQLModel = new();
			ZohoDispositionResponseModel zohoDispositionResponseModel;

			try
			{
				zohoCOQLModel.Query = string.Format(_zohoCRMConfigurationSettings.COQLQuery, dispositionId);
				do
				{
					unauthorized = false;
					using (var client = new HttpClient())
					{
						client.DefaultRequestHeaders.Add("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

						var json = JsonConvert.SerializeObject(zohoCOQLModel);
						var data = new StringContent(json, Encoding.UTF8, "application/json");
						client.BaseAddress = new Uri(_zohoCRMConfigurationSettings.ZohoLeadsBaseUrl);
					 var response = client.PostAsync(_zohoCRMConfigurationSettings.ZohoCOQLPath, data).Result;

						//if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
						//{
						//	client.DefaultRequestHeaders.Add("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

						//	 json= JsonConvert.SerializeObject(zohoCOQLModel);
						//	 data = new StringContent(json, Encoding.UTF8, "application/json");
						//	client.BaseAddress = new Uri(_zohoCRMConfigurationSettings.ZohoLeadsBaseUrl);
						//	response = client.PostAsync(_zohoCRMConfigurationSettings.ZohoCOQLPath, data).Result;
						//}

						var result = response.Content.ReadAsStringAsync().Result;

						if (string.IsNullOrEmpty(result))
							return new List<ZohoDispositions>();

						zohoDispositionResponseModel = JsonConvert.DeserializeObject<ZohoDispositionResponseModel>(result);

						if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
						{
							ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
							unauthorized = true;
							continue;
						}

						zohoDispositions.AddRange(zohoDispositionResponseModel.Data);

						if (zohoDispositionResponseModel.info.more_records)
						{
							zohoCOQLModel.Query = string.Format(_zohoCRMConfigurationSettings.COQLQuery, zohoDispositionResponseModel.Data.Max(entry => entry.id));
						}
						if (!response.IsSuccessStatusCode)
							return null;

					}
				} while (unauthorized || zohoDispositionResponseModel.info.more_records);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Message {ex}");
			}

			return zohoDispositions;
		}

		public void DeleteFromCRM(List<ZohoDispositions> zohoDispositions)
		{
			List<Datum> data = new();
			List<ZohoErrorModel> errorData = new();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var batches = Utility.BuildBatches(zohoDispositions, 100);
			var client = new RestClient(_zohoCRMConfigurationSettings.ZohoLeadsBaseUrl + _zohoCRMConfigurationSettings.ZohoDispositionsPath);


			foreach (var batch in batches)
			{
				var ids = batch.Select(x => x.id).ToArray();

				var request = new RestRequest(Method.DELETE);
				request.AddHeader("Content-Type", "text/plain");
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

				request.AddParameter("ids", string.Join(',', ids));

				var response = client.Execute<ZohoResponseModel>(request);

				if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
				//	var clientRetry = new RestClient(_zohoCRMConfigurationSettings.ZohoLeadsBaseUrl + _zohoCRMConfigurationSettings.ZohoDispositionsPath);

					ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
					var requestRetry = new RestRequest(Method.DELETE);
					requestRetry.AddHeader("Content-Type", "text/plain");
					requestRetry.AddParameter("ids", string.Join(',', ids));

					requestRetry.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
					response = client.Execute<ZohoResponseModel>(requestRetry);
				}
			}

		}
	}


}

