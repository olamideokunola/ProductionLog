﻿using System;
using System.Collections.Generic;
using System.IO;
using BrewingModel;
using OfficeOpenXml;
using Util;

namespace Datasource
{
	public class XlDatasource : Datasource.DataSource
    {

        public XlDatasource(string connectionString, string templatePath)
        {
            //template = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}period_template.xlsx");
            this.template = new FileInfo(templatePath);
            this.connectionString = connectionString;
            //this.connectionString = $"{AppDomain.CurrentDomain.BaseDirectory}";
        }

        public override void AddPeriod(Period period)
        {
            using (xlExcelPackage = new ExcelPackage(period.FileInfo))
            {
                if (!periods.ContainsKey(period.PeriodName))
                {
                    CreateNewPeriodWorkBook(period);
                    periods.Add(period.PeriodName, period);
                }
            }
        }

        public override Period CreatePeriod(string year, Month month)
        {
            Period period = new Period(year, month, this.connectionString);
            return period;
        }

        public override Period CreatePeriod(string year, string month)
        {
            Period period = new Period(year, month, this.connectionString);
            return period;
        }

        public override void DeletePeriod(Period period)
        {
            using (xlExcelPackage = new ExcelPackage(period.FileInfo))
            {
                if (periods.ContainsKey(period.PeriodName) && period.FileInfo.Exists)
                {
                    DeletePeriodWorkBook(period);
                    periods.Remove(period.PeriodName);
                }
            }
        }

        private void DeletePeriodWorkBook(Period period)
        {
            using (xlExcelPackage = new ExcelPackage(period.FileInfo))
            {
                Byte[] bin = xlExcelPackage.GetAsByteArray();

                FileInfo file = period.FileInfo;
                File.Delete(file.FullName);
            }
        }

        public override IDictionary<string, Period> LoadPeriods()
        {
            DirectoryInfo dir = new DirectoryInfo(ConnectionString);

            // Get year directories
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                string year = subDir.Name;
                // Get months in the directory
                foreach (FileInfo file in subDir.GetFiles())
                {
                    int startIndex = file.Name.LastIndexOf(file.Extension, StringComparison.CurrentCulture);
                    string month = file.Name.Remove(startIndex);
                    Period period = new Period(year, month, connectionString);
                    string periodName = year + "-" + month;
                    periods.Add(periodName, period);
                }
            }

            return periods;
        }

        public override void UpdatePeriod(Period period)
        {
            throw new NotImplementedException();
        }

        private string CreateNewPeriodWorkBook(Period period)
        {
            using (xlExcelPackage = new ExcelPackage(template, true))
            {
                Byte[] bin = xlExcelPackage.GetAsByteArray();

                FileInfo file = period.FileInfo;
                File.WriteAllBytes(file.FullName, bin);
                return file.FullName;
            }
        }

        public override IBrew GetBrewProcessParameters(IBrew brew)
        {
            using (xlExcelPackage = new ExcelPackage(template, true))
            {
                string brewNumber = brew.BrewNumber;
                string startDate = brew.StartDate;

                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                string year = stringDateWorker.GetYear(startDate);
                string month = stringDateWorker.GetMonth(startDate);

                Period period = CreatePeriod(year, month);

                return period.GetBrewFromWorkSheet(brew);
            }
        }
    }
}
