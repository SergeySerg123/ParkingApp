using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System;

namespace CoolParking.BL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly Transactions transactions = new Transactions();

        public void CreateTransaction(decimal sum, DateTime dateTime, string vechicleId)
        {
            var transactionInfo = new TransactionInfo(sum, dateTime, vechicleId);
            transactions.AddParkingTransaction(transactionInfo);
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return transactions.GetLastParkingTransactions();
        }
    }
}
