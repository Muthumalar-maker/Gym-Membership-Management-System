
using System;
using System.IO;

namespace Gym_Membership_Management_System.Utilities
{
    public class OutputWriter
    {
        private readonly string filePath =
            Path.Combine(
                Environment.CurrentDirectory,
                "GymOutput.txt");

        public void Display(string message)
        {
            Console.WriteLine(message);

            File.AppendAllText(
                filePath,
                message + Environment.NewLine);
        }

        public void OpenOutputFile()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Gym Membership System Output");
            }

            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "notepad.exe",
                    Arguments = "\"" + filePath + "\"",
                    UseShellExecute = true
                });
        }

        public void ClearOutputFile()
        {
            File.WriteAllText(filePath, "");
        }
    }
}
