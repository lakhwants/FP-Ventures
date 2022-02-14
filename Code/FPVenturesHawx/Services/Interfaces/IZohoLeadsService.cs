using FPVenturesHawx.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVenturesHawx.Services.Interfaces
{
	public interface IZohoLeadsService
	{
		public List<Data> GetZohoLeads(string dateTime, ILogger logger);
	}
}
