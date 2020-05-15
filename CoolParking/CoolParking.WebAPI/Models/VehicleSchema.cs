using CoolParking.WebAPI.Interfaces;

namespace CoolParking.WebAPI.Models
{
    public class VehicleSchema
    {
        public string id { get; private set; }
        public int vehicleType { get; private set; }
        public decimal balance { get; private set; }

        public VehicleSchema(Vehicle vehicle)
        {
            id = vehicle.Id;
            vehicleType = (int) vehicle.VehicleType;
            balance = vehicle.Balance;
        }
    }
}
