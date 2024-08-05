using System;
using System.IO;

namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the File Processing Application!");

            // Check if a file path is provided as a command line argument
            if (args.Length == 0)
            {
                Console.WriteLine("ERROR: No file path provided. Please specify a file path.");
                return;
            }

            // Get the file path from the command line arguments
            string filePath = args[0];

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"ERROR: The file at {filePath} does not exist.");
                return;
            }

            // Create an instance of FileProcessor
            var fileProcessor = new FileProcessor(filePath);

            // Start processing the file
            fileProcessor.Process();

            Console.WriteLine("Processing complete.");
        }
    }
}
