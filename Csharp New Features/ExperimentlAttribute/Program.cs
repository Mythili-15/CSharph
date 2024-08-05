using System;
using System.Diagnostics.CodeAnalysis;

namespace ExperimentalAttributeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
#pragma warning disable test // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var result = GetNumber();
#pragma warning restore test // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            Console.WriteLine("Hello, World!" + result);
        }

        [Experimental("test")]
        static int GetNumber()
        {
            return 0;
        }
    }
}
