// New Approach (C# 10 onwards)
using System;

namespace AliasAnyType
{
    // Alias for the Employee type
    using NitishEmployee = Employee;

    // Alias for a custom point type
    using CustomPoint = (int X, int Y);

    // Alias for a 2D array of integers
    using My2DArray = int[,];

    internal class Program
    {
        static void Main(string[] args)
        {
            // Usage of type aliases
            //NitishEmployee employee = new NitishEmployee();
            //Console.WriteLine(employee.GetName());

            CustomPoint myPoint = (X: 10, Y: 20);
            Console.WriteLine($"{myPoint.X} {myPoint.Y}");

            My2DArray myArray = { {1, 2}, {3, 4} };
        }
    }
}
