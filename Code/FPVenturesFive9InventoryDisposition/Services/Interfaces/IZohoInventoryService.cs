using FPVenturesFive9InventoryDisposition.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        /// <summary>
        /// Gets ZOHO Inventory items in a given time frame
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Returns list of inventory items</returns>
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate);
    }
}
