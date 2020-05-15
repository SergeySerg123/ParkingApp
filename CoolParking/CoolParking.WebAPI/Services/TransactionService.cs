using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using System;

namespace CoolParking.WebAPI.Services
{
    public class TransactionService : ITransactionsService
    {
        private readonly Transactions transactions; 

        public TransactionService(Transactions transactions) 
        {
            this.transactions = transactions;
        }

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
