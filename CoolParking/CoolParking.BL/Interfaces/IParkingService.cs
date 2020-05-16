using System;
using System.Collections.ObjectModel;
using CoolParking.BL.Models;

namespace CoolParking.BL.Interfaces
{
    public interface IParkingService
    {
        decimal GetBalance();
        int GetCapacity();
        int GetFreePlaces();
        ReadOnlyCollection<VehicleSchema> GetVehicles();
        bool AddVehicle(Vehicle vehicle);
        bool RemoveVehicle(string vehicleId);
        bool TopUpVehicle(string vehicleId, decimal sum);
        TransactionInfo[] GetLastParkingTransactions();
        string ReadFromLog();
    }
}
