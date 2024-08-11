using System;

namespace ReferenceTypeAndBoxingExample
{
    class Person
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void ChangeNameValue(Person person)
        {
            person.Name = "Aya";
        }

        static void AssignNewPerson(Person person)
        {
            person = new Person { Name = "salwa" };
        }

        static void ChangeNameRef(ref Person person)
        {
            person = new Person { Name = "salwa" };
        }

        static void Main(string[] args)
        {
            // Example 1: Passing Reference Type by Value
            Person person1 = new Person { Name = "Jojo" };
            Console.WriteLine("Before ChangeNameValue: " + person1.Name);  // Output: Jojo

            ChangeNameValue(person1);
            Console.WriteLine("After ChangeNameValue: " + person1.Name);   // Output: Aya

            AssignNewPerson(person1);
            Console.WriteLine("After AssignNewPerson: " + person1.Name);   // Output: Aya

            // Example 2: Passing Reference Type by Reference
            Person person2 = new Person { Name = "Jojo" };
            Console.WriteLine("\nBefore ChangeNameRef: " + person2.Name);  // Output: Jojo

            ChangeNameRef(ref person2);
            Console.WriteLine("After ChangeNameRef: " + person2.Name);   // Output: salwa

            // Example 3: Boxing
            int value = 123;  // Value type
            object boxedValue = value;  // Boxing

            Console.WriteLine("\nBoxed value: " + boxedValue);  // Output: 123

            // Example 4: Unboxing
            object anotherBoxedValue = 456;  // Boxing
            int unboxedValue = (int)anotherBoxedValue;  // Unboxing

            Console.WriteLine("Unboxed value: " + unboxedValue);  // Output: 456
        }
    }
}
