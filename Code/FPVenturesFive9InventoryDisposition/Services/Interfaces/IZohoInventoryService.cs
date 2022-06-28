using FPVenturesFive9InventoryDisposition.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate);

    }
}
