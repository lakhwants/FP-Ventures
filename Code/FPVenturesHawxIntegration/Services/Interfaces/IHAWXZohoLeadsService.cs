using FPVenturesHAWXIntegration.Models;
using System.Collections.Generic;

namespace FPVenturesHAWXIntegration.Services.Interfaces
{
	public interface IHAWXZohoLeadsService
	{
	    List<Record> DuplicateZohoHAWXLeads(List<Data> zohoRecords);
		(List<Datum> successModels, List<ZohoErrorModel> errorModels) AddZohoLeadsToHawx(List<Record> hawxZohoLeadRecords);
	}
}
