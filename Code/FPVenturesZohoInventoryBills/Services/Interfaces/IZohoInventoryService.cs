using FPVenturesZohoInventoryBills.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBills.Services.Interfaces
{
    public interface IZohoInventoryService
	{
		public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate);
		public ZohoInventoryVendorsResponseModel GetVendors();
	}
}
