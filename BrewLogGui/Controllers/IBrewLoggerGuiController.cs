using System;
using BrewLogGui.Models;

namespace BrewLogGui.Controllers
{
    public interface IBrewLoggerGuiController
    {
        // Set Model
        void SetModel(IBrewLoggerGuiModel guiModel);
        void SetView(IBrewLoggerGuiView guiView);

        void StartNewBrew(string startDate, string brandName, string brewNumber);

        // Mash Copper
        void SetMashCopperProteinRestTemperature(string temperature);
        void SetMashCopperHeatingUp1Temperature(string temperature);
        void SetMashCopperHeatingUp2Temperature(string temperature);

        // Mash Tun
        void SetMashTunProteinRestTemperature(string temperature);
        void SetMashTunSacharificationRestTemperature(string temperature);
        void SetMashTunHeatingUpTemperature(string temperature);

        // Wort Copper
        void SetWortCopperVolumeBeforeBoiling(string volume);
        void SetWortCopperVolumeAfterBoiling(string volume);
    }
}
