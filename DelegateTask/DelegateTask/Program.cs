using System;
using System.Collections.Generic;

public class Employee
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Department { get; set; }

    public Employee(string name, int age, string department)
    {
        Name = name;
        Age = age;
        Department = department;
    }
}

public class Program
{
    // User-defined delegate
    public delegate bool EmployeeFilterDelegate(Employee employee);

    // Method to filter employees using a user-defined delegate
    public static List<Employee> FilterEmployees(List<Employee> employees, EmployeeFilterDelegate filter)
    {
        List<Employee> filteredEmployees = new List<Employee>();
        foreach (var employee in employees)
        {
            if (filter(employee))
            {
                filteredEmployees.Add(employee);
            }
        }
        return filteredEmployees;
    }

    // Method to filter employees using a built-in delegate (Func<T, bool>)
    public static List<Employee> FilterEmployees(List<Employee> employees, Func<Employee, bool> filter)
    {
        List<Employee> filteredEmployees = new List<Employee>();
        foreach (var employee in employees)
        {
            if (filter(employee))
            {
                filteredEmployees.Add(employee);
            }
        }
        return filteredEmployees;
    }

    public static void Main(string[] args)
    {
        var employees = new List<Employee>
        {
            new Employee("Aya", 22, "IT"),
            new Employee("Ahmed", 30, "HR"),
            new Employee("Mona", 25, "Finance"),
            new Employee("Ali", 28, "IT")
        };

        // User-defined delegate to filter employees in IT department
        EmployeeFilterDelegate itFilter = delegate (Employee employee) { return employee.Department == "IT"; };
        List<Employee> itEmployees = FilterEmployees(employees, itFilter);

        Console.WriteLine("Employees in IT department (using user-defined delegate):");
        foreach (var employee in itEmployees)
        {
            Console.WriteLine(employee.Name);
        }

        // Built-in delegate to filter employees older than 25
        Func<Employee, bool> ageFilter = employee => employee.Age > 25;
        List<Employee> olderEmployees = FilterEmployees(employees, ageFilter);

        Console.WriteLine("\nEmployees older than 25 (using built-in delegate):");
        foreach (var employee in olderEmployees)
        {
            Console.WriteLine(employee.Name);
        }
    }
}
