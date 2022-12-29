using NUnit.Framework;
using System.Diagnostics;
namespace ProcessMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Error: Insufficient arguments provided. Please provide the process name, maximum lifetime and monitoring frequency.");
                Console.WriteLine("Usage: ProcessMonitor.exe process_name maximum_lifetime (in minutes) monitoring_frequency (also in minutes)");
                return;
            }

            string processName = args[0];
            int maximumLifetime;
            int monitoringFrequency;

            if (!int.TryParse(args[1], out maximumLifetime))
            {
                Console.WriteLine("Error: Invalid maximum lifetime provided. Please provide an integer value.");
                return;
            }

            if (!int.TryParse(args[2], out monitoringFrequency))
            {
                Console.WriteLine("Error: Invalid monitoring frequency provided. Please provide an integer value for the mniutes.");
                return;
            }

            Console.WriteLine($"Monitoring process '{processName}' with maximum lifetime of {maximumLifetime} minutes and monitoring frequency of {monitoringFrequency} minutes.");
            Console.WriteLine("Press 'q' to stop the monitoring.");

            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Q)
                {
                    break;
                }

                Process[] processes = Process.GetProcessesByName(processName);

                // Iterate over the processes and check if any of them have exceeded the maximum lifetime
                foreach (Process process in processes)
                {
                    if (process.StartTime.AddMinutes(maximumLifetime) < DateTime.Now)
                    {
                        Console.WriteLine($"Killing process '{process.ProcessName}' with PID {process.Id} as it has exceeded the maximum lifetime of {maximumLifetime} minutes.");
                        process.Kill();
                    }
                }

                Thread.Sleep(monitoringFrequency * 60 * 1000);
            }
        }
    }

}