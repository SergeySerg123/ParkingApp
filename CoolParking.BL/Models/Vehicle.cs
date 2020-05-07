// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal).
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing.
//       The Balance should be able to change only in the CoolParking.BL project.
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.
public class Vehicle
{
    public string Id { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public decimal Balance { get; private set; }

    public Vehicle(string id, VehicleType type, decimal b)
    {
        Id = id;
        VehicleType = type;
        Balance = b;
    }
}