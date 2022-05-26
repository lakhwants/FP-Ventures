using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventory.Models
{
	public class AccountAddress
    {
        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }

    public class Account
    {
        [JsonProperty("accountCreationEpoch")]
        public long AccountCreationEpoch { get; set; }

        [JsonProperty("timeZoneId")]
        public string TimeZoneId { get; set; }

        [JsonProperty("accountAddress")]
        public AccountAddress AccountAddress { get; set; }

        [JsonProperty("accountMngrId")]
        public string AccountMngrId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
    }

    public class RingbaAccountModel
    {

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("account")]
        public List<Account> Account { get; set; }
    }


}
