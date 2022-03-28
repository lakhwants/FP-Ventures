using Newtonsoft.Json;

namespace FPVenturesZohoInventoryPurchaseOrder.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
