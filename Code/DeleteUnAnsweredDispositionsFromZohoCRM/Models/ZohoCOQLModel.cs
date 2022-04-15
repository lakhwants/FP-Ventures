using Newtonsoft.Json;

namespace DeleteUnAnsweredDispositionsFromZohoCRM.Models
{
	public class ZohoCOQLModel
	{
		[JsonProperty("select_query")]
		public string Query { get; set; }
	}
}
