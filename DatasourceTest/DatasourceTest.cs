using BrewingModel;
using Datasource;
using NUnit.Framework;
using System;
namespace DatasourceTest
{
    [TestFixture()]
    public class DatasourceTest
    {
        static string connectionString = "/home/olamide/Projects/BrewLog/Datasource/bin/Debug";
        Period period = new Period("2018", Month.March, connectionString);

        static string templateFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}period_template.xlsx";

        [Test()]
        public void PeriodTestCase()
        {
            Assert.AreEqual("Brewing forms", period.XlWorkSheet.Name);
        }

        [Test()]
        public void GetColumnNumberTestCase()
        {
            Brew brew = new Brew("01.01.2018", "Amstel", "214");
            int colNumber = period.GetColumnNumber(brew);
            //period.XlWorkSheet.Name = "Name";
            Assert.AreEqual(4, colNumber);
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
            Assert.AreEqual("/home/olamide/Projects/BrewLog/Datasource/bin/Debug/2018", period.FileInfo.DirectoryName);
        }

        [Test()]
        public void CreateAndAddPeriodTestCase()
        {

            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            Period newPeriod = xlDatasource.CreatePeriod("2018", Month.March);
            xlDatasource.AddPeriod(newPeriod);
            Assert.AreEqual("/home/olamide/Projects/BrewLog/Datasource/bin/Debug/2018", period.FileInfo.DirectoryName);
        }

        [Test()]
        public void DeletePeriodWorkBookTestCase()
        {

            XlDatasource xlDatasource = new XlDatasource(connectionString, templateFilePath);
            xlDatasource.DeletePeriod(period);

            Assert.AreEqual("/home/olamide/Projects/BrewLog/Datasource/bin/Debug/2018", period.FileInfo.DirectoryName);
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
        
            Period period = new Period("2018","January",connectionString);
            period.LoadBrewsFromWorkSheet();

            Assert.AreEqual(2, period.Brews.Count);
        }
    }
}
    