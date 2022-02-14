using FPVenturesHAWXIntegration.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesHAWXIntegration.Services.Mapper
{
	public class ModelMapper
	{
		public static List<Record> MapZohoLeadsToHawxLeads(List<Data> zohoLeads)
		{
			List<Record> hawxZohoRecords = new();
			foreach (var zohoLead in zohoLeads)
			{
				Record record = new Record();
				record.Company = zohoLead.Company;
				record.Email = zohoLead.Email;
				record.FirstName = zohoLead.FirstName;
				record.LastName = zohoLead.LastName;
				record.Phone = zohoLead.Phone;
				record.State = zohoLead.State;

				hawxZohoRecords.Add(record);
			}

			return hawxZohoRecords;
		}

		public static ZohoErrorModel MapHawxLeadToHawxZohoErrorModel(Record errorlist)
		{

			ZohoErrorModel zohoErrorModel = new()
			{
				LastName = errorlist.LastName,
				Company = errorlist.Company,
				Phone = errorlist.Phone,
				FirstName = errorlist.FirstName,
				Email=errorlist.Email
			};

			return zohoErrorModel;
		}

		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
