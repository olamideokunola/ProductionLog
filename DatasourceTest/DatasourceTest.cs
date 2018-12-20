using BrewingModel;
using BrewingModel.Datasources;
using NUnit.Framework;
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

        [Test()]
        public void PeriodTestCase()
        {
            Console.Write(xlPeriod.XlBrewingFormWorksheet.Name);
            Assert.AreEqual("Brewing forms", xlPeriod.XlBrewingFormWorksheet.Name);
        }

        [Test()]
        public void GetColumnNumberTestCase()
        {
            Brew brew = new Brew("01.01.2018", "Amstel", "AM2334");
            int colNumber = xlPeriod.GetColumnNumber(brew);
            //period.XlWorkSheet.Name = "Name";
            Assert.AreEqual(3, colNumber);
        }


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
    }
}
    