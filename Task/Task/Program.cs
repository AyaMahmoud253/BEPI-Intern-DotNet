using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string Name { get; set; }
    public double Salary { get; set; }
    public string Department { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Part 1: Working with Numbers
        List<int> numbers = new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

        // Select first number
        int firstNumber = numbers.First();
        Console.WriteLine("First number: " + firstNumber);

        // Select last number
        int lastNumber = numbers.Last();
        Console.WriteLine("Last number: " + lastNumber);

        // Minimum and maximum
        int minNumber = numbers.Min();
        int maxNumber = numbers.Max();
        Console.WriteLine("Minimum number: " + minNumber);
        Console.WriteLine("Maximum number: " + maxNumber);

        // Average
        double averageNumber = numbers.Average();
        Console.WriteLine("Average number: " + averageNumber);

        // Select numbers after 5 numbers after start
        var numbersAfterFive = numbers.Skip(5).ToList();
        Console.WriteLine("Numbers after skipping first 5:");
        numbersAfterFive.ForEach(n => Console.WriteLine(n));

        // Select only 6 numbers not from start
        var sixNumbersNotFromStart = numbers.Skip(1).Take(6).ToList();
        Console.WriteLine("6 numbers not from start:");
        sixNumbersNotFromStart.ForEach(n => Console.WriteLine(n));

        Console.WriteLine("\n---\n");

        // Part 2: Working with Students and Sections
        List<Student> students = new List<Student>
        {
            new Student { Name = "Aya", Salary = 3000, Department = "IT" },
            new Student { Name = "Ahmed", Salary = 3500, Department = "HR" },
            new Student { Name = "Mona", Salary = 4000, Department = "Finance" },
            new Student { Name = "Ali", Salary = 3200, Department = "IT" },
            new Student { Name = "Sara", Salary = 4200, Department = "HR" }
        };

        // Select first and last student
        var firstStudent = students.First();
        var lastStudent = students.Last();
        Console.WriteLine("First student: " + firstStudent.Name);
        Console.WriteLine("Last student: " + lastStudent.Name);

        // Calculate average salary
        double averageSalary = students.Average(s => s.Salary);
        Console.WriteLine("Average salary: " + averageSalary);

        // Minimum and maximum salary
        double minSalary = students.Min(s => s.Salary);
        double maxSalary = students.Max(s => s.Salary);
        Console.WriteLine("Minimum salary: " + minSalary);
        Console.WriteLine("Maximum salary: " + maxSalary);

        // Students with salary more than average
        var aboveAverageSalaryStudents = students.Where(s => s.Salary > averageSalary).ToList();
        Console.WriteLine("Students with salary above average:");
        aboveAverageSalaryStudents.ForEach(s => Console.WriteLine(s.Name + ": " + s.Salary));

        // Select top 2 student salaries
        var top2Students = students.OrderByDescending(s => s.Salary).Take(2).Select(s => new { s.Name, s.Salary }).ToList();
        Console.WriteLine("Top 2 students by salary:");
        top2Students.ForEach(s => Console.WriteLine(s.Name + ": " + s.Salary));

        // Select student with department name
        var studentsWithDepartment = students.Select(s => new { s.Name, s.Department }).ToList();
        Console.WriteLine("Students with department:");
        studentsWithDepartment.ForEach(s => Console.WriteLine(s.Name + " - " + s.Department));

        // Group students by department
        var groupedByDepartment = students.GroupBy(s => s.Department).ToList();
        Console.WriteLine("Grouped by department:");
        foreach (var group in groupedByDepartment)
        {
            Console.WriteLine("Department: " + group.Key);
            foreach (var student in group)
            {
                Console.WriteLine(student.Name);
            }
        }
    }
}
