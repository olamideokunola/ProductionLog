using NUnit.Framework;
using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModelTest
{
    [TestFixture()]
    public class BrewTest
    {
        [Test()]
        public void SetProcessParameterValueTest()
        {
            Brew brew = new Brew("01/01/2016", "Maltina", "258");
            string paramName = MashCopperProcessParameters.MashingInStartTime.ToString();
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, paramName, "12:00");

            string paramValue = brew.GetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInStartTime.ToString());
            Assert.AreEqual("12:00", paramValue);
        }

        [Test()]
        public void ConvertStringToDateTest()
        {
            string dateStr = "01.01.1990 20:40:31";
            DateTime dateConverted = DateHelper.ConvertStringToDateTime(dateStr);
            Assert.AreEqual("Datetime", dateConverted.GetType());
        }
    }
}
