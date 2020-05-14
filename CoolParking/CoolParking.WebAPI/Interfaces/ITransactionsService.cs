using CoolParking.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolParking.WebAPI.Interfaces
{
    public interface ITransactionsService
    {
        void CreateTransaction(Parking p, Vehicle vehicle);
        TransactionInfo[] GetLastParkingTransactions();
    }
}
