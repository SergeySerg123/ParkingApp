using CoolParking.WebAPI.Models;
using System.Collections.ObjectModel;

namespace CoolParking.WebAPI.Interfaces
{
    public interface IParkingService
    {
        decimal GetBalance();
        int GetCapacity();
        int GetFreePlaces();
        void AddVehicle(Vehicle v);
        Vehicle RemoveVehicle(string vehicleId);
        void TopUpVehicle(string vehicleId, decimal sum);
        ReadOnlyCollection<Vehicle> GetVehicles();
        Vehicle GetVehicle(string vehicleId);
        TransactionInfo[] GetLastParkingTransactions();
    }
}
