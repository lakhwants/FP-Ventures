using FPVenturesInventoryWebLeadsIntegration.Models;
using RestSharp;
using System.Collections.Generic;

namespace FPVenturesInventoryWebLeadsIntegration.Services.Interfaces
{
    public interface IHAWXZohoLeadsService
	{
		/// <summary>
		/// Posts lead to HAWX's ZOHO CRM
		/// </summary>
		/// <param name="hawxZohoLeadModel">HAWX's lead record</param>
		/// <returns>Returns HAWX's ZOHO CRM's response</returns>
		IRestResponse<ZohoResponseModel> AddZohoLeadsToHawx(HawxZohoLeadsModel hawxZohoLeadModel);
	}
}
