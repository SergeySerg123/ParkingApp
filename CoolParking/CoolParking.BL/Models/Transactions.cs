using System;
using System.Collections.Generic;
using System.Text;

namespace CoolParking.BL.Models
{
    public class Transactions
    {
        private readonly List<TransactionInfo> lastTransactions = new List<TransactionInfo>();

        public void AddParkingTransaction(TransactionInfo transactionInfo)
        {
            lastTransactions.Add(transactionInfo);
        }
        
        public TransactionInfo[] GetLastParkingTransactions()
        {
            return lastTransactions.ToArray();
        }
    }
}
