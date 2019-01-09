using System;
using NUnit.Framework;
using BrewingModel.BrewMonitor;
using System.Collections.Generic;
namespace BrewMonitorTest
{
    [TestFixture()]
    public class RequiredBrewParametersTest
    {
        public RequiredBrewParametersTest()
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


            RequiredBrewParameters requiredBrewParameters = new RequiredBrewParameters();

            Assert.AreEqual(true, requiredBrewParameters.HasStartField(availableFields));
        }

        [Test]
        public void FieldIsRequiredTest()
        {
            RequiredBrewParameters requiredBrewParameters = new RequiredBrewParameters();
            Assert.True(requiredBrewParameters.FieldIsRequired("MASH COPPER", "Mash in Time - Finish"));
        }

        [Test]
        public void FieldIsAvailableTest()
        {
            RequiredBrewParameters requiredBrewParameters = new RequiredBrewParameters();
            Assert.False(requiredBrewParameters.FieldIsAvailable("MASH COPPER", "Mash in Time - Finish"));
        }

        [Test]
        public void PopulateFieldsTest()
        {
            RequiredBrewParameters requiredBrewParameters = new RequiredBrewParameters();

            IDictionary<string, IDictionary<string, string>> availableFields = new Dictionary<string, IDictionary<string, string>>();

            //Weigh bin Mash Copper Section
            IDictionary<string, string> param = new Dictionary<string, string>();
            param = new Dictionary<string, string>
            {
                { "Start Transport RAW Sorguum to WB MC - Finish", "20" },
                { "Transport Time RAW Sorguum to WB MC - Finish", "30" }
            };

            availableFields.Add("Weigh bin Mash Copper", param);

            requiredBrewParameters.PopulateFields(availableFields);
            Assert.True(requiredBrewParameters.FieldIsAvailable("Weigh bin Mash Copper", "Transport Time RAW Sorguum to WB MC - Finish"));

        }
    }
}
