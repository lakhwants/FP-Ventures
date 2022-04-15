using FPVenturesZohoInventoryPurchaseOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryPurchaseOrder.Services.Mapper
{
	public class ModelMapper
	{
	public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
