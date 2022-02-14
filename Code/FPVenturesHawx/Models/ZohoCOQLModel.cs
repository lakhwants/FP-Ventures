using Newtonsoft.Json;

namespace FPVenturesHawx.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
