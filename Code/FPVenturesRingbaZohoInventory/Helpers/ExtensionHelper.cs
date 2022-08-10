using FPVenturesRingbaZohoInventory.Constants;
using FPVenturesRingbaZohoInventory.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace FPVenturesRingbaZohoInventory.Helpers
{
    public static class ExtensionHelper
    {
        public static string GetMessageTagValue(this CallLogDetail messageTag, string key)
        {
            return messageTag.MessageTags.FirstOrDefault(x => x.Name == key)?.Value;
        }

        public static string GetMethodName(this Exception ex)
        {
            return new StackTrace(ex).GetFrame(0).GetMethod().Name;
        }   
        
        public static string ToZohoDateString(this DateTime date)
        {
                return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        } 
        
        public static string RemoveCountryCode(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return null;

            foreach (var country in CountryCodes.PhoneCountryCodes)
                phone = phone.Replace(country, "");

            return phone;
        }

    }
}
