// TODO: implement struct TransactionInfo.
//       Necessarily implement the Sum property (decimal) - is used in tests.
//       Other implementation details are up to you, they just have to meet the requirements of the homework.

using Newtonsoft.Json;
using System;

namespace CoolParking.WebAPI.Models
{
    public struct TransactionInfo
    {
        public TransactionInfo(decimal sum, DateTime dateTime, string vechicleId)
        {
            Sum = sum;
            TransactionDate = dateTime;
            VechicleId = vechicleId;
        }

        [JsonProperty("vehicleId")]
        public string VechicleId { get; set; }

        [JsonProperty("transactionDate")]
        public DateTime TransactionDate { get; set; }

        [JsonProperty("sum")]
        public decimal Sum { get; set; }
       
        
    }
}
