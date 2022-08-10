using FPVenturesZohoInventoryVendorCredits.Constants;
using System;
using System.Diagnostics;

namespace FPVenturesZohoInventoryVendorCredits.Helpers
{
    public static class ExtensionHelper
    {

        public static string GetMethodName(this Exception ex)
        {
            return new StackTrace(ex).GetFrame(0).GetMethod().Name;
        }

        public static string ToZohoDateString(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss+00:00");
        }

        public static string RemoveCountryCode(this string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return null;

            foreach (var country in CountryCodes.PhoneCountryCodes)
                phone = phone.Replace(country, "");

            return phone;
        }

        public static string ToInventoryDate(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

    }
}
