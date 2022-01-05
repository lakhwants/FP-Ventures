using FPVentures.Models;
using System.Collections.Generic;

namespace FPVentures.Services.Interfaces
{
	public interface IZohoLeadsService
	{
		(List<Datum> successModels, List<ZohoErrorModel> errorModels) AddCallLogsToZoho(ZohoLeadsModel zohoLeadsModel);
		public List<string> DuplicateZohoLeads(List<Record> ringbaRecords);
	}
}
