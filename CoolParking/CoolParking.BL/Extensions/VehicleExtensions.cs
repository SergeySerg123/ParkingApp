using CoolParking.BL;
using CoolParking.BL.Models;

namespace CoolParking.BL.Extensions
{
    public static class VehicleExtensions
    {
        public static VehicleSchema ToVehicleSchema(this Vehicle vehicle)
            => new VehicleSchema { id = vehicle.Id, vehicleType = (int)vehicle.VehicleType, balance = vehicle.Balance};
    }
}
