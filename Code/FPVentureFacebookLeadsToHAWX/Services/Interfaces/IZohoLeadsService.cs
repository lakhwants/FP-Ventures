using FPVentureFacebookLeadsToHAWX.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FPVentureFacebookLeadsToHAWX.Services.Interfaces
{
	public interface IZohoLeadsService
	{
		public List<Data> GetZohoLeads(ILogger logger);
	}
}
