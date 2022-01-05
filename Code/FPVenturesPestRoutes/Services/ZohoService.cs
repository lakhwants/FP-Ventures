using FPVenturesFive9Dispostion.Models;
using FPVenturesPestRoutes.Models;
using FPVenturesPestRoutes.Services.Interfaces;
using FPVenturesPestRoutes.Services.Mapper;
using FPVenturesPestRoutes.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FPVenturesPestRoutes.Services
{
	public class ZohoService : IZohoService
	{
		private string ZohoAccessToken = string.Empty;
		private readonly string SeacrchCriteria = "(Customer_ID:equals:{0})";
		private readonly PestRouteZohoConfigurationSettings _pestRouteZohoConfigurationSettings;

		public ZohoService(PestRouteZohoConfigurationSettings pestRouteZohoConfigurationSettings)
		{
			_pestRouteZohoConfigurationSettings = pestRouteZohoConfigurationSettings;
			ZohoAccessToken = GetZohoAccessTokenFromRefreshToken();
		}

		/// <summary>
		/// Gets Access Token for Zoho
		/// </summary>
		/// <returns></returns>
		private string GetZohoAccessTokenFromRefreshToken()
		{
			var client = new RestClient(string.Format(_pestRouteZohoConfigurationSettings.ZohoAccessTokenFromRefreshTokenPath,
														_pestRouteZohoConfigurationSettings.ZohoRefreshToken,
														_pestRouteZohoConfigurationSettings.ZohoClientId,
														_pestRouteZohoConfigurationSettings.ZohoClientSecret))
			{
				Timeout = -1
			};
			var request = new RestRequest(Method.POST);
			var response = client.Execute<ZohoAccessTokenModel>(request);

			if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK )
				return null;

			if (string.IsNullOrEmpty(response.Data.AccessToken))
			{
				Thread.Sleep(70000);
				GetZohoAccessTokenFromRefreshToken();
			}

			return response.Data.AccessToken;
		}

		/// <summary>
		/// Adds all the PestRoute Customers to ZOHO
		/// </summary>
		/// <param name="zohoLeadsModel"></param>
		/// <returns></returns>
		public (List<Datum> successModels, List<ZohoPestRouteCustomerErrorModel> errorModels) AddPestRouteCustomerToZoho(ZohoPestRouteModel zohoPestRouteModel)
		{
			List<Datum> data = new();
			List<ZohoPestRouteCustomerErrorModel> errorData = new List<ZohoPestRouteCustomerErrorModel>();
			var batches = Utility.BuildBatches(zohoPestRouteModel.Data, 100);
			var client = new RestClient(_pestRouteZohoConfigurationSettings.ZohoBaseUrl + _pestRouteZohoConfigurationSettings.ZohoAddPestRouteCustomerPath)
			{
				Timeout = -1
			};

			foreach (var batch in batches)
			{
				ZohoPestRouteModel zohoPestRouteModelBatch = new()
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
		/// Check for duplicate PestRoute Customers in ZOHO using the Customer ID
		/// </summary>
		/// <param name="ringbaRecords"></param>
		/// <returns></returns>
		public List<int> DuplicateZohoPestRoutes(List<int> pestRouteCustomerIds)
		{

			List<int> customerIDs = new();
			ZohoPestRouteModel zohoPestRouteModelResponse = new();
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

				var client = new RestClient(_pestRouteZohoConfigurationSettings.ZohoBaseUrl + string.Format(_pestRouteZohoConfigurationSettings.ZohoCheckDuplicatePestRouteCustomerPath, criteriaString))
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
					customerIDs.AddRange(response.Data.Data.Select(x => Convert.ToInt32(x.CustomerID)));
			}

			return customerIDs.Distinct().ToList();
		}

		private static void CreateErrorSuccessModels(List<ZohoPestRouteCustomerErrorModel> errorData, List<Data> batch, IRestResponse<ZohoResponseModel> response)
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
