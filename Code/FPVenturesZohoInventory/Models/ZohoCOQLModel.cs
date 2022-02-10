using Newtonsoft.Json;

namespace FPVenturesZohoInventory.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
