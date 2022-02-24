using Newtonsoft.Json;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
