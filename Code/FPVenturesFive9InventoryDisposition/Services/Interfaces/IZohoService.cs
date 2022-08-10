using FPVenturesFive9InventoryDisposition.Models;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Services.Interfaces
{
    public interface IZohoService
	{
		/// <summary>
		/// Gets all the duplicate dispositions from ZOHO CRM
		/// </summary>
		/// <param name="five9Models">List of Five9 disposition model</param>
		/// <returns>Returns list of ZOHO CRM disposition model</returns>
		List<CallDispositionRecordModel> DuplicateZohoDispositions(List<Five9Model> five9Models);

		/// <summary>
		/// Posts dispositions to ZOHO CRM
		/// </summary>
		/// <param name="callDispositionModel">ZOHO CRM disposition model</param>
		/// <returns>Returns tuple of successful and failed responses</returns>
		(List<Datum> successModels, List<ZohoCallDispositionErrorModel> errorModels) PostZohoDispositions(CallDispositionModel callDispositionModel);
	}
}
