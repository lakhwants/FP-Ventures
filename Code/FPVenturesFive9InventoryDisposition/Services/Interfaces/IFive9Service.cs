using FPVenturesFive9InventoryDisposition.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Services.Interfaces
{
    public interface IFive9Service
	{
		List<Five9Model> CallWebService(DateTime startDate, DateTime endDate);
	}
}
