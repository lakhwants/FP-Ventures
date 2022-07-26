using FPVenturesZohoInventoryBill.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBill.Services.Interfaces
{
    public interface IZohoInventoryService
	{
		public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate);
		public ZohoInventoryVendorsResponseModel GetVendors();
		public List<ZohoInventoryBillsResponseModel> PostBills(List<ZohoInventoryBillsModel> zohoInventorySalesOrderModels, ILogger logger);
	}
}
