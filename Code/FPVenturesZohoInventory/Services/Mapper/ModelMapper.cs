using FPVenturesZohoInventory.Constants;
using FPVenturesZohoInventory.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventory.Services.Mapper
{
	public class ModelMapper
	{
		public static List<ZohoInventoryModel> MapZohoLeadsToZohoInventoryItems(List<Data> zohoLeads)
		{
			List<ZohoInventoryModel> zohoInventoryRecords = new();
			foreach (var zohoLead in zohoLeads)
			{
				ZohoInventoryModel zohoInventoryModel = new ZohoInventoryModel();
				List<CustomField> customFields = new()
				{
					new CustomField { label = ZohoInventoryCustomFields.FirstName, value = zohoLead.FirstName },
					new CustomField { label = ZohoInventoryCustomFields.Vendor, value = zohoLead.PublisherName },
					new CustomField { label = ZohoInventoryCustomFields.LastName, value = zohoLead.LastName },
					new CustomField { label = ZohoInventoryCustomFields.Email, value = zohoLead.Email },
					new CustomField { label = ZohoInventoryCustomFields.CampaignName, value = zohoLead.CampaignName },
					new CustomField { label = ZohoInventoryCustomFields.PublisherName, value = zohoLead.PublisherName },
					new CustomField { label = ZohoInventoryCustomFields.InboundCallID, value = zohoLead.InboundCallID },
					new CustomField { label = ZohoInventoryCustomFields.Duration, value = zohoLead.Duration },
					new CustomField { label = ZohoInventoryCustomFields.TaggedState, value = zohoLead.TaggedCity },
					new CustomField { label = ZohoInventoryCustomFields.CallDateTime, value = zohoLead.CallDateTime },
					new CustomField { label = ZohoInventoryCustomFields.LeadsDateTime, value = zohoLead.LeadsDateTime }
				};
				if (zohoLead.IsUnbounceRecord)
				{
					zohoInventoryModel.name = zohoLead.Phone;
					CustomField customField = new()
					{
						label = ZohoInventoryCustomFields.Phone,
						value = zohoLead.Phone
					};

					customFields.Add(customField);
					customFields.Add(new CustomField { label = ZohoInventoryCustomFields.Payout, value = 30.ToString() });
					zohoInventoryModel.description = "Unbounce Lead";
				}
				else if (zohoLead.IsHeyFlowLead)
				{
					zohoInventoryModel.name = zohoLead.Phone;
					CustomField customField = new()
					{
						label = ZohoInventoryCustomFields.Phone,
						value = zohoLead.Phone
					};
					customFields.Add(new CustomField { label = ZohoInventoryCustomFields.Payout, value = 30.ToString() });
					customFields.Add(customField);
					zohoInventoryModel.description = "Heyflow Lead";
				}
				else if (zohoLead.IsContactFormLead)
				{
					zohoInventoryModel.name = zohoLead.Phone;
					CustomField customField = new()
					{
						label = ZohoInventoryCustomFields.Phone,
						value = zohoLead.Phone
					};
					customFields.Add(new CustomField { label = ZohoInventoryCustomFields.Payout, value = 30.ToString() });
					customFields.Add(customField);
					zohoInventoryModel.description = "Contact form Lead";
				}
				else
				{
					zohoInventoryModel.name = zohoLead.CallerID;
					CustomField customField = new()
					{
						label = ZohoInventoryCustomFields.CallerID,
						value = zohoLead.Phone
					};
					customFields.Add(new CustomField { label = ZohoInventoryCustomFields.Payout, value = !string.IsNullOrEmpty(zohoLead.Payout)? zohoLead.Payout :0.ToString() });
					zohoInventoryModel.description = "Ringba Lead";
				}
				zohoInventoryModel.product_type = "service";
				zohoInventoryModel.source = "user";
				zohoInventoryModel.status = "active";

				zohoInventoryModel.custom_fields = customFields;
				zohoInventoryRecords.Add(zohoInventoryModel);
			}

			return zohoInventoryRecords;
		}

		//public static ZohoErrorModel MapHawxLeadToHawxZohoErrorModel(Record errorlist)
		//{

		//	ZohoErrorModel zohoErrorModel = new()
		//	{
		//		LastName = errorlist.LastName,
		//		Company = errorlist.Company,
		//		Phone = errorlist.Phone,
		//		FirstName = errorlist.FirstName,
		//		Email = errorlist.Email
		//	};

		//	return zohoErrorModel;
		//}

		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
