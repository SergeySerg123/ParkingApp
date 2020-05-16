using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System;
using System.Net.Http;

namespace CoolParking.BL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly Transactions transactions = new Transactions();
        private TransactionService() { }

        public static TransactionService CreateInstance()
         => new TransactionService();

        public void CreateTransaction(Parking p, Vehicle vehicle)
        {
            if (vehicle != null)
            {
                var transactionInfo = Withdraw(p, vehicle);
                transactions.AddParkingTransaction(transactionInfo);
            }
            
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return transactions.GetLastParkingTransactions();
        }

        private TransactionInfo Withdraw(Parking p, Vehicle v)
        {
            var priceForHour = Settings.GetPrice(v.VehicleType);
            decimal actualPrice = p.WithdrawFromVechicle(v, priceForHour);
            p.TopUpParking(actualPrice);
            return new TransactionInfo(actualPrice, DateTime.Now, v.Id);
        }
    }
}
