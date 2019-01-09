using System;
using NUnit.Framework;
using BrewingModel.BrewMonitor;
using System.Timers;

namespace BrewMonitorTest
{
    [TestFixture()]
    public class LiveBrewMonitorTest
    {
        public LiveBrewMonitorTest()
        {
        }

        [Test]
        public void StartMonitoringTest()
        {
            string filePath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/2018/september/9/";
            string brewNumber = "258";

            LiveBrewMonitor liveBrewMonitor = LiveBrewMonitor.GetInstance();
            liveBrewMonitor.StartMonitoring(filePath, "Maltina", brewNumber);

            Assert.IsTrue(liveBrewMonitor.IsMonitoring);
        }


    }

}
