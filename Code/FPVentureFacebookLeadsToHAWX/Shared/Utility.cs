using FPVentureFacebookLeadsToHAWX.Models;
using System.Collections.Generic;
using System.Linq;

namespace FPVentureFacebookLeadsToHAWX.Shared
{
	public static class Utility
	{
		public static List<List<T>> BuildBatches<T>(List<T> source,int size=10)
		{
			return source
				.Select((x, i) => new { Index = i, Value = x })
				.GroupBy(x => x.Index / size)
				.Select(x => x.Select(v => v.Value).ToList())
				.ToList();
		}
		public static ZohoLeadsModel UpdateZohoCRMLeads(ZohoLeadsModel records)
		{
			foreach(var record in records.Data)
			{
				record.IsFacebookLead = true;
			}
			return records;
		}

    }
}
