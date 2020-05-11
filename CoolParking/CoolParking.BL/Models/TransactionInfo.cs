// TODO: implement struct TransactionInfo.
//       Necessarily implement the Sum property (decimal) - is used in tests.
//       Other implementation details are up to you, they just have to meet the requirements of the homework.

using System;
using System.Collections.Generic;

namespace CoolParking.BL.Models 
{
    public struct TransactionInfo
    {
        public TransactionInfo(decimal sum, DateTime dateTime, string vechicleId)
        {
            Sum = sum;
            DateTime = dateTime;
            VechicleId = vechicleId;
        }

        public decimal Sum { get; set; }
        public DateTime DateTime { get; set; }
        public string VechicleId { get; set; }
    }
}
