// TODO: implement class Parking.
//       Implementation details are up to you, they just have to meet the requirements 
//       of the home task and be consistent with other classes and tests.



using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoolParking.BL.Models
{
    public class Parking 
    {
        private Parking instance = null;

        public int Capacity { get; private set; }
        public decimal Balance { get; private set; } = 0;
        private readonly List<Vehicle> Vehicles = new List<Vehicle>();

        private Parking() { }

        public Parking GetInstance()
        {
            if (instance == null)
            {
                instance = new Parking();
            }
            return instance;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicles.Add(vehicle);
        }

        public void RemoveVehicle(string id)
        {
            var vehicle = Vehicles.Find(v => v.Id == id);
            Vehicles.Remove(vehicle);
        }

        public ReadOnlyCollection<Vehicle> GetVehicles
            => new ReadOnlyCollection<Vehicle>(Vehicles);
    }
}

