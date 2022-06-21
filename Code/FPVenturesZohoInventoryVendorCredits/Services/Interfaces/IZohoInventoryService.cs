using FPVenturesZohoInventoryVendorCredits.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        public ZohoInventoryVendorsResponseModel GetVendors();
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate);
        public void PostVendorCredits(List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels);

    }
}
