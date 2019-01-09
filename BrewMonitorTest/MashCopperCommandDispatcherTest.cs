using System;
using NUnit.Framework;
using BrewingModel.BrewMonitor;
using BrewingModel;

namespace BrewMonitorTest
{
    [TestFixture()]
    public class MashCopperCommandDispatcherTest
    {
        public MashCopperCommandDispatcherTest()
        {
        }

        [Test]
        public void CreateLiveBrewCommandTest()
        {
            //LiveBrewCommandManager liveBrewCommandManager = LiveBrewCommandManager.GetInstance();
            //string fieldSection = "Weigh bin Mash Copper";

            //LiveBrewCommandDispatcher liveBrewCommandDispatcher = liveBrewCommandManager.GetLiveBrewCommandDispatcher(fieldSection);

            //LiveBrewCommand liveBrewCommand = liveBrewCommandDispatcher.CreateLiveBrewCommand("Transport Time RAW Sorguum to WB MC - Finish", "", new Brew(), fieldSection);

            //Assert.AreEqual("BrewMonitor.StartMashCopperMashingInCommand", liveBrewCommand.GetType().ToString());
        }
    }
}
