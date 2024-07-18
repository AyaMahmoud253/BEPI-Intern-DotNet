double num1, num2, result = 0;
char operation;

Console.WriteLine("Console Calculator\n");

// Input first number
Console.Write("Enter the first number: ");
num1 = Convert.ToDouble(Console.ReadLine());

// Input operation
Console.Write("Enter an operator (+, -, *, /): ");
operation = Convert.ToChar(Console.ReadLine());

// Input second number
Console.Write("Enter the second number: ");
num2 = Convert.ToDouble(Console.ReadLine());

// Perform calculation based on operation
if (operation == '+')
{
    result = num1 + num2;
}
else if (operation == '-')
{
    result = num1 - num2;
}
else if (operation == '*')
{
    result = num1 * num2;
}
else if (operation == '/')
{
    // Handle division by zero
    if (num2 != 0)
    {
        result = num1 / num2;
    }
    else
    {
        Console.WriteLine("Error: Division by zero is not allowed.");
        return;
    }
}
else
{
    Console.WriteLine("Error: Invalid operator.");
    return;
}

// Output result
Console.WriteLine($"\nResult: {num1} {operation} {num2} = {result}");