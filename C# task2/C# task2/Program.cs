using System;

namespace ShapesApp
{
    // Approach 1: Using Inheritance and Polymorphism

    // Base class Shape (Abstraction)
    abstract class Shape
    {
        public abstract double CalculateArea(); // Abstraction
    }

    // Derived class Circle (Inheritance)
    class Circle : Shape
    {
        private double _radius; // Encapsulation

        public Circle(double radius)
        {
            _radius = radius;
        }

        public override double CalculateArea() // Polymorphism
        {
            return Math.PI * _radius * _radius;
        }
    }

    // Derived class Rectangle (Inheritance)
    class Rectangle : Shape
    {
        private double _width; // Encapsulation
        private double _height; // Encapsulation

        public Rectangle(double width, double height)
        {
            _width = width;
            _height = height;
        }

        public override double CalculateArea() // Polymorphism
        {
            return _width * _height;
        }
    }

    // Approach 2: Using Interfaces and Dependency Injection

    // Interface IShape (Abstraction)
    interface IShape
    {
        double CalculateArea(); // Abstraction
    }

    // Class Circle implementing IShape (Inheritance)
    class CircleUsingInterface : IShape
    {
        private double _radius; // Encapsulation

        public CircleUsingInterface(double radius)
        {
            _radius = radius;
        }

        public double CalculateArea() // Polymorphism
        {
            return Math.PI * _radius * _radius;
        }
    }

    // Class Rectangle implementing IShape (Inheritance)
    class RectangleUsingInterface : IShape
    {
        private double _width; // Encapsulation
        private double _height; // Encapsulation

        public RectangleUsingInterface(double width, double height)
        {
            _width = width;
            _height = height;
        }

        public double CalculateArea() // Polymorphism
        {
            return _width * _height;
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            // Approach 1: Using Inheritance and Polymorphism
            Shape circle = new Circle(5); // Radius of 5 units
            Shape rectangle = new Rectangle(4, 6); // Width of 4 units and height of 6 units

            Console.WriteLine("Using Inheritance and Polymorphism:");
            Console.WriteLine($"The area of the circle is: {circle.CalculateArea()}");
            Console.WriteLine($"The area of the rectangle is: {rectangle.CalculateArea()}");

            // Approach 2: Using Interfaces and Dependency Injection
            IShape circleUsingInterface = new CircleUsingInterface(5); // Radius of 5 units
            IShape rectangleUsingInterface = new RectangleUsingInterface(4, 6); // Width of 4 units and height of 6 units

            Console.WriteLine("\nUsing Interfaces and Dependency Injection:");
            Console.WriteLine($"The area of the circle is: {circleUsingInterface.CalculateArea()}");
            Console.WriteLine($"The area of the rectangle is: {rectangleUsingInterface.CalculateArea()}");
        }
    }
}
