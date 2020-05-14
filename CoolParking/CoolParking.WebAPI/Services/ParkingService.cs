using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;
using System;
using System.Collections.ObjectModel;
using static CoolParking.WebAPI.Helpers.VehicleValidator;

namespace CoolParking.WebAPI.Services
{
    public class ParkingService : IParkingService
    {
        private readonly Parking Parking;
        private readonly ITransactionsService _transactionService;

        public ParkingService(Parking parking,
            ITransactionsService transactionService)
        {
            Parking = parking;
            _transactionService = transactionService;
        }


        //TODO: exception when v >= 10
        public Vehicle AddVehicle(Vehicle vehicle)
        {
            Vehicle v = GetVehicle(vehicle.Id);
            if (v != null)
            {
                return null;
            } 
           
            return Parking.AddVehicle(vehicle);
        }

        public Vehicle RemoveVehicle(string vehicleId)
        {
            Vehicle vehicle = GetVehicle(vehicleId);
            if (vehicle == null)
            {
                return vehicle;
            }
            return Parking.RemoveVehicle(vehicle);
        }

        public Vehicle TopUpVehicle(string vehicleId, decimal sum)
        {
            bool isValidSum = IsValidTopUpSum(sum);
            if (isValidSum)
            {
                Vehicle vehicle = Parking.GetVehicle(vehicleId);
                if (vehicle != null)
                {
                    return vehicle.TopUpVehicle(sum);
                }
                return vehicle;
            }
            return null;
        }

        public decimal GetBalance() => Parking.Balance;

        public int GetCapacity() => Parking.Capacity;

        public int GetFreePlaces() => Parking.GetFreePlaces();

        public Vehicle GetVehicle(string vehicleId)
        {
            return Parking.GetVehicle(vehicleId);
        }

        public ReadOnlyCollection<Vehicle> GetVehicles() => Parking.GetVehicles;

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return _transactionService.GetLastParkingTransactions();
        }
    }
}
