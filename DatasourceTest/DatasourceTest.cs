using BrewingModel;
using BrewingModel.Datasources;
using NUnit.Framework;
using ProcessEquipmentParameters;
using System;

namespace DatasourceTest
{
    [TestFixture()]
    public class DatasourceTest
    {
        static string connectionString = "/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug";
        static Period period = new XlPeriod("2018", Month.January, connectionString);
        static XlPeriod xlPeriod = (XlPeriod)period;

        static string templateFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}period_template.xlsx";

        // Setup Datasource Handler
        static Datasource datasource = new XlDatasource(connectionString, templateFilePath);
        static DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance(datasource);

        [Test()]
        public void PeriodTestCase()
        {
            Console.Write(xlPeriod.XlBrewingFormWorksheet.Name);
            Assert.AreEqual("Brewing forms", xlPeriod.XlBrewingFormWorksheet.Name);
        }

        //[Test()]
        //public void GetColumnNumberTestCase()
        //{
        //    Brew brew = new Brew("01.01.2018", "Amstel", "AM2334");
        //    int colNumber = xlPeriod.GetColumnNumber(brew);
        //    //period.XlWorkSheet.Name = "Name";
        //    Assert.AreEqual(3, colNumber);
        //}


        [Test()]
        public void CreatePeriodWorkBookTestCase()
        {
            //XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            //xlDatasource.CreateNewPeriodWorkBook(period);
            //Assert.AreEqual("/home/olamide/Projects/BrewLog/Datasource/bin/Debug/2018", period.FileInfo.DirectoryName);
        }

        [Test()]
        public void AddPeriodTestCase()
        {
            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.AddPeriod(period);
            Assert.AreEqual("/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/2018", xlPeriod.FileInfo.DirectoryName);
        }

        [Test()]
        public void CreateAndAddPeriodTestCase()
        {
            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            Period newPeriod = xlDatasource.CreatePeriod("2018", Month.January);
            xlDatasource.AddPeriod(newPeriod);
            Assert.AreEqual("/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/2018", xlPeriod.FileInfo.DirectoryName);
        }

        [Test()]
        public void DeletePeriodWorkBookTestCase()
        {

            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.LoadPeriods();
            xlDatasource.DeletePeriod(period);

            Assert.AreEqual("/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/2018", xlPeriod.FileInfo.DirectoryName);
        }

        [Test()]
        public void LoadPeriodsTestCase()
        {
            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.LoadPeriods();

            Assert.AreEqual(3, xlDatasource.Periods.Count);
        }

        [Test()]
        public void PeriodLoadBrewsFromWorkSheetTestCase()
        {
            Period period = new XlPeriod("2018","January",connectionString);
            period.LoadBrews();

            Assert.AreEqual(2, period.Brews.Count);
        }

        [Test()]
        public void PeriodGetPeriodTestCase()
        {
            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.LoadPeriods();
            Period period = xlDatasource.GetPeriod("2018", Month.January);

            Assert.AreEqual("2018-January", period.PeriodName);
        }

        [Test()]
        public void PeriodAddBrewTestCase()
        {
            //CreateAndAddPeriodTestCase();

            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.LoadPeriods();

            Brew brew = new Brew("01.01.2018","Amstel","AM18007");
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInStartTime.ToString(), "01.01.2018 12:00:00");
            string month = brew.Month;
            //Assert.AreEqual("January", brew.Month);
            Period period = xlDatasource.GetPeriod("2018", Month.January);
            int oldBrewCount = period.Brews.Count;

            period.AddBrew(brew);

            //Assert.AreEqual("2018", brew.Year);
            Assert.AreEqual("2018-January", period.PeriodName);
            Assert.AreEqual(oldBrewCount + 1, period.Brews.Count);
        }

        [Test()]
        public void PeriodUpdateBrewTestCase()
        {
            //CreateAndAddPeriodTestCase();

            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.LoadPeriods();

            Brew brew = new Brew("01.01.2018", "Amstel", "AM18007");
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInStartTime.ToString(), "01.01.2018 12:00:00"); 
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInEndTime.ToString(), "01.01.2018 12:30:00");

            Period period = xlDatasource.GetPeriod("2018", Month.January);
            int oldBrewCount = period.Brews.Count;

            period.UpdateBrew(brew);

            Assert.AreEqual("2018-January", period.PeriodName);
            Assert.AreEqual(oldBrewCount, period.Brews.Count);
        }

        [Test()]
        public void DatasourceSaveBrewTestCase()
        {
            //CreateAndAddPeriodTestCase();

            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.LoadPeriods();

            Brew brew = new Brew("01.01.2018", "Amstel", "AM18007");
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInStartTime.ToString(), "01.01.2018 12:00:00");
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInEndTime.ToString(), "01.01.2018 12:40:00");

            string month = brew.Month;

            xlDatasource.SaveBrew(brew);
            Period period = xlDatasource.GetPeriod(brew);

            Assert.AreEqual("2018-January", period.PeriodName);
        }

        [Test()]
        public void BrewSaveBrewTestCase()
        {
            //CreateAndAddPeriodTestCase();
            //datasourceHandler.Datasource = datasource;

            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.LoadPeriods();

            Brew brew = new Brew("01.01.2018", "Amstel", "AM18008");
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInStartTime.ToString(), "01.01.2018 12:00:00");
            brew.SetProcessParameterValue(ProcessEquipment.MashCopper, MashCopperProcessParameters.MashingInEndTime.ToString(), "01.01.2018 12:38:00");

            string month = brew.Month;
            brew.Save();
            Period period = xlDatasource.GetPeriod(brew);

            Assert.AreEqual("2018-January", period.PeriodName);
        }
    }
}
    