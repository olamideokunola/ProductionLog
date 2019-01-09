using System;
using NUnit.Framework;
using BrewingModel.BrewMonitor;
using System.Collections.Generic;

namespace BrewMonitorTest
{
    [TestFixture()]
    public class RequiredFieldsTest
    {
        public RequiredFieldsTest()
        {
        }

        [Test]
        public void HasStartFieldTest()
        {
            IDictionary<string, IDictionary<string, string>> availableFields = new Dictionary<string, IDictionary<string, string>>();

            //Weigh bin Mash Copper Section
            IDictionary<string, string> param = new Dictionary<string, string>();
            param = new Dictionary<string, string>
            {
                { "Start Transport RAW Sorguum to WB MC - Finish", "" },
                { "Transport Time RAW Sorguum to WB MC - Finish", "" }
            };

            availableFields.Add("Weigh bin Mash Copper", param);


            RequiredFieldsChecker requiredFieldsChecker = new RequiredFieldsChecker();

            Assert.AreEqual(true, requiredFieldsChecker.HasStartField(availableFields));
        }

        //[Test]
        public void SetStartSectionAndFieldsTest()
        {
            RequiredFieldsChecker requiredFieldsChecker = new RequiredFieldsChecker();
            //Assert.AreEqual(true, requiredFieldsChecker.StartSectionAndFields.ContainsKey("StartField"));
        }

    }
}
