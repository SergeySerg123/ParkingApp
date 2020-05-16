// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.
using CoolParking.BL.Extensions;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;


namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        private readonly ILogService _logService;
        private readonly ITransactionService _transactionService;
        private readonly ITimerService _withdrawTimer;
        private readonly ITimerService _logTimer;

        public ParkingService(ITimerService withdrawTimer, ITimerService logTimer, ILogService logService)
        {
            _logService = logService;
            _transactionService = TransactionService.CreateInstance();
            _withdrawTimer = withdrawTimer;
            _logTimer = logTimer;
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            using(var client = new HttpClient())
            {
                VehicleSchema vehicleSchema = vehicle.ToVehicleSchema();
                client.BaseAddress = new Uri(Settings.BASE_URL);
                var res = client.PostAsJsonAsync("api/vehicles", vehicleSchema).Result;
                return res.IsSuccessStatusCode;
            }
            
        }

        public bool RemoveVehicle(string vehicleId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Settings.BASE_URL);
                var res = client.DeleteAsync("api/vehicles/" + vehicleId).Result;
                return res.IsSuccessStatusCode;
            }
        }

        public bool TopUpVehicle(string vehicleId, decimal sum)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Settings.BASE_URL);
                var res = client.PutAsJsonAsync("api/transactions/topupvehicle", new TopUpSchema { Id = vehicleId, Sum = sum}).Result;
                return res.IsSuccessStatusCode;
            }
        }

       
        public ReadOnlyCollection<VehicleSchema> GetVehicles()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(Settings.BASE_URL_VEHICLES_API);
                string vehicles = response.Result;
                return JsonConvert.DeserializeObject<ReadOnlyCollection<VehicleSchema>>(vehicles);
            }                
        }

        public decimal GetBalance()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(Settings.BASE_URL_PARKING_API + "balance");
                string balance = response.Result;
                var b = decimal.Parse(balance, CultureInfo.InvariantCulture);
                return Math.Round(b, 2);

            }
        }


        public int GetCapacity()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(Settings.BASE_URL_PARKING_API + "capacity");
                string capacity = response.Result;
                return int.Parse(capacity);
            }               
        }

        public int GetFreePlaces()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(Settings.BASE_URL_PARKING_API + "freePlaces");
                string freePlaces = response.Result;
                return int.Parse(freePlaces);
            }               
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return _transactionService.GetLastParkingTransactions();
        }

        public string ReadFromLog()
        {
            string s = _logService.Read();
            return s;
        }
    }
}