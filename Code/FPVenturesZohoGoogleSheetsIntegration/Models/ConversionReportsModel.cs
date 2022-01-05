using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZohoGoogleSheetsIntegration.Models
{

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
	public class Result
	{
		public List<string> column_order { get; set; }
		public List<List<string>> rows { get; set; }
	}

	public class Response
	{
		public string uri { get; set; }
		public string action { get; set; }
		public Result result { get; set; }
	}

	public class ConversionReportsModel
	{
		public Response response { get; set; }
	}



}
