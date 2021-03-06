﻿using CoolParking.WebAPI.Interfaces;
using Newtonsoft.Json;

namespace CoolParking.WebAPI.Models
{
    public class VehicleSchema
    {

        public string id { get;  set; }

        public int vehicleType { get;  set; }

        public decimal balance { get;  set; }
    }
}
