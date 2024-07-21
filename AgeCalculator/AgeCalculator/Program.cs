using System;

namespace AgeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool validInput = false;
            int birthYear = 0;

            while (!validInput)
            {
                try
                {
                    Console.Write("Enter your birth year: ");
                    birthYear = int.Parse(Console.ReadLine());

                    // Check if the birth year is in a valid range
                    if (birthYear < 1900 || birthYear > DateTime.Now.Year)
                    {
                        // Throw an exception if the year is out of the valid range
                        throw new ArgumentOutOfRangeException(null, "The year must be between 1900 and the current year.");
                    }

                    validInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid year.");//if input is not a number
                }
                catch (ArgumentOutOfRangeException ex)//handling out of range exceptions
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)// General catch any other exceptions
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }

            int age = DateTime.Now.Year - birthYear;

            Console.WriteLine($"You are {age} years old.");

            // Prevent the console window from closing immediately
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
