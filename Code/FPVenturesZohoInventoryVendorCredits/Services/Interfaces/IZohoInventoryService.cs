using FPVenturesZohoInventoryVendorCredits.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        /// <summary>
        /// Get all vendors from ZOHO Inventory
        /// </summary>
        /// <param name="logger">Logger object</param>
        /// <returns>Returns ZOHO Inventory vendor response</returns>
        public ZohoInventoryVendorsResponseModel GetVendors(ILogger logger);
        
        /// <summary>
        /// Get all the items from inventory in given timespan
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="logger">Logger object</param>
        /// <returns>Returns list of inventory items</returns>
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate, ILogger logger);

        /// <summary>
        /// Post vendor credits model to ZOHO Inventory
        /// </summary>
        /// <param name="zohoInventoryPostVendorCreditModels">List of ZOHO Inventory vendor credit models</param>
        /// <param name="logger">Logger object</param>
        /// <returns>Returns list of ZOHO Inventory vendor credit response</returns>
        public List<ZohoInventoryVendorCreditResponseModel> PostVendorCredits(List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels,ILogger logger);

    }
}
