using FPVenturesFive9Disposition.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9Dispostion.Services.Interfaces
{
	public interface IFive9Service
	{
		List<Five9Model> CallWebService(DateTime startDate, DateTime endDate);
	}
}
