using System;
using System.Collections.Generic;
using System.IO;
using BrewingModel;
using OfficeOpenXml;
using Util;

namespace BrewingModel.Datasources
{
	public class XlDatasource : Datasource
    {

        private ExcelPackage xlExcelPackage;

        public XlDatasource(string connectionString, string templatePath)
        {
            //template = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}period_template.xlsx");
            this.template = new FileInfo(templatePath);
            this.connectionString = connectionString;
            //this.connectionString = $"{AppDomain.CurrentDomain.BaseDirectory}";
        }

        public XlDatasource()
        {
        }

        public override void AddPeriod(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(xlPeriod.FileInfo))
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
            Period period = new XlPeriod(year, month.ToString(), this.connectionString);
            return period;
        }

        public override Period CreatePeriod(string year, string month)
        {
            Period period = new XlPeriod(year, month, this.connectionString);
            return period;
        }

        public override void DeletePeriod(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(xlPeriod.FileInfo))
            {
                if (periods.ContainsKey(period.PeriodName) && xlPeriod.FileInfo.Exists)
                {
                    DeletePeriodWorkBook(period);
                    periods.Remove(period.PeriodName);
                }
            }
        }

        private void DeletePeriodWorkBook(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(xlPeriod.FileInfo))
            {
                Byte[] bin = xlExcelPackage.GetAsByteArray();

                FileInfo file = xlPeriod.FileInfo;
                File.Delete(file.FullName);
            }
        }


        public override void LoadPeriods()
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
                    Period period = new XlPeriod(year, month, connectionString);
                    string periodName = year + "-" + month;
                    periods.Add(periodName, period);
                }
            }
        }

        public override void UpdatePeriod(Period period)
        {
            throw new NotImplementedException();
        }

        private string CreateNewPeriodWorkBook(Period period)
        {
            XlPeriod xlPeriod = (XlPeriod)period;
            using (xlExcelPackage = new ExcelPackage(template, true))
            {
                Byte[] bin = xlExcelPackage.GetAsByteArray();

                FileInfo file = xlPeriod.FileInfo;
                File.WriteAllBytes(file.FullName, bin);
                return file.FullName;
            }
        }

        public override IBrew GetBrewWithProcessParameters(IBrew brew)
        {
            using (xlExcelPackage = new ExcelPackage(template, true))
            {
                string brewNumber = brew.BrewNumber;
                string startDate = brew.StartDate;

                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                string year = stringDateWorker.GetYear(startDate);
                string month = stringDateWorker.GetMonth(startDate);

                Period period = CreatePeriod(year, month);

                return period.GetBrewWithProcessParameters(brew);
            }
        }

        public override Period GetPeriod(IBrew brew)
        {
            return GetPeriod(brew.Year, brew.Month);
        }

        public override Period GetPeriod(string year, Month month)
        {            
            return GetPeriod(year, month.ToString());
        }

        public Period GetPeriod(string year, string month)
        {
            string periodName = year + "-" + month;
            if (periods.ContainsKey(periodName))
            {
                Period period = periods[periodName];
                period.LoadBrews();
                return period;
            }
            else
            {
                return null;
            }
        }

        public override string SaveBrew(IBrew brew)
        {
            if(brew.BrewNumber.Length > 0 && brew.BrandName.Length > 0 && brew.StartDate.Length > 0)
            {
                Period period = GetPeriod(brew);
                if(period.Brews.ContainsKey(brew.BrewNumber))
                {
                    period.UpdateBrew(brew);
                }
                else
                {
                    period.AddBrew(brew);
                }
                return "Success";
            }
            else
            {
                return "Failure";
            }
        }

    }
}
