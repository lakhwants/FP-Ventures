using HeyFlowIntegration.Constants;
using HeyFlowIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeyFlowIntegration.Services.Mapper
{
	public class ModelMapper
	{
		public static ZohoLeadsModel MapHeyFlowToZohoLead(HeyFlowReponseModel heyFlowReponseModel)
		{
			ZohoLeadsModel zohoLeadsModel = new ZohoLeadsModel();
			Data data = new Data();

			data.ServiceType = (heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.ServiceType).FirstOrDefault().Values).FirstOrDefault().Label.ToString();
			data.PestIssueType = string.Join(",", (heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.PestIssueType).LastOrDefault().Values).Select(x => x.Label).ToList());
			data.FirstName = (heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.FirstName).FirstOrDefault().Values).FirstOrDefault().Label.ToString();
			data.LastName = (heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.LastName).FirstOrDefault().Values).FirstOrDefault().Label.ToString();
			data.Email= (heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.Email).FirstOrDefault().Values).FirstOrDefault().Label.ToString();
			data.Phone= RemoveCountryCode((heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.Phone).FirstOrDefault().Values).FirstOrDefault().Label.ToString());
			data.Mobile = RemoveCountryCode((heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.Mobile).FirstOrDefault().Values).FirstOrDefault().Label.ToString());
			data.ZipCode = (heyFlowReponseModel.Fields.Where(x => x.Label == HeyflowColumns.ZipCode).FirstOrDefault().Values).FirstOrDefault().Label.ToString();
			data.LeadsDateTime = GetDateString(heyFlowReponseModel.Created);
			data.IsHeyFlowLead = true;

			zohoLeadsModel.Data = new List<Data>
			{
				data
			};

			return zohoLeadsModel;
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

		public static ZohoErrorModel MapLeadToZohoErrorModel(Data errorlist)
		{
			ZohoErrorModel zohoErrorModel = new()
			{
				FirstName=errorlist.FirstName,
				LastName = errorlist.LastName,
				Phone = errorlist.Phone,
				ServiceType=errorlist.ServiceType,
				PestIssueType=errorlist.PestIssueType,
				Email=errorlist.Email,
				ZipCode=errorlist.ZipCode,
				LeadsDateTime=errorlist.LeadsDateTime
			};

			return zohoErrorModel;
		}

		static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
