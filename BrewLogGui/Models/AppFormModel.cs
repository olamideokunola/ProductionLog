using System;
using BrewingModel;

namespace BrewLogGui.Models
{
	public class AppFormModel : IBrewLoggerGuiModel
    {
        private BrewingProcessHandler guiModel = BrewingProcessHandler.GetInstance();

        public AppFormModel()
        {
        }

        public void AddObserver(IBrewLoggerGuiView guiView)
        {
        }

        public void NotifyObservers()
        {
        }

        public void RemoveObserver(IBrewLoggerGuiView guiView)
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
