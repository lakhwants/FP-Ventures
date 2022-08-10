using FPVenturesZohoInventoryBill.Models;

namespace FPVenturesZohoInventoryBill.Services.Interfaces
{
    public interface IZohoLeadsService
	{
		/// <summary>
		/// Get all the vendors from the ZOHO CRM
		/// </summary>
		/// <returns>Vendor model with all the vendors</returns>
		public ZohoCRMVendorsResponseModel GetVendors();
	}
}
