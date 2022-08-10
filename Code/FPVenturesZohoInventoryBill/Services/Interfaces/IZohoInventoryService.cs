using FPVenturesZohoInventoryBill.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBill.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        /// <summary>
        /// Get items from the ZOHO Inventory in a particular time frame
        /// </summary>
        /// <param name="startDate"> Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>List of inventory items</returns>
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate, ILogger logger);

        /// <summary>
        /// Get all the vendors from ZOHO Inventory
        /// </summary>
        /// <returns>Vendor model with all the vendors</returns>
        public ZohoInventoryVendorsResponseModel GetVendors(ILogger logger);

        /// <summary>
        /// Post Bills to the ZOHO Inventory
        /// </summary>
        /// <param name="zohoInventoryBillsModels">List of Bills</param>
        /// <param name="logger">Logger object</param>
        /// <returns>List of responses of the ZOHO Inventory Bills</returns>
        public List<ZohoInventoryBillsResponseModel> PostBills(List<ZohoInventoryBillsModel> zohoInventoryBillsModels, ILogger logger);
    }
}
