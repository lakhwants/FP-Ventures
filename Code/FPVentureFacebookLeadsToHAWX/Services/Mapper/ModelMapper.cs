using FPVentureFacebookLeadsToHAWX.Models;
using System;
using System.Collections.Generic;

namespace FPVentureFacebookLeadsToHAWX.Services.Mapper
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
				record.First_Name = zohoLead.FirstName;
				record.Last_Name = zohoLead.LastName;
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
				LastName = errorlist.Last_Name,
				Company = errorlist.Company,
				Phone = errorlist.Phone,
				FirstName = errorlist.First_Name,
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
