using System;
using BrewMonitor;
using System.Collections.Generic;
namespace BrewLog
{
    class MainClass
    {
        //public static void Main(string[] args)
        //{

        //    TestMonitorTimer();

        //}

        private static void TestFileParser()
        {
            string filePath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/2018/september/9/";
            string brewNumber = "258";
            FileParser fileParser = new FileParser();

            IDictionary<string, string> headerFields = fileParser.GetHeaderFields();

            foreach (KeyValuePair<string, string> headerField in headerFields)
            {
                Console.WriteLine("Header Field: " + headerField.Key.Trim() + ": " + headerField.Value);
            }

            IDictionary<string, IDictionary<string, string>> sectionFields = fileParser.GetSectionFields();

            foreach (KeyValuePair<string, IDictionary<string, string>> sectionField in sectionFields)
            {
                Console.WriteLine("Section: " + sectionField.Key.Trim());
            }
        }

        private static void TestMonitorTimer()
        {
            //LiveBrewMonitor liveBrewMonitor = new LiveBrewMonitor();
            //MonitorTimer monitorTimer = new MonitorTimer(3000, liveBrewMonitor);
            //MonitorTimer.Start();
        }
    }
}
