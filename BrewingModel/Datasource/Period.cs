using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace BrewingModel.Datasource
{
    public abstract class Period
    {
        string periodName;
        string year;
        string month;

        IDictionary<string, Brew> _brews;
        ExcelPackage package;
        ExcelWorkbook workBook;

        public abstract void AddBrew(IBrew brew);

        public IDictionary<string, Brew> Brews
        {
            get
            {
                return _brews;
            }
        }
    }
}
