using System;
using System.IO;
using static System.Console;

namespace DataProcessor
{
    class FileProcessor
    {
        private const string BackupDirectoryName = "backup";
        private const string InProgressDirectoryName = "processing";
        private const string CompletedDirectoryName = "complete";
        public string InputFilePath { get; }
        public FileProcessor(string filePath) => InputFilePath = filePath;

        public void Process()
        {
            WriteLine($"Begin process of {InputFilePath}");

            if (!File.Exists(InputFilePath))
            {
                WriteLine($"ERROR: file {InputFilePath} does not exist.");
                return;
            }

            string rootDirectoryPath = GetRootDirectoryPath();
            CreateBackupDirectory(rootDirectoryPath);
            string backupFilePath = BackupFile(rootDirectoryPath);
            string inProgressFilePath = MoveToInProgressDirectory(rootDirectoryPath);

            string extension = Path.GetExtension(InputFilePath);
            string completedFilePath = PrepareCompletedFilePath(rootDirectoryPath, extension);

            ProcessFileByExtension(extension, inProgressFilePath, completedFilePath);

            FinalizeProcessing(inProgressFilePath);
        }

        private string GetRootDirectoryPath()
        {
            return new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
        }

        private void CreateBackupDirectory(string rootDirectoryPath)
        {
            string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);
            WriteLine($"Attempting to create {backupDirectoryPath}");
            Directory.CreateDirectory(backupDirectoryPath);
        }

        private string BackupFile(string rootDirectoryPath)
        {
            string inputFileName = Path.GetFileName(InputFilePath);
            string backupFilePath = Path.Combine(rootDirectoryPath, BackupDirectoryName, inputFileName);
            WriteLine($"Copying {InputFilePath} to {backupFilePath}");
            File.Copy(InputFilePath, backupFilePath, true);
            return backupFilePath;
        }

        private string MoveToInProgressDirectory(string rootDirectoryPath)
        {
            Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
            string inputFileName = Path.GetFileName(InputFilePath);
            string inProgressFilePath = Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);

            if (File.Exists(inProgressFilePath))
            {
                WriteLine($"ERROR: a file with the name {inProgressFilePath} is already being processed.");
                return inProgressFilePath; // Returning early to avoid moving file
            }

            WriteLine($"Moving {InputFilePath} to {inProgressFilePath}");
            File.Move(InputFilePath, inProgressFilePath);
            return inProgressFilePath;
        }

        private string PrepareCompletedFilePath(string rootDirectoryPath, string extension)
        {
            string completedDirectoryPath = Path.Combine(rootDirectoryPath, CompletedDirectoryName);
            Directory.CreateDirectory(completedDirectoryPath);
            string completedFileName = $"{Path.GetFileNameWithoutExtension(InputFilePath)}-{Guid.NewGuid()}{extension}";
            return Path.Combine(completedDirectoryPath, completedFileName);
        }

        private void ProcessFileByExtension(string extension, string inProgressFilePath, string completedFilePath)
        {
            switch (extension)
            {
                case ".txt":
                    var textProcessor = new TextFileProcessor(inProgressFilePath, completedFilePath);
                    textProcessor.Process();
                    break;
                /*case ".data":
                    var binaryProcessor = new BinaryFileProcessor(inProgressFilePath, completedFilePath);
                    binaryProcessor.Process();
                    break;
                case ".csv":
                    var csvProcessor = new CsvFileProcessor(inProgressFilePath, completedFilePath);
                    csvProcessor.Process();
                    break;*/
                default:
                    WriteLine($"{extension} is an unsupported file type.");
                    break;
            }
        }

        private void FinalizeProcessing(string inProgressFilePath)
        {
            WriteLine($"Completed processing of {inProgressFilePath}");
            WriteLine($"Deleting {inProgressFilePath}");
            File.Delete(inProgressFilePath);
        }
    }
}
