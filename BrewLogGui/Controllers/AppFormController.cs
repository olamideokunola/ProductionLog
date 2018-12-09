using System;
using BrewLogGui.Models;

namespace BrewLogGui.Controllers
{
	public class AppFormController : IBrewLoggerGuiController
    {
        private IBrewLoggerGuiModel _guiModel;
        private IBrewLoggerGuiView _guiView;

        public AppFormController(IBrewLoggerGuiModel guiModel, IBrewLoggerGuiView guiView)
        {
            this._guiModel = guiModel;
            this._guiView = guiView;
        }

        public AppFormController()
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

        public void SetModel(IBrewLoggerGuiModel guiModel)
        {
            this._guiModel = guiModel;
        }

        public void SetView(IBrewLoggerGuiView guiView)
        {
            this._guiView = guiView;
        }

        public void SetWortCopperVolumeAfterBoiling(string volume)
        {

        }

        public void SetWortCopperVolumeBeforeBoiling(string volume)
        {

        }

        public void StartNewBrew(string startDate, string brandName, string brewNumber)
        {

        }
    }
}
