using System;
using System.Collections.Generic;
using System.IO;
using BrewingModel;
using OfficeOpenXml;

namespace Datasource
{
    public abstract class DataSource
    {
        /// <summary>
        /// The Datasource allows access to the data store of processed brews
        /// It is made up of periods, which represent each month of production
        /// It is represented on the system as a folder structure
        /// </summary>

        protected string connectionString;
        protected IDictionary<string, Period> periods = new Dictionary<string, Period>();

        protected string fileName;
        protected FileInfo template;

        // Excel Objects
        protected ExcelPackage xlExcelPackage;
        protected ExcelWorksheet xlWorkSheet;

        public IDictionary<string, Period> Periods
        {
            get
            {
                return periods;
            }
        }

        public string ConnectionString { get => connectionString; set => connectionString = value; }


        // Methods
        public abstract Period CreatePeriod(string year, Month month);
        public abstract Period CreatePeriod(string year, string month);

        public abstract void AddPeriod(Period period);

        public abstract void UpdatePeriod(Period period);

        public abstract void DeletePeriod(Period period);

        public abstract IDictionary<string, Period> LoadPeriods();

        public abstract IBrew GetBrewProcessParameters(IBrew brew);
    }
}
