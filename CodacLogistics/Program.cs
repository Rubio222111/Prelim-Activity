using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        // Set culture to Philippines for Peso symbol
        CultureInfo phCulture = new CultureInfo("en-PH");

       

        // Data Type: string for names
        Console.Write("Enter Driver's Full Name: ");
        string driverName = Console.ReadLine();

        // Data Type: decimal for currency (Budget) to ensure financial precision
        Console.Write("Enter Weekly Fuel Budget (PHP): ");
        decimal weeklyBudget = decimal.Parse(Console.ReadLine());

        // Data Type: double for distance (km) allows for decimal ranges like 10.5
        double totalDistance = 0;

        // Validation: While loop ensures the user cannot proceed until valid data (1.0 - 5000.0) is entered
        bool validDistance = false;
        while (!validDistance)
        {
            Console.Write("Enter Total Distance Traveled this week (km): ");
            string input = Console.ReadLine();

            // Attempt to parse input to double
            if (double.TryParse(input, out totalDistance))
            {
                if (totalDistance >= 1.0 && totalDistance <= 5000.0)
                {
                    validDistance = true; // Exit the loop
                }
                else
                {
                    Console.WriteLine("Error: Distance must be between 1.0 and 5000.0 km.");
                }
            }
            else
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
        }

       
        decimal[] fuelExpenses = new decimal[5];
        decimal totalFuelSpent = 0m;

        // Control Flow: For loop to iterate through the 5 days
        for (int i = 0; i < fuelExpenses.Length; i++)
        {
            // Logic: (i + 1) is used because arrays are 0-indexed, but days start at 1
            Console.Write($"Enter fuel cost for Day {i + 1}: ₱");
            string dayInput = Console.ReadLine();

            // Parse and store
            fuelExpenses[i] = decimal.Parse(dayInput);

            // Accumulate total
            totalFuelSpent += fuelExpenses[i];
        }

       

        // Calculate Average Daily Fuel Expense
        decimal averageFuel = totalFuelSpent / 5;

        // Calculate Fuel Efficiency Rating (Km per Peso)
        // We cast totalFuelSpent to double to match the totalDistance data type
        double efficiencyRatio = totalDistance / (double)totalFuelSpent;
        string efficiencyRating = "";

        // Control Flow: If/Else for efficiency ratings
        if (efficiencyRatio > 15)
        {
            efficiencyRating = "High Efficiency";
        }
        else if (efficiencyRatio >= 10)
        {
            efficiencyRating = "Standard Efficiency";
        }
        else
        {
            efficiencyRating = "Low Efficiency / Maintenance Required";
        }

        // Determine Budget Status (Boolean check)
        bool underBudget = totalFuelSpent <= weeklyBudget;

        Console.WriteLine("\n" + new string('=', 45));
        Console.WriteLine("         CODAC LOGISTICS AUDIT REPORT");
        Console.WriteLine(new string('=', 45));

        // I/O: String Interpolation with Peso Sign (₱)
        Console.WriteLine($"Driver:           {driverName}");
        Console.WriteLine($"Weekly Budget:    ₱{weeklyBudget:N2}");
        Console.WriteLine($"Total Distance:   {totalDistance} km");

        Console.WriteLine("\n--- Daily Expense Breakdown ---");
        for (int i = 0; i < 5; i++)
        {
            // Displaying Day 1-5 using (i+1)
            Console.WriteLine($"Day {i + 1}:         ₱{fuelExpenses[i]:N2}");
        }

        Console.WriteLine(new string('-', 35));
        Console.WriteLine($"Total Fuel Spent: ₱{totalFuelSpent:N2}");
        Console.WriteLine($"Avg. Daily Cost:  ₱{averageFuel:N2}");
        Console.WriteLine($"Efficiency Ratio: {efficiencyRatio:F2} km/₱");
        Console.WriteLine($"Rating:           {efficiencyRating}");

        // Data Type: Bool displayed as "Yes" or "No" for better readability
        Console.WriteLine($"\nUnder Budget?:   {(underBudget ? "YES (Good Job)" : "NO (Over Budget)")}");

        Console.WriteLine(new string('=', 45));
    }
}