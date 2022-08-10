using FPVenturesFive9InventoryDisposition.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Services.Interfaces
{
    public interface IFive9Service
	{
		/// <summary>
		/// Gets Disposition records from Five9 in a given time frame
		/// </summary>
		/// <param name="startDate">Start date</param>
		/// <param name="endDate">End date</param>
		/// <returns>Returns list of Five9 Disposition records</returns>
		List<Five9Model> CallWebService(DateTime startDate, DateTime endDate);
	}
}
