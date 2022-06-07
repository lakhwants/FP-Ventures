using FPVenturesRingbaInventoryUpdateService.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesRingbaInventoryUpdateService.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        public List<ZohoInventoryResponseModel> PutLeadsToZohoInventory(List<ZohoInventoryModel> ZohoInventoryModels);
        public ZohoInventoryItemGroupsListResponseModel GetItemGroupsList();
        public List<ZohoInventoryItemGroupReponseModel> CreateItemGroups(List<ZohoInventoryPostItemGroupRequestModel> newGroups);
        public void DeleteItem(List<ZohoInventoryItemGroupReponseModel> items);
        public ZohoInventoryVendorsResponseModel GetVendors();
        public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate);

    }
}
