using FPVenturesZohoInventoryPurchaseOrder.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryPurchaseOrder.Services.Interfaces
{
	public interface IZohoLeadsService
	{
		public List<Data> GetZohoLeads(string dateTime, ILogger logger);
	}
}
