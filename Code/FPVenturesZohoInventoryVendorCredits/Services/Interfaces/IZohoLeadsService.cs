using FPVenturesZohoInventoryVendorCredits.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Services.Interfaces
{
    public interface IZohoLeadsService
	{
		public ZohoCRMVendorsResponseModel GetVendors(ILogger logger);
		public List<DispositionModel> GetZohoDispositions(DateTime startDate, DateTime endDate, ILogger logger);
	}
}
