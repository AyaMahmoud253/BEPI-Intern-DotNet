using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // List<T> Example
        List<string> names = new List<string>();
        names.Add("Aya");
        names.Add("Mona");
        names.Add("Ahmed");

        Console.WriteLine("List<T> Example:");
        Console.WriteLine("First name: " + names[0]);

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        names.Remove("Mona");
        Console.WriteLine("Total names after removal: " + names.Count);
        Console.WriteLine();

        // Dictionary<TKey, TValue> Example
        Dictionary<int, string> studentGrades = new Dictionary<int, string>();
        studentGrades[1] = "A";   // Aya's grade
        studentGrades[2] = "B";   // Mona's grade
        studentGrades[3] = "A";   // Ahmed's grade

        Console.WriteLine("Dictionary<TKey, TValue> Example:");
        Console.WriteLine("Aya's grade: " + studentGrades[1]);

        if (studentGrades.ContainsKey(2))
        {
            Console.WriteLine("Mona's grade: " + studentGrades[2]);
        }

        foreach (var kvp in studentGrades)
        {
            Console.WriteLine($"Student {kvp.Key}: {kvp.Value}");
        }

        studentGrades.Remove(2);  // Removing Mona's grade
        Console.WriteLine();

        // Queue<T> Example
        Queue<string> tasks = new Queue<string>();
        tasks.Enqueue("Prepare report");  // Task for Aya
        tasks.Enqueue("Review code");     // Task for Mona
        tasks.Enqueue("Meeting with team"); // Task for Ahmed

        Console.WriteLine("Queue<T> Example:");
        Console.WriteLine("Processing: " + tasks.Dequeue());  // Process first task
        Console.WriteLine("Next task: " + tasks.Peek());     // View next task

        foreach (var task in tasks)
        {
            Console.WriteLine("Remaining task: " + task);
        }
        Console.WriteLine();

        // Stack<T> Example
        Stack<int> numbers = new Stack<int>();
        numbers.Push(100);   // Aya's number
        numbers.Push(200);   // Mona's number
        numbers.Push(300);   // Ahmed's number

        Console.WriteLine("Stack<T> Example:");
        Console.WriteLine("Popped: " + numbers.Pop());     // Remove and show last added number
        Console.WriteLine("Top of the stack: " + numbers.Peek()); // Show current top number

        foreach (var number in numbers)
        {
            Console.WriteLine("Remaining number: " + number);
        }
        Console.WriteLine();

        // HashSet<T> Example
        HashSet<string> fruits = new HashSet<string>();
        fruits.Add("Apple");   // Aya's favorite fruit
        fruits.Add("Banana");  // Mona's favorite fruit
        fruits.Add("Cherry");  // Ahmed's favorite fruit
        fruits.Add("Apple");   // Duplicate entry (will not be added)

        Console.WriteLine("HashSet<T> Example:");
        Console.WriteLine("Contains 'Banana': " + fruits.Contains("Banana"));

        foreach (var fruit in fruits)
        {
            Console.WriteLine("Fruit: " + fruit);
        }
    }
}
