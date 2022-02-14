using FPVenturesHawx.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesHawx.Services.Interfaces
{
	public interface IHAWXZohoLeadsService
	{
	    List<Record> DuplicateZohoHAWXLeads(List<Data> zohoRecords);
		(List<Datum> successModels, List<ZohoErrorModel> errorModels) AddZohoLeadsToHawx(List<Record> hawxZohoLeadRecords);
	}
}
