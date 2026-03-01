using System;

namespace GreenTransitFleet
{
   
    {
        public LowEnergyException(string message) : base(message) { }
    }

   
    public abstract class Vehicle
    {
        // Private Fields (Encapsulation: protects data)
        private string _vehicleID;
        private string _modelName;

        // Public Properties with Validation (Encapsulation)
        public string VehicleID
        {
            get { return _vehicleID; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Vehicle ID cannot be empty.");
                _vehicleID = value;
            }
        }

        public string ModelName
        {
            get { return _modelName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Model Name cannot be empty.");
                _modelName = value;
            }
        }

        // Constructor using base() syntax (Inheritance)
        protected Vehicle(string vehicleID, string modelName)
        {
            VehicleID = vehicleID;
            ModelName = modelName;
        }

        // Virtual Method (Polymorphism: can be overridden)
        public virtual double CalculateRange()
        {
            return 0;
        }

        // Virtual Method for displaying details
        public virtual string GetDetails()
        {
            return $"Vehicle ID: {VehicleID}, Model: {ModelName}";
        }
    }

   
    public class ElectricBus : Vehicle
    {
        // Private Field
        private double _batteryPercent;

        // Public Property with validation (Encapsulation)
        public double BatteryPercent
        {
            get { return _batteryPercent; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Battery percentage must be between 0 and 100.");
                _batteryPercent = value;
            }
        }

        // Constructor using : base() (Inheritance)
        public ElectricBus(string vehicleID, string modelName, double batteryPercent)
            : base(vehicleID, modelName)
        {
            BatteryPercent = batteryPercent;
        }

        // Override CalculateRange (Polymorphism)
        public override double CalculateRange()
        {
            // Exception: Throw if battery is below 5%
            if (_batteryPercent < 5)
            {
                throw new LowEnergyException($"CRITICAL: {VehicleID} battery too low ({_batteryPercent}%)! Charge immediately!");
            }

            // Electric bus max range: 400km at 100% battery
            return (_batteryPercent / 100) * 400;
        }

        public override string GetDetails()
        {
            return base.GetDetails() + $", Type: Electric Bus, Battery: {_batteryPercent}%";
        }
    }

  
    public class GasPoweredVan : Vehicle
    {
        // Private Field
        private double _fuelLevel;

        // Public Property with validation (Encapsulation)
        public double FuelLevel
        {
            get { return _fuelLevel; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Fuel level must be between 0 and 100.");
                _fuelLevel = value;
            }
        }

        // Constructor using : base() (Inheritance)
        public GasPoweredVan(string vehicleID, string modelName, double fuelLevel)
            : base(vehicleID, modelName)
        {
            FuelLevel = fuelLevel;
        }

        // Override CalculateRange (Polymorphism)
        public override double CalculateRange()
        {
            // Exception: Throw if fuel is below 5%
            if (_fuelLevel < 5)
            {
                throw new LowEnergyException($"CRITICAL: {VehicleID} fuel too low ({_fuelLevel}%)! Refuel immediately!");
            }

            // Van max range: 300km at 100% fuel
            return (_fuelLevel / 100) * 300;
        }

        public override string GetDetails()
        {
            return base.GetDetails() + $", Type: Gas Van, Fuel: {_fuelLevel}%";
        }
    }

  
    class Program
    {
        static void Main(string[] args)
        {
            // Try-Catch-Finally for Robustness
            try
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("    GREEN-TRANSIT FLEET MANAGER");
                Console.WriteLine("===========================================");

               
                Console.WriteLine("\n--- Creating Electric Bus ---");
                ElectricBus bus = new ElectricBus("EV-001", "Tesla Bus", 45);
                Console.WriteLine(bus.GetDetails());

                // Calculate range (will work since battery > 5%)
                double busRange = bus.CalculateRange();
                Console.WriteLine($"Estimated Range: {busRange} km");

               
                Console.WriteLine("\n--- Creating Gas Powered Van ---");
                GasPoweredVan van = new GasPoweredVan("GV-002", "Ford Transit", 80);
                Console.WriteLine(van.GetDetails());

                // Calculate range
                double vanRange = van.CalculateRange();
                Console.WriteLine($"Estimated Range: {vanRange} km");

                
                Console.WriteLine("\n--- Testing Low Energy Exception ---");
                ElectricBus lowBatteryBus = new ElectricBus("EV-003", "City Bus", 3); // Below 5%
                
             
                try
                {
                    double lowRange = lowBatteryBus.CalculateRange();
                    Console.WriteLine($"Range: {lowRange} km");
                }
                catch (LowEnergyException ex)
                {
                    Console.WriteLine($"EXCEPTION CAUGHT: {ex.Message}");
                }

              
                Console.WriteLine("\n--- Polymorphism Demo ---");
                Vehicle[] fleet = new Vehicle[]
                {
                    new ElectricBus("EV-100", "Volvo Electric", 60),
                    new GasPoweredVan("GV-200", "Mercedes Sprinter", 40),
                    new ElectricBus("EV-300", "BYD Bus", 90)
                };

                double totalRange = 0;
                foreach (Vehicle v in fleet)
                {
                    Console.WriteLine($"{v.VehicleID}: {v.CalculateRange()} km");
                    totalRange += v.CalculateRange();
                }
                Console.WriteLine($"Total Fleet Range: {totalRange} km");

                
                Console.WriteLine("\n--- Testing Argument Exceptions ---");
                try
                {
                    ElectricBus invalidBus = new ElectricBus("", "Test Bus", 50); // Invalid ID
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"VALIDATION ERROR: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                // Catches any unexpected errors
                Console.WriteLine($"\nFATAL ERROR: {ex.Message}");
            }
            finally
            {
                // Always runs - Robustness
                Console.WriteLine("\n===========================================");
                Console.WriteLine("        System Shutdown / Session Ended");
                Console.WriteLine("===========================================");
            }
        }
    }
}