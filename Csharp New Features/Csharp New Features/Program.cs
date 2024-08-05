using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        // Create an array using collection expressions
        int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

        // Create a list using collection expressions
        List<string> b = ["one", "two", "three"];

        // Create a span using collection expressions
        Span<char> c = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

        // Create a jagged 2D array using collection expressions
        int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

        // Create a jagged 2D array from variables using collection expressions
        int[] row0 = [1, 2, 3];
        int[] row1 = [4, 5, 6];
        int[] row2 = [7, 8, 9];
        int[][] twoDFromVariables = [row0, row1, row2];

        // Create a single array by spreading elements from other arrays
        int[] single = [.. row0, .. row1, .. row2];

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
