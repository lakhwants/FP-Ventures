using FPVenturesPestRoutes.Models;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Services.Interfaces
{
	public interface IPestRouteService
	{
		public PestRouteCustomerIDsModel GetPestRouteCustomerIDs();
		public List<Customer> GetPestRouteCustomerDetails(List<int> pestRouteCustomerIDs);
	}
}
