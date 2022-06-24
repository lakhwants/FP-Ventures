using FPVenturesFive9InventoryDisposition.Models;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Services.Interfaces
{
    public interface IZohoService
	{
		List<CallDispositionRecordModel> DuplicateZohoDispositions(List<Five9Model> five9Models);
		(List<Datum> successModels, List<ZohoCallDispositionErrorModel> errorModels) PostZohoDispositions(CallDispositionModel callDispositionModel);
	}
}
