using HeyFlowIntegration.Models;
using System.Collections.Generic;

namespace HeyFlowIntegration.Services.Interfaces
{
	public interface IZohoLeadsService
	{
		public (List<Datum> successModels, List<ZohoErrorModel> errorModels) AddHeyFlowLeads(ZohoLeadsModel zohoLeadsModel, string refreshToken, string clientID, string clientSecret, string addTo);
	}
}
