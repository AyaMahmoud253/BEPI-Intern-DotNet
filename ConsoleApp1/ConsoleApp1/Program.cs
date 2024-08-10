using System;

class Program
{
    // Method that takes a value parameter
    static void PassByValue(int param)
    {
        param += 5; // This change will not affect the original value
        Console.WriteLine($"\nInside PassByValue method: {param}");
    }

    // Method that takes a reference parameter
    static void PassByReference(ref int param)
    {
        param += 5; // This change will affect the original value
        Console.WriteLine($"\nInside PassByReference method: {param}");
    }

    // The Main method
    static void Main(string[] args)
    {
        int valueParameter = 10;
        int referenceParameter = 10;

        Console.WriteLine("Before method call:");
        Console.WriteLine($"Value Parameter: {valueParameter}");
        Console.WriteLine($"Reference Parameter: {referenceParameter}");

        // Passing by value
        PassByValue(valueParameter);

        // Passing by reference
        PassByReference(ref referenceParameter);

        Console.WriteLine("\nAfter method call:");
        Console.WriteLine($"Value Parameter: {valueParameter}");
        Console.WriteLine($"Reference Parameter: {referenceParameter}");
    }
}
