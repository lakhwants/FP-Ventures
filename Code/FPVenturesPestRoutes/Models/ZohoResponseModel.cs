using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Models
{
	public class ModifiedBy
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class CreatedBy
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Details
    {
        [JsonProperty("api_name")]
        public string ApiName { get; set; }

        [JsonProperty("Modified_Time")]
        public DateTime ModifiedTime { get; set; }

        [JsonProperty("Modified_By")]
        public ModifiedBy ModifiedBy { get; set; }

        [JsonProperty("Created_Time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Created_By")]
        public CreatedBy CreatedBy { get; set; }

	}

    public class Datum
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("details")]
        public Details Details { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ZohoResponseModel
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }

}
