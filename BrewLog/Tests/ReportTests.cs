using BrewingModel.Datasources;
using BrewingModel.Reports;
using BrewingModel.Settings;
using NUnit.Framework;
using System;
using System.IO;

namespace BrewingModelTest
{
    [TestFixture()]
    public class ReportTests
    {
        Datasource datasource;
        XlPeriod period;
        MyAppSettings myAppSettings = MyAppSettings.GetInstance();
        string connectionString = "/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/datasource";
        string periodTemplateFilePath = "/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/period_template.xlsx";

        [Test()]
        public void CreateReportCase()
        {
            CreatePeriod();
            string reportName = "testReport";
            string reportPath = "/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/reports";

            XlReport report = new XlReport(period, reportName, reportPath);
            Assert.AreEqual("Brewing forms", report.XlReportWorksheet.Name);
        }

        private void CreatePeriod()
        {
            datasource = new XlDatasource(connectionString, periodTemplateFilePath);
            period = (BrewingModel.Datasources.XlPeriod)datasource.CreatePeriod("2018", Month.September);
        }

        [Test()]
        public void XlReportGeneratorCreateReportCase()
        {
            //LoadPeriods
            ReportGenerator xlReportGenerator = new XlReportGenerator();
            xlReportGenerator.LoadPeriods();

            //CreateReport
            string reportName = "testReportGeneratorReport";
            string reportPath = "/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/reports";
            xlReportGenerator.CreateReport("2018", Month.September, reportName, reportPath);
            XlReport xlReport = (XlReport)xlReportGenerator.Report;

            //Carry out test
            Assert.AreEqual("Brewing forms", xlReport.XlReportWorksheet.Name);
        }
    }
}
