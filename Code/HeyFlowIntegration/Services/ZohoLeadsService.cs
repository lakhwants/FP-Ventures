using HeyFlowIntegration.Models;
using HeyFlowIntegration.Services.Interfaces;
using HeyFlowIntegration.Services.Mapper;
using HeyFlowIntegration.Shared;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace HeyFlowIntegration.Services
{
	public class ZohoLeadsService : IZohoLeadsService
	{
		public string ZohoAccessToken = string.Empty;
		public HeyFlowZohoConfigurationSettings _heyFlowZohoConfigurationSettings;

		public ZohoLeadsService(HeyFlowZohoConfigurationSettings heyFlowZohoConfigurationSettings)
		{
			_heyFlowZohoConfigurationSettings = heyFlowZohoConfigurationSettings;
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken(string refreshToken, string clientID, string clientSecret)
		{
			var client = new RestClient(string.Format(_heyFlowZohoConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath, refreshToken, clientID, clientSecret))
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
				return null;

			return response.Data.AccessToken;
		}

		public (List<Datum> successModels, List<ZohoErrorModel> errorModels) AddHeyFlowLeads(ZohoLeadsModel zohoLeadsModel, string refreshToken, string clientID, string clientSecret, string addTo)
		{
			IRestResponse<ZohoResponseModel> response = null;

			List<Datum> data = new();
			List<ZohoErrorModel> errorData = new();
			var client = new RestClient(_heyFlowZohoConfigurationSettings.ZohoLeadsBaseUrl + _heyFlowZohoConfigurationSettings.ZohoAddLeadsPath);
		
			var body = JsonConvert.SerializeObject(zohoLeadsModel);
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "text/plain");
			request.AddParameter("text/plain", body, ParameterType.RequestBody);

			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken(refreshToken, clientID, clientSecret);
			if (ZohoAccessToken == null)
			{
				return (null, null);
			}

			var duplicateModel = DuplicateZohoLeads(zohoLeadsModel, ZohoAccessToken);
			if (duplicateModel != null)
			{
				return (null, null);
			}

			request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
			response = client.Execute<ZohoResponseModel>(request);
			CreateErrorSuccessModels(errorData, zohoLeadsModel, response, addTo);
			data.AddRange(response.Data.Data.Where(x => x.Status == Enums.Status.success.ToString()));

			return (data, errorData);
		}

		/// <summary>
		/// Check for duplicate leads in ZOHO using the inbound ID
		/// </summary>
		/// <param name="zohoLeadsModel"></param>
		/// <returns></returns>
		private string DuplicateZohoLeads(ZohoLeadsModel zohoLeadsModel, string accessToken)
		{

			var zohoLead = zohoLeadsModel.Data.FirstOrDefault();
			string criteriaString = $"((Email:equals:{zohoLead.Email}) or (Phone:equals:{zohoLead.Phone}))";

			var client = new RestClient(_heyFlowZohoConfigurationSettings.ZohoLeadsBaseUrl + string.Format(_heyFlowZohoConfigurationSettings.ZohoCheckDuplicateLeadsPath, criteriaString));
			var request = new RestRequest(Method.GET);
			request.AddHeader("Authorization", "Zoho-oauthtoken " + accessToken);
			var response = client.Execute<ZohoLeadsModel>(request);

			if (response == null)
				return null;

			if (response.Data == null)
				return null;

			return response.Data.Data.FirstOrDefault().Email;
		}

		private static void CreateErrorSuccessModels(List<ZohoErrorModel> errorData, ZohoLeadsModel zohoLeadsModel, IRestResponse<ZohoResponseModel> response, string addedTo)
		{
			var indicis = response.Data.Data.Select((c, i) => new { c.Status, c.Details.ApiName, c.Message, Index = i })
												 .Where(x => x.Status == Enums.Status.error.ToString())
												 .Select(x => new { x.Index, x.ApiName, x.Status, x.Message });

			foreach (var index in indicis)
			{
				var errorlist = zohoLeadsModel.Data.ElementAt(index.Index);

				var zohoErrorModel = ModelMapper.MapLeadToZohoErrorModel(errorlist);
				zohoErrorModel.Message = index.Message.ToUpper();
				zohoErrorModel.Status = index.Status.ToUpper();
				zohoErrorModel.ApiName = index.ApiName;
				zohoErrorModel.AddedTo = addedTo;

				errorData.Add(zohoErrorModel);
			}
		}


	}
}
