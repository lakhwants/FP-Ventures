using Newtonsoft.Json;

namespace FPVenturesZohoInventoryVendorCredits.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
