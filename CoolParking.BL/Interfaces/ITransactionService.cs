using CoolParking.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoolParking.BL.Interfaces
{
    public interface ITransactionService
    {
        void CreateTransaction(decimal sum, DateTime dateTime, string vechicleId);
        TransactionInfo[] GetLastParkingTransactions();
    }
}
