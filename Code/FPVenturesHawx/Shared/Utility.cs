using System.Collections.Generic;
using System.Linq;

namespace FPVenturesHawx.Shared
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

    }
}
