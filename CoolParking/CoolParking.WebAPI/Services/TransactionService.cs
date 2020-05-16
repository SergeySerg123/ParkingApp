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
            decimal actualPrice = WithdrawFromVechicle(v, priceForHour);
            p.TopUpParking(actualPrice);
            return new TransactionInfo(actualPrice, DateTime.Now, v.Id);
        }

        public decimal WithdrawFromVechicle(Vehicle v, decimal sum)
        {
            var balance = v.Balance;
            decimal actualSum = ((balance - sum) < 0) ? sum * ApplyPenalty(balance, sum) : sum;
            v.Withdraw(actualSum);
            return actualSum;
        }

        private decimal ApplyPenalty(decimal balance, decimal sum)
        {
            decimal total = balance - sum;
            return total * (decimal)Settings.PenaltyRatio;
        }
    }
}
