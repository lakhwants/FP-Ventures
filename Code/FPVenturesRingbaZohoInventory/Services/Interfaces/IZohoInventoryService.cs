using FPVenturesRingbaZohoInventory.Models;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventory.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        public List<ZohoInventoryResponseModel> AddLeadsToZohoInventory(List<List<ZohoInventoryModel>> zohoInventoryModels);
        public ZohoInventoryItemGroupsListResponseModel GetItemGroupsList();
        public List<ZohoInventoryItemGroupReponseModel> CreateItemGroups(List<ZohoInventoryPostItemGroupRequestModel> newGroups);
        public void DeleteItem(List<ZohoInventoryItemGroupReponseModel> items);
        public ZohoInventoryVendorsResponseModel GetVendors();

    }
}
