using System;
using NUnit.Framework;
using BrewingModel.BrewMonitor;
using BrewingModel;
using System.Collections.Generic;
namespace BrewMonitorTest
{
    [TestFixture()]
    public class BrewInProcessTest
    {
        public BrewInProcessTest()
        {
        }

        string filePath;
        string brewNumber;
        IDictionary<string, string> headerFields;
        IDictionary<string, IDictionary<string, string>> sectionFields;

        BrewInProcess brewInProcess;

        public void SetUpBrewInProcess()
        {
            filePath = "258.txt";
            brewNumber = "258";

            headerFields = new Dictionary<string, string>
            {
                { "Date", "" }
            };

            IDictionary<string, string> sectionParams = new Dictionary<string, string>
            {
                { "Protein Rest Time - Finish", "" }
            };

            sectionFields = new Dictionary<string, IDictionary<string, string>>
            {
                { "MASH COPPER", sectionParams }
            };

            brewInProcess = new BrewInProcess(filePath, brewNumber, headerFields, sectionFields);
        }

        [Test]
        public void UpdateTest()
        {
            SetUpBrewInProcess();

            IDictionary<string, string> nHeaderFields = new Dictionary<string, string>
            {
                { "Date", "" }
            };

            IDictionary<string, string> sectionParams = new Dictionary<string, string>
            {
                { "Protein Rest Time - Finish", "60" }
            };

            IDictionary<string, IDictionary<string, string>> nSectionFields = new Dictionary<string, IDictionary<string, string>>
            {
                { "MASH COPPER", sectionParams }
            };


            brewInProcess.Update(nHeaderFields, nSectionFields);

            Assert.True (brewInProcess.HasNewField());
        }

        
    }
}
