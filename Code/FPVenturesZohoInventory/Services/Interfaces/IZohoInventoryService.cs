using FPVenturesZohoInventory.Models;
using System.Collections.Generic;

namespace FPVenturesZohoInventory.Services.Interfaces
{
	public interface IZohoInventoryService
	{
		public List<ZohoInventoryResponseModel> AddLeadsToZohoInventory(List<ZohoInventoryModel> zohoInventoryModels);
	}
}
