using FPVenturesRingbaInventoryUpdateService.Models;

namespace FPVenturesRingbaInventoryUpdateService.Services.Interfaces
{
    public interface IZohoLeadsService
	{
		public ZohoCRMVendorsResponseModel GetVendors();
	}
}
