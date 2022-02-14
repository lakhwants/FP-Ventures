using Newtonsoft.Json;

namespace FPVenturesHAWXIntegration.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
