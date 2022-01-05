using System;
using System.Data;
using System.Linq;
using System.Text;
using ZohoGoogleSheetsIntegration.Models;

namespace ZohoGoogleSheetsIntegration.Helper
{
	public class ModelToCSV
	{
		public static string ToCSV(ConversionReportsModel conversionReportsModel)
		{
			var csv = new StringBuilder();

			csv.AppendLine("Parameters:TimeZone=-0500,");
			csv.AppendLine(String.Join(",", conversionReportsModel.response.result.column_order.Select(x => x.ToString()).ToArray()));

			foreach (var row in conversionReportsModel.response.result.rows)
			{
				csv.AppendLine(String.Join(",", row.Select(x => x.ToString()).ToArray()));
			}

			return csv.ToString();
		}
	}
}
