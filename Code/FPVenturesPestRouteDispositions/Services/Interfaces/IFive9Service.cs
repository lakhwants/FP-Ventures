using FPVenturesFive9PestRouteDispositions.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9PestRouteDispositions.Services.Interfaces
{
	public interface IFive9Service
	{
		List<Five9Model> CallWebService(DateTime startDate, DateTime endDate);
	}
}
