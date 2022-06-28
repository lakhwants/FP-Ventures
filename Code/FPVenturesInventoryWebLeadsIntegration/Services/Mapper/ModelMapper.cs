using FPVenturesInventoryWebLeadsIntegration.Constants;
using FPVenturesInventoryWebLeadsIntegration.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesInventoryWebLeadsIntegration.Services.Mapper
{
    public class ModelMapper
    {
        public static HawxZohoLeadsModel MapZohoLeadsToHawxLeads(ZohoInventoryRecordModel inventoryLead)
        {
            List<Record> hawxZohoRecords = new();
            Record record = new();
            record.Company = inventoryLead.Company;
            record.Email = inventoryLead.Email;
            record.LeadSource = inventoryLead.LeadSource;
            record.Phone = inventoryLead.Phone;
            record.State = inventoryLead.State;
            record.LastName = inventoryLead.Name;
            hawxZohoRecords.Add(record);

            HawxZohoLeadsModel hawxZohoLeadsModel = new();
            hawxZohoLeadsModel.Data = hawxZohoRecords;

            return hawxZohoLeadsModel;
        }

        public static string GetDateString(DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }

        public static string RemoveCountryCode(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return null;

            foreach (var country in CountryCodes.PhoneCountryCodes)
            {
                phone = phone.Replace(country, "");
            }
            return phone;
        }

    }
}
