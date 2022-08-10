using FPVenturesRingbaZohoInventory.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventory.Services.Interfaces
{
    public interface IZohoInventoryService
    {
        /// <summary>
        /// Post Ringba calls to ZOHO Inventory as item which are grouped into item groups
        /// </summary>
        /// <param name="zohoInventoryModels">List of items grouped in Item groups</param>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns list of ZOHO Inventory responses</returns>
        public List<ZohoInventoryResponseModel> AddLeadsToZohoInventory(List<List<ZohoInventoryModel>> zohoInventoryModels, ILogger logger);

        /// <summary>
        /// Gets all the item groups from ZOHO Inventory
        /// </summary>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns list of ZOHO Inventory item groups</returns>
        public ZohoInventoryItemGroupsListResponseModel GetItemGroupsList(ILogger logger);

        /// <summary>
        /// Posts item groups to ZOHO Inventory
        /// </summary>
        /// <param name="newGroups">List of new items groups</param>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns list of ZOHO Inventory items groups responses</returns>
        public List<ZohoInventoryItemGroupReponseModel> CreateItemGroups(List<ZohoInventoryPostItemGroupRequestModel> newGroups, ILogger logger);

        /// <summary>
        /// Deletes ZOHO Inventory items
        /// </summary>
        /// <param name="items">List of inventory items</param>
        /// <param name="logger">Logger Object</param>
        public void DeleteItem(List<ZohoInventoryItemGroupReponseModel> items, ILogger logger);

        /// <summary>
        /// Gets all the vendors of ZOHO Inventory
        /// </summary>
        /// <param name="logger">Logger Object</param>
        /// <returns>Returns ZOHO Inventory Vendor response</returns>
        public ZohoInventoryVendorsResponseModel GetVendors(ILogger logger);

    }
}
