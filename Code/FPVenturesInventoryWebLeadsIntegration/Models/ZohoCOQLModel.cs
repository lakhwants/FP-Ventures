using Newtonsoft.Json;

namespace FPVenturesInventoryWebLeadsIntegration.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
