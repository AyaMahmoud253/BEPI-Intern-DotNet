using System;

namespace GenericAndExtensionMethods
{
    public static class Program
    {
        // Generic method to sum two numbers of any numeric type
        public static T Sum<T>(T a, T b) where T : struct, IComparable, IConvertible, IFormattable
        {
            return (dynamic)a + (dynamic)b;
        }

        // Extension method to add '*' before and after a string
        public static string AddStars(this string str)
        {
            return $"*{str}*";
        }

        public static void Main()
        {
            // Example usage of the generic sum method
            int sumInt = Sum(10, 20);
            double sumDouble = Sum(5.5, 3.3);
            decimal sumDecimal = Sum(7.7m, 2.3m);

            Console.WriteLine($"Sum of 10 and 20 (int): {sumInt}");
            Console.WriteLine($"Sum of 5.5 and 3.3 (double): {sumDouble}");
            Console.WriteLine($"Sum of 7.7 and 2.3 (decimal): {sumDecimal}");

            // Example usage of the string extension method
            string originalString = "Hello";
            string modifiedString = originalString.AddStars();

            Console.WriteLine($"Original String: {originalString}");
            Console.WriteLine($"Modified String: {modifiedString}");
        }
    }
}
