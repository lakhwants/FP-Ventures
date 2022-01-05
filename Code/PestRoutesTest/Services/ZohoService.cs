using Newtonsoft.Json;
using PestRoutesTest.Models;
using PestRoutesTest.Services.Mapper;
using PestRoutesTest.Shared;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PestRoutesTest.Services
{
	public class ZohoService
	{
		public string ZohoAccessToken = string.Empty;
		public string SeacrchCriteria = "(Customer_ID:equals:{0})";

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient("https://accounts.zoho.com/oauth/v2/token?refresh_token=1000.783dc14ce6e456c3eab0c53d486c0485.50ab0411d72f00488bd0a83323657031&client_id=1000.KGAQ5OHG3RREM70F468UA2MI7NXTCK&client_secret=16a37f8f68fb2bfe6a7eb4fa88177184a40c9adbb5&grant_type=refresh_token")
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
				return null;

			return response.Data.AccessToken;
		}

		/// <summary>
		/// Adds all the Leads to ZOHO
		/// </summary>
		/// <param name="zohoLeadsModel"></param>
		/// <returns></returns>
		public (List<Datum> successModels, List<ZohoErrorModel> errorModels) AddPestRouteCustomerToZoho(ZohoPestRouteModel zohoPestRouteModel)
		{
			List<Datum> data = new List<Datum>();
			List<ZohoErrorModel> errorData = new List<ZohoErrorModel>();
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			var batches = Utility.BuildBatches(zohoPestRouteModel.Data, 100);
			var client = new RestClient("https://www.zohoapis.com/crm/v2/PestRoutes")
			{
				Timeout = -1
			};

			foreach (var batch in batches)
			{
				ZohoPestRouteModel zohoPestRouteModelBatch = new ZohoPestRouteModel()
				{
					Data = batch
				};

				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "text/plain");
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);

				var body = JsonConvert.SerializeObject(zohoPestRouteModelBatch);
				request.AddParameter("text/plain", body, ParameterType.RequestBody);
				var response = client.Execute<ZohoResponseModel>(request);

				if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
					var requestRetry = new RestRequest(Method.POST);
					requestRetry.AddHeader("Content-Type", "text/plain");
					ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
					requestRetry.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
					requestRetry.AddParameter("text/plain", body, ParameterType.RequestBody);
					response = client.Execute<ZohoResponseModel>(request);
				}
				CreateErrorSuccessModels(errorData, batch, response);

				data.AddRange(response.Data.Data.Where(x => x.Status == Enums.Status.success.ToString()));
			}

			return (data, errorData);
		}

		/// <summary>
		/// Check for duplicate leads in ZOHO using the inbound ID
		/// </summary>
		/// <param name="ringbaRecords"></param>
		/// <returns></returns>
		public List<string> DuplicateZohoPestRoutes(List<string> pestRouteCustomerIds)
		{

			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
			List<string> customerIDs = new List<string>();
			ZohoPestRouteModel zohoPestRouteModelResponse = new ZohoPestRouteModel();
			string criteriaString = "";
			var batches = Utility.BuildBatches(pestRouteCustomerIds, 10);

			foreach (var batch in batches)
			{
				var lastID = batch.LastOrDefault();
				foreach (var IDs in batch)
				{
					criteriaString += string.Format(SeacrchCriteria, IDs);

					if (lastID != IDs)
						criteriaString += "or";
				}

				var client = new RestClient("https://www.zohoapis.com/crm/v2/" + string.Format("PestRoutes/search?criteria=({0})", criteriaString))
				{
					Timeout = -1
				};
				var request = new RestRequest(Method.GET);
				request.AddHeader("Authorization", "Zoho-oauthtoken " + ZohoAccessToken);
				var response = client.Execute<ZohoPestRouteModel>(request);

				criteriaString = string.Empty;

				if (response == null)
					return null;

				if (response.Data == null)
					continue;

				if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
					customerIDs.AddRange(response.Data.Data.Select(x => x.Customer_ID));
			}

			return customerIDs.Distinct().ToList();
		}

		private static void CreateErrorSuccessModels(List<ZohoErrorModel> errorData, List<Data> batch, IRestResponse<ZohoResponseModel> response)
		{
			var indicis = response.Data.Data.Select((c, i) => new { c.Status, c.Details.ApiName, c.Message, Index = i })
												 .Where(x => x.Status == Enums.Status.error.ToString())
												 .Select(x => new { x.Index, x.ApiName, x.Status, x.Message });

			foreach (var index in indicis)
			{
				var errorlist = batch.ElementAt(index.Index);

				var zohoErrorModel = ModelMapper.MapCustomersToZohoErrorModel(errorlist);
				zohoErrorModel.Message = index.Message.ToUpper();
				zohoErrorModel.Status = index.Status.ToUpper();
				zohoErrorModel.ApiName = index.ApiName;

				errorData.Add(zohoErrorModel);
			}
		}

	}
}
