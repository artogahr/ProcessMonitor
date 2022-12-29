using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace ProcessMonitor.Tests
{
    public class ProcessMonitorTests
    {
        [Test]
        public void TestProcessKilling()
        {
            // In real life we could use some dummy binary for it, but I'm using notepad for now
            ProcessStartInfo startInfo = new ProcessStartInfo("notepad.exe");
            
            Process testProcess = Process.Start(startInfo);

            Process monitorProcess = Process.Start("ProcessMonitor.exe", "notepad 1 1");

            // Sleep for 2 minutes to allow the process to be killed
            Thread.Sleep(2 * 60 * 1000);

            // Check if the test process is still running
            Process[] processes = Process.GetProcessesByName("testProcess");
            Assert.IsEmpty(processes, "Test process was not killed by the monitoring utility.");

            monitorProcess.Kill();
            testProcess.Kill();
        }
    }

}
