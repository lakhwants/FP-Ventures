using FPVenturesRingbaZohoInventory.Models;

namespace FPVenturesRingbaZohoInventory.Services.Interfaces
{
    public interface IZohoLeadsService
	{
        /// <summary>
        /// Gets all the vendors of ZOHO CRM
        /// </summary>
        /// <returns>Returns ZOHO CRM Vendor response</returns>
		public ZohoCRMVendorsResponseModel GetVendors();
	}
}
