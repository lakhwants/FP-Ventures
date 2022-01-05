using FPVenturesPestRoutes.Models;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Services.Interfaces
{
	public interface IZohoService
	{
		public (List<Datum> successModels, List<ZohoPestRouteCustomerErrorModel> errorModels) AddPestRouteCustomerToZoho(ZohoPestRouteModel zohoPestRouteModel);
		public List<int> DuplicateZohoPestRoutes(List<int> pestRouteCustomerIds);
	}
}
