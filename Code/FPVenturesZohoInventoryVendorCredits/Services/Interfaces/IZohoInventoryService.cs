using FPVenturesZohoInventoryVendorCredits.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        public ZohoInventoryVendorsResponseModel GetVendors(ILogger logger);
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate, ILogger logger);
        public List<ZohoInventoryVendorCreditResponseModel> PostVendorCredits(List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels,ILogger logger);

    }
}
