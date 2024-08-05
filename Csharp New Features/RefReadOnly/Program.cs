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

        // Method that takes a parameter by reference (no enforcement of immutability)
        static void DisplayValue(ref int number)
        {
            Console.WriteLine($"Value: {number}");
            // The number can be modified here, which is not intended
            number++;
        }
    }
}
