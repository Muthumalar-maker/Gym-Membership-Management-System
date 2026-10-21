
using System;
using System.IO;

namespace Gym_Membership_Management_System.Utilities
{
    public class OutputWriter
    {
        private readonly string filePath = "GymOutput.txt";

        public void Display(string message)
        {
            Console.WriteLine(message);

            File.AppendAllText(
                filePath,
                message + Environment.NewLine);
        }

        public void ClearOutputFile()
        {
            File.WriteAllText(filePath, "");
        }
    }
}
