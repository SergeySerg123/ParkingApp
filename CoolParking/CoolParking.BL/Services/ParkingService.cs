// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        private readonly Parking Parking;
        private readonly ILogService _logService;
        private readonly ITransactionService _transactionService;
        private readonly ITimerService _withdrawTimer;
        private readonly ITimerService _logTimer;
        private readonly HttpClient httpClient = new HttpClient();
        bool disposed = false;

        public ParkingService(ITimerService withdrawTimer, ITimerService logTimer, ILogService logService)
        {
            Parking = Parking.GetInstance();
            _logService = logService;
            _transactionService = TransactionService.GetInstance();
            _withdrawTimer = withdrawTimer;
            _logTimer = logTimer;
        }

        // Test of addVehicle TODO Console
        public bool AddVehicle(Vehicle vehicle)
        {
            var request = new HttpRequestMessage();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.RequestUri = new Uri(Settings.BASE_URL_VEHICLES_API);
            var response =  httpClient.PostAsync(Settings.BASE_URL_VEHICLES_API, httpContent);
            var vehicleAsJson =  response.Result.Content.ReadAsStringAsync();
            var v = JsonConvert.DeserializeObject<Vehicle>(vehicleAsJson.Result);
            return (v != null);
        }

        public bool RemoveVehicle(string vehicleId)
        {
            Parking.RemoveVehicle(vehicleId);
            return true;
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            Parking.TopUpVehicle(vehicleId, sum);
        }

       
        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            var response = httpClient.GetStringAsync(Settings.BASE_URL_VEHICLES_API);
            string vehicles =  response.Result;
            return JsonConvert.DeserializeObject<ReadOnlyCollection<Vehicle>>(vehicles);
        }

        public decimal GetBalance() 
        {
            var response = httpClient.GetStringAsync(Settings.BASE_URL_PARKING_API + "balance");
            string balance = response.Result;
            return decimal.Parse(balance);
        } 

        public int GetCapacity()
        {
            var response = httpClient.GetStringAsync(Settings.BASE_URL_PARKING_API + "capacity");
            string capacity = response.Result;
            return int.Parse(capacity);
        }

        public int GetFreePlaces()
        {
            var response = httpClient.GetStringAsync(Settings.BASE_URL_PARKING_API + "freePlaces");
            string freePlaces = response.Result;
            return int.Parse(freePlaces);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Parking.Distruct();
                _logTimer.Dispose();
            }

            disposed = true;
        }

        //~ParkingService()
        //{
        //    Dispose(false);
        //}

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