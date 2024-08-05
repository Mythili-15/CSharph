using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        // Create an array:
        int[] a = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        // Create a list:
        List<string> b = new List<string> { "one", "two", "three" };

        // Create a span:
        Span<char> c = stackalloc char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'h', 'i' };

        // Create a jagged 2D array:
        int[][] twoD = new int[][]
        {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6 },
            new int[] { 7, 8, 9 }
        };

        // Create a jagged 2D array from variables:
        int[] row0 = new int[] { 1, 2, 3 };
        int[] row1 = new int[] { 4, 5, 6 };
        int[] row2 = new int[] { 7, 8, 9 };
        int[][] twoDFromVariables = new int[][] { row0, row1, row2 };

        // Create a single array by manually combining elements from other arrays
        List<int> tempList = new List<int>();
        tempList.AddRange(row0);
        tempList.AddRange(row1);
        tempList.AddRange(row2);
        int[] single = tempList.ToArray();

        // Print the elements of the single array
        foreach (var element in single)
        {
            Console.Write($"{element}, ");
        }
        Console.WriteLine();

        // Output:
        // 1, 2, 3, 4, 5, 6, 7, 8, 9,
    }
}
