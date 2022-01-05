using FPVenturesFive9UnbounceDisposition.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9UnbounceDisposition.Services.Interfaces
{
	public interface IFive9Service
	{
		List<Five9Model> CallWebService(DateTime startDate, DateTime endDate);
	}
}
