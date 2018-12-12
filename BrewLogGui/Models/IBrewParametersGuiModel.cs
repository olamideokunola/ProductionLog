using System;
using System.Collections.Generic;
using BrewingModel;

namespace Models
{
    public interface IBrewParametersGuiModel 
    {
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

        // UI interface
        void SetProcessEquipment(string processEquipment);

        // Process Equipment Parameter methods
        void SelectProcessEquipmentParameter(string processEquipment, string parameterName);
        void ChangeProcessEquipmentParameterValue(string processEquipment, string parameterName, string parameterValue);

        IDictionary<string, string> ProcessEquipmentParameters { get; }
        //IDictionary<string, Brew> Brews { get; }
    }
}
