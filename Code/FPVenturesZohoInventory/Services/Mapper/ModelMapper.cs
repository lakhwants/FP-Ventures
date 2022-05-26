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
					new CustomField { Label = ZohoInventoryCustomFields.FirstName, Value = zohoLead.FirstName },
					new CustomField { Label = ZohoInventoryCustomFields.Vendor, Value = zohoLead.PublisherName },
					new CustomField { Label = ZohoInventoryCustomFields.LastName, Value = zohoLead.LastName },
					new CustomField { Label = ZohoInventoryCustomFields.Email, Value = zohoLead.Email },
					new CustomField { Label = ZohoInventoryCustomFields.CampaignName, Value = zohoLead.CampaignName },
					new CustomField { Label = ZohoInventoryCustomFields.PublisherName, Value = zohoLead.PublisherName },
					new CustomField { Label = ZohoInventoryCustomFields.InboundCallID, Value = zohoLead.InboundCallID },
					new CustomField { Label = ZohoInventoryCustomFields.CallerID, Value = zohoLead.CallerID},
					new CustomField { Label = ZohoInventoryCustomFields.Duration, Value = zohoLead.Duration },
					new CustomField { Label = ZohoInventoryCustomFields.TaggedState, Value = zohoLead.TaggedCity },
					new CustomField { Label = ZohoInventoryCustomFields.CallDateTime, Value = zohoLead.CallDateTime },
					new CustomField { Label = ZohoInventoryCustomFields.LeadsDateTime, Value = zohoLead.LeadsDateTime }
				};
				if (zohoLead.IsUnbounceRecord)
				{
					zohoInventoryModel.Name = $"{zohoLead.FirstName} {zohoLead.LastName}";
					CustomField customField = new()
					{
						Label = ZohoInventoryCustomFields.Phone,
						Value = zohoLead.Phone
					};

					customFields.Add(customField);
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.Payout, Value = 30.ToString() });
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.UnbouncePageName, Value = zohoLead.UnbouncePageName });
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.IsUnbounceLead, Value = true.ToString() });

					zohoInventoryModel.Description = "Unbounce Lead";
					zohoInventoryModel.Rate = 30M;
				}
				else if (zohoLead.IsHeyFlowLead)
				{
					zohoInventoryModel.Name = $"{zohoLead.FirstName} {zohoLead.LastName}";
					CustomField customField = new()
					{
						Label = ZohoInventoryCustomFields.Phone,
						Value = zohoLead.Phone
					};
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.Payout, Value = 30.ToString() });
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.IsHeyFlowLead, Value = true.ToString() });
					customFields.Add(customField);
					zohoInventoryModel.Description = "Heyflow Lead";
					zohoInventoryModel.Rate = 30M;
				}
				else if (zohoLead.IsContactFormLead)
				{
					zohoInventoryModel.Name = $"{zohoLead.FirstName} {zohoLead.LastName}";
					CustomField customField = new()
					{
						Label = ZohoInventoryCustomFields.Phone,
						Value = zohoLead.Phone
					};
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.Payout, Value = 30.ToString() });
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.IsContactFormLead, Value = true.ToString() });
					customFields.Add(customField);
					zohoInventoryModel.Description = "Contact form Lead";
					zohoInventoryModel.Rate = 30M;
				}
				else
				{
					zohoInventoryModel.Name = $"{zohoLead.FirstName} {zohoLead.LastName}";
					
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.Payout, Value = !string.IsNullOrEmpty(zohoLead.Payout) ? zohoLead.Payout : 0.ToString() });
					customFields.Add(new CustomField { Label = ZohoInventoryCustomFields.Phone, Value = zohoLead.Phone });
					zohoInventoryModel.Description = "Ringba Lead";
					zohoInventoryModel.Rate = !string.IsNullOrEmpty(zohoLead.Payout) ? Convert.ToDecimal(zohoLead.Payout) : 0M;
				}
				zohoInventoryModel.ProductType = "service";
				zohoInventoryModel.Source = "user";
				zohoInventoryModel.Status = "active";
				zohoInventoryModel.SKU = zohoLead.LeadId;
				zohoInventoryModel.TrackSerialNumber = true;

				zohoInventoryModel.CustomFields = customFields;
				zohoInventoryRecords.Add(zohoInventoryModel);
			}

			return zohoInventoryRecords;
		}

		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
