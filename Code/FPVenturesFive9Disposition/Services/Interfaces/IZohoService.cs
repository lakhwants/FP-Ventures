using FPVenturesFive9UnbounceDisposition.Models;
using System.Collections.Generic;

namespace FPVenturesFive9UnbounceDisposition.Services.Interfaces
{
	public interface IZohoService
	{
		List<Data> GetZohoLeads(List<Five9Model> phoneNumbers);
		List<CallDispositionRecordModel> DuplicateZohoDispositions(List<Five9Model> five9Models);
		(List<Datum> successModels, List<ZohoCallDispositionErrorModel> errorModels) PostZohoDispositions(CallDispositionModel callDispositionModel);
	}
}
