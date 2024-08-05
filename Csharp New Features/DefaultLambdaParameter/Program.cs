using System;

namespace DefaultLambdaDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lambda expression with a default parameter
            var GetSquare = (int number = 10) => number * number;

            Console.WriteLine(GetSquare(5));  // Output: 25 (using provided value)
            Console.WriteLine(GetSquare());   // Output: 100 (using default value)
        }
    }
}
