using System;
namespace BrewLogGui.Controllers
{
    public interface ICreateReportViewController
    {
        void CreateReport(string year, string month, string reportName, string reportPath);
    }
}
