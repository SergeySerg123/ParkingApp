using CoolParking.BL.Models;


namespace CoolParking.BL.Interfaces
{
    public interface ITransactionService
    {
        void CreateTransaction(Parking p, Vehicle vehicle);
        TransactionInfo[] GetLastParkingTransactions();
    }
}
