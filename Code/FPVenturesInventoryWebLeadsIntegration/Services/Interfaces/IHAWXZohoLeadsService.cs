using FPVenturesInventoryWebLeadsIntegration.Models;
using RestSharp;
using System.Collections.Generic;

namespace FPVenturesInventoryWebLeadsIntegration.Services.Interfaces
{
    public interface IHAWXZohoLeadsService
	{
		IRestResponse<ZohoResponseModel> AddZohoLeadsToHawx(HawxZohoLeadsModel hawxZohoLeadModel);
	}
}
