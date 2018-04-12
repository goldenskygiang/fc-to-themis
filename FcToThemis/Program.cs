using System;
using System.IO;

namespace FcToThemis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Task name: ");
            string taskName = Console.ReadLine();

            string newInputName = $"{taskName}.inp";
            string newOutputName = $"{taskName}.out";

            Console.Write("Specify Free Contest test directory: ");
            string fcDir = Console.ReadLine();

            string themisTestFolder = $@"{fcDir}\{taskName}";
            Directory.CreateDirectory(themisTestFolder);

            string[] testFiles = Directory.GetFiles(fcDir);
            int tests = testFiles.Length / 2;

            int suffixNumLength = tests.ToString().Length;
            string folderNameFormat = $"D{suffixNumLength}";

            int successfulTests = 0;
            for (int i = 0, count = 1; count <= tests; i += 2, count++)
            {
                try
                {
                    string testFolderName = $"Test{count.ToString(folderNameFormat)}";
                    string testDir = $@"{themisTestFolder}\{testFolderName}";
                    Directory.CreateDirectory(testDir);

                    string destInput = Path.Combine(testDir, newInputName);
                    string destOutput = Path.Combine(testDir, newOutputName);

                    File.Copy(testFiles[i], destInput);
                    File.Copy(testFiles[i + 1], destOutput);

                    successfulTests++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error copying test #{count}: {ex.Message}");
                }
            }

            Console.WriteLine($"Completed. {successfulTests}/{tests} copied.");
            Console.WriteLine($"Themis test folder is at \"{themisTestFolder}\".");
            Console.ReadLine();
        }
    }
}
