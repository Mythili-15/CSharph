using System;

namespace RefReadOnlyDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int myNumber = 10;
            DisplayValue(ref myNumber);
            Console.WriteLine(myNumber);
        }

        // Method that takes a parameter by readonly reference
        static void DisplayValue(ref readonly int number)
        {
            Console.WriteLine($"Value: {number}");
            // Uncommenting the next line would cause a compile-time error
            // number++;
        }
    }
}
