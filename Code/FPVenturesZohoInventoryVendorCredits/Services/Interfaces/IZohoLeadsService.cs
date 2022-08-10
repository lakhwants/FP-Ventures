using FPVenturesZohoInventoryVendorCredits.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Services.Interfaces
{
    public interface IZohoLeadsService
	{
        /// <summary>
        /// Gets all vendors from ZOHO CRM
        /// </summary>
        /// <param name="logger">Logger object</param>
        /// <returns>Returns ZOHO CRM vendor response</returns>
        public ZohoCRMVendorsResponseModel GetVendors(ILogger logger);

        /// <summary>
        /// Gets all the dispositions based on the query written in the local.settings.json in a given timeframe
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="logger">Logger object</param>
        /// <returns>Returns list of disposition models</returns>
		public List<DispositionModel> GetZohoDispositions(DateTime startDate, DateTime endDate, ILogger logger);
	}
}
