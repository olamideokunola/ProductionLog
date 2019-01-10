using System;
using BrewingModel;
using BrewingModel.Datasources;
using BrewingModel.Reports;
using Models;
using Observer;

namespace BrewLogGui.Controllers
{
	public class BrewLoggerGuiController : IBrewLoggerGuiController
    {
        private Subject guiModel;
        private BrewParametersGuiModel brewParametersGuiModel;
        private IBrewLoggerGuiView guiView;
        private BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();
        private ReportGenerator reportGenerator;

        public string ProcessEquipment
        {
            get {
                return brewParametersGuiModel.SelectedProcessEquipment;
            }
        }

        public BrewLoggerGuiController(Subject guiModel, IBrewLoggerGuiView guiView, ReportGenerator reportGenerator)
        {
            this.guiModel = guiModel;
            this.guiView = guiView;
            brewParametersGuiModel = (Models.BrewParametersGuiModel)guiModel;
            this.reportGenerator = reportGenerator;
        }

        public BrewLoggerGuiController()
        {

        }

        public void SetMashCopperHeatingUp1Temperature(string temperature)
        {

        }

        public void SetMashCopperHeatingUp2Temperature(string temperature)
        {

        }

        public void SetMashCopperProteinRestTemperature(string temperature)
        {

        }

        public void SetMashTunHeatingUpTemperature(string temperature)
        {

        }

        public void SetMashTunProteinRestTemperature(string temperature)
        {

        }

        public void SetMashTunSacharificationRestTemperature(string temperature)
        {

        }

        public void SetModel(Subject guiModel)
        {
            this.guiModel = guiModel;
        }

        public void SetView(IBrewLoggerGuiView guiView)
        {
            this.guiView = guiView;
        }

        public void SetWortCopperVolumeAfterBoiling(string volume)
        {

        }

        public void SetWortCopperVolumeBeforeBoiling(string volume)
        {

        }

        public void StartNewBrew(string startDate, string brandName, string brewNumber)
        {
            brewingProcessHandler.StartNewBrew(startDate, brandName, brewNumber);
        }

        public void SetProcessEquipment(string processEquipment)
        {
            brewParametersGuiModel.SetProcessEquipment(processEquipment);
        }

        public void SelectProcessEquipmentParameter(string processEquipment, string parameterName)
        {
            brewParametersGuiModel.SelectProcessEquipmentParameter(processEquipment, parameterName);
        }

        public void ChangeProcessEquipmentParameterValue(string processEquipment, string parameterName, string parameterValue)
        {
            brewParametersGuiModel.ChangeProcessEquipmentParameterValue(processEquipment, parameterName, parameterValue);
        }

        public void CreateReport(string year, string month, string reportName, string reportPath)
        {
            Month enumMonth = (Month)Enum.Parse(typeof(Month), month);
            reportGenerator.CreateReport(year, enumMonth, reportName, reportPath);
        }
    }
}
