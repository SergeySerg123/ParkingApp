using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolParking.BL.Models
{
    public class TopUpSchema
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("sum")]
        public decimal Sum { get; set; }
        
    }
}
