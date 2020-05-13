using CoolParking.WebAPI.Interfaces;
using CoolParking.WebAPI.Models;

namespace CoolParking.WebAPI.Extensions
{
    public static class VehicleExtensions
    {
        public static VehicleSchema ToVehicleSchema(this Vehicle vehicle)
            => new VehicleSchema(vehicle);
    }
}
