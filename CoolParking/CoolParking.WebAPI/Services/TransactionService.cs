using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using System;

namespace CoolParking.WebAPI.Services
{
    public class TransactionService : ITransactionsService
    {
        private readonly Transactions transactions = new Transactions();
        //private static TransactionService instance = null;

        public TransactionService() { }

        //public static TransactionService GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new TransactionService();
        //    }
        //    return instance;
        //}

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
