﻿namespace FPVenturesZohoInventory.Models
{
	public class ZohoCRMAndInventoryConfigurationSettings
	{
		public string ZohoLeadsBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		//public string ZohoAddLeadsPath { get; set; }
		//public string ZohoCheckDuplicateLeadsPath { get; set; }
		public string ZohoInventoryBaseUrl { get; set; }
		public string ZohoInventoryAddItemPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoInventoryRefreshToken { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string COQLQuery { get; set; }
		public string ZohoCOQLPath { get; set; }
		//public string SearchCriteria { get; set; }
		//public string SearchOperator { get; set; }

	}
}
