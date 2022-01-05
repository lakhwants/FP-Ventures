using FPVenturesFive9Dispostion.Models;
using FPVenturesPestRoutes.Models;
using FPVenturesPestRoutes.Services.Interfaces;
using FPVenturesPestRoutes.Shared;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Services
{
	public class PestRouteService : IPestRouteService
	{
		private readonly PestRouteZohoConfigurationSettings _pestRouteZohoConfigurationSettings;

		public PestRouteService(PestRouteZohoConfigurationSettings pestRouteZohoConfigurationSettings)
		{
			_pestRouteZohoConfigurationSettings = pestRouteZohoConfigurationSettings;
		}

		/// <summary>
		/// Get all the Customer IDs
		/// </summary>
		/// <returns></returns>
		public PestRouteCustomerIDsModel GetPestRouteCustomerIDs()
		{
			var client = new RestClient(_pestRouteZohoConfigurationSettings.PestRouteBaseUrl + _pestRouteZohoConfigurationSettings.PestRouteSearchCustomerPath);
			var request = new RestRequest(Method.POST);
			request.AddHeader("Accept", "*/*");
			request.AddParameter("dateAdded", DateTime.Now.Date.AddDays(-10));
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddParameter("authenticationKey", _pestRouteZohoConfigurationSettings.PestRouteAuthenticationKey, ParameterType.QueryString);
			request.AddParameter("authenticationToken", _pestRouteZohoConfigurationSettings.PestRouteAuthenticationToken, ParameterType.QueryString);
			var response = client.Execute(request).Content;
			var pestRouteCustomerIDsModel = JsonConvert.DeserializeObject<PestRouteCustomerIDsModel>(response);
			return pestRouteCustomerIDsModel;
		}

		/// <summary>
		/// Get Details of the Customers using Ids
		/// </summary>
		/// <param name="pestRouteCustomerIDs">List of Customer IDs</param>
		/// <returns></returns>
		public List<Customer> GetPestRouteCustomerDetails(List<int> pestRouteCustomerIDs)
		{
			var batches = Utility.BuildBatches<int>(pestRouteCustomerIDs, 1000);

			List<Customer> customers = new();
			foreach (var batch in batches)
			{
				var client = new RestClient(_pestRouteZohoConfigurationSettings.PestRouteBaseUrl + _pestRouteZohoConfigurationSettings.PestRouteGetCutomerDetailsPath)
				{
					Timeout = -1
				};
				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				request.AddHeader("Accept", "*/*");
				request.AddParameter("authenticationKey", _pestRouteZohoConfigurationSettings.PestRouteAuthenticationKey);
				request.AddParameter("authenticationToken", _pestRouteZohoConfigurationSettings.PestRouteAuthenticationToken);
				request.AddParameter("customerIDs", JsonConvert.SerializeObject(batch));
				var response = client.Execute(request).Content;
				var pestRouteCustomerDetailModel = JsonConvert.DeserializeObject<PestRouteCustomerDetailModel>(response);
				customers.AddRange(pestRouteCustomerDetailModel.Customers);
			}

			return customers;
		}
	}
}
