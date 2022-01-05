using Newtonsoft.Json;
using PestRoutesTest.Models;
using PestRoutesTest.Shared;
using RestSharp;
using System.Collections.Generic;

namespace PestRoutesTest.Services
{
	public class PestRouteService
	{
		public PestRouteCustomerIDsModel GetPestRouteCustomerIDs()
		{
			var client = new RestClient("https://hawx.pestroutes.com/api/customer/search?authenticationKey=b719e28a12385b1d8ce46f3321b91654d3c95de596538d1c1117f68e0e1b1fe4&officeID=1&authenticationToken=d1589bb1e3a040ce8c2d6e2a5ec6790f559e39efe4ea5b2a026c11220103b060&active=1");
			var request = new RestRequest(Method.POST);
			request.AddHeader("Accept", "*/*");
			request.AddParameter("undefined", "{}", ParameterType.RequestBody);
			var response = client.Execute(request).Content;
			var pestRouteCustomerIDsModel = JsonConvert.DeserializeObject<PestRouteCustomerIDsModel>(response);
			return pestRouteCustomerIDsModel;
		}

		public List<Customer> GetPestRouteCustomerDetails(List<string> pestRouteCustomerIDs)
		{
			var batches = Utility.BuildBatches<string>(pestRouteCustomerIDs, 1000);

			List<Customer> customers = new List<Customer>();
			foreach (var batch in batches)
			{
				var client = new RestClient("https://hawx.pestroutes.com/api/customer/get?authenticationKey=b719e28a12385b1d8ce46f3321b91654d3c95de596538d1c1117f68e0e1b1fe4&authenticationToken=d1589bb1e3a040ce8c2d6e2a5ec6790f559e39efe4ea5b2a026c11220103b060");
				client.Timeout = -1;
				var request = new RestRequest(Method.POST);
				request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
				request.AddHeader("Accept", "*/*");
				request.AddHeader("Cookie", "PHPSESSID=bmep4v87mjplqmjef321rn7k46");
				request.AddParameter("customerIDs", JsonConvert.SerializeObject(batch));
				var response = client.Execute(request).Content;
				var pestRouteCustomerDetailModel = JsonConvert.DeserializeObject<PestRouteCustomerDetailModel>(response);
				customers.AddRange(pestRouteCustomerDetailModel.customers);
			}

			return customers;
		}
	}
}
