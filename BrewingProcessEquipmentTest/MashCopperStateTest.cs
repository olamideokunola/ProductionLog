using NUnit.Framework;
using System;
using BrewingModel;
using BrewingModel.BrewingProcessEquipment;
using ProcessEquipmentParameters;

namespace BrewingProcessEquipmentTest
{
    [TestFixture()]
    public class MashCopperStateTest
    {
        [Test()]
        public void SetProcessStepEndTimeTestCase()
        {
            Brew brew = new Brew("01/01/2016","Maltina","258");
            MashCopper mashCopper = new MashCopper(brew);

            brew.SetProcessParameterValue(ProcessEquipment.MashCopper,
                                         MashCopperProcessParameters.HeatingUp1EndTime.ToString(),
                                         "10:00");

            string checkValue = brew.GetProcessParameterValue(ProcessEquipment.MashCopper,
                                                             MashCopperProcessParameters.HeatingUp1EndTime.ToString());


            MashCopperRest1State mashCopperRest1State = new MashCopperRest1State();
            mashCopperRest1State.SetEndTime("12:00", mashCopper, brew);

           

 
            string paraValue = brew.GetProcessParameterValue(ProcessEquipment.MashCopper,
                                          MashCopperProcessParameters.Rest1EndTime.ToString());

            Assert.AreEqual("12:00", paraValue);
        }
    }
}
