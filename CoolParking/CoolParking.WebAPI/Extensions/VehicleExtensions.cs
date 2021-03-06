﻿using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;

namespace CoolParking.WebAPI.Extensions
{
    public static class VehicleExtensions
    {
        public static VehicleSchema ToVehicleSchema(this Vehicle vehicle)
            => new VehicleSchema { id = vehicle.Id, vehicleType = (int)vehicle.VehicleType, balance = vehicle.Balance};
    }
}
