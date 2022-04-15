using Newtonsoft.Json;

namespace FPVentureFacebookLeadsToHAWX.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
