using System;
using System.Collections.Generic;
using BrewingModel;
using BrewingModel.BrewingProcessEquipment;
using BrewLogGui;
using Observer;

namespace Models
{
    public class BrewParametersGuiModel : Subject, IBrewParametersGuiModel
    {
        private BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();

        // Process Equipment
        private string selectedProcessEquipment = "";
        private IDictionary<string, string> processEquipmentParameters = new Dictionary<string, string>();

        private string selectedProcessEquipmentParameterName = "";
        private string selectedProcessEquipmentParameterValue = "";

        public IDictionary<string, string> ProcessEquipmentParameters
        {
            get
            {
                return processEquipmentParameters;
            }
        }

        //Brews
        public IDictionary<string, Brew> Brews => brewingProcessHandler.Brews;

        public BrewParametersGuiModel()
        {

        }

        // IBrewLoggerGuiModel Interface Implementation
        public void SetMashCopperHeatingUp1Temperature(string temperature)
        {
            
        }

        public void SetMashCopperHeatingUp2Temperature(string temperature)
        {
            
        }

        public void SetMashCopperProteinRestTemperature(string temperature)
        {
            brewingProcessHandler.SetMashCopperProteinRestTemperature(temperature);
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
            brewingProcessHandler.StartNewBrew(startDate, brandName, brewNumber); 
        }

        // UI interface
        public void SetProcessEquipment(string processEquipment)
        {
            switch (processEquipment)
            {
                case "Mash Copper":
                    MashCopper mashCopper = brewingProcessHandler.MashCopper;
                    if (mashCopper.Brew.ProcessEquipmentParameters.ContainsKey(ProcessEquipment.MashCopper))
                    {
                        processEquipmentParameters = mashCopper.Brew.ProcessEquipmentParameters[ProcessEquipment.MashCopper];
                    }
                    break;
                case "Mash Tun":
                    MashTun mashTun = brewingProcessHandler.MashTun;
                    if (mashTun.Brew.ProcessEquipmentParameters.ContainsKey(ProcessEquipment.MashTun))
                    {
                        processEquipmentParameters = mashTun.Brew.ProcessEquipmentParameters[ProcessEquipment.MashTun];
                    }
                    break;
                case "Mash Filter":
                    MashFilter mashFilter = brewingProcessHandler.MashFilter;
                    if (mashFilter.Brew.ProcessEquipmentParameters.ContainsKey(ProcessEquipment.MashFilter))
                    {
                        processEquipmentParameters = mashFilter.Brew.ProcessEquipmentParameters[ProcessEquipment.MashFilter];
                    }
                    break;
                case "Holding Vessel":
                    HoldingVessel holdingVessel = brewingProcessHandler.HoldingVessel;
                    processEquipmentParameters = holdingVessel.Brew.ProcessEquipmentParameters[ProcessEquipment.HoldingVessel];
                    if (holdingVessel.Brew.ProcessEquipmentParameters.ContainsKey(ProcessEquipment.HoldingVessel))
                    {
                        processEquipmentParameters = holdingVessel.Brew.ProcessEquipmentParameters[ProcessEquipment.HoldingVessel];
                    }
                    break;
                case "Wort Copper":
                    WortCopper wortCopper = brewingProcessHandler.WortCopper;
                    if (wortCopper.Brew.ProcessEquipmentParameters.ContainsKey(ProcessEquipment.WortCopper))
                    {
                        processEquipmentParameters = wortCopper.Brew.ProcessEquipmentParameters[ProcessEquipment.WortCopper];
                    }
                    break;
                case "Whirlpool":
                    Whirlpool whirlpool = brewingProcessHandler.Whirlpool;
                    if (whirlpool.Brew.ProcessEquipmentParameters.ContainsKey(ProcessEquipment.Whirlpool))
                    {
                        processEquipmentParameters = whirlpool.Brew.ProcessEquipmentParameters[ProcessEquipment.Whirlpool];
                    }
                    break;
            }
            selectedProcessEquipment = processEquipment;
            Notify();
        }

        // UI Process Equipment methods
        public void SelectProcessEquipmentParameter(string processEquipment, string parameterName)
        {
            switch (processEquipment)
            {
                case "Mash Copper":
                    MashCopper mashCopper = brewingProcessHandler.MashCopper;
                    selectedProcessEquipmentParameterValue =
                        mashCopper.Brew.GetProcessParameterValue(ProcessEquipment.MashCopper, parameterName);
                    break;
                case "Mash Tun":
                    MashTun mashTun = brewingProcessHandler.MashTun;
                    selectedProcessEquipmentParameterValue =
                        mashTun.Brew.GetProcessParameterValue(ProcessEquipment.MashTun, parameterName);
                    break;
                case "Mash Filter":
                    MashFilter mashFilter = brewingProcessHandler.MashFilter;
                    selectedProcessEquipmentParameterValue =
                        mashFilter.Brew.GetProcessParameterValue(ProcessEquipment.MashFilter, parameterName);
                    break;
                case "Holding Vessel":
                    HoldingVessel holdingVessel = brewingProcessHandler.HoldingVessel;
                    selectedProcessEquipmentParameterValue =
                        holdingVessel.Brew.GetProcessParameterValue(ProcessEquipment.HoldingVessel, parameterName);
                    break;
                case "Wort Copper":
                    WortCopper wortCopper = brewingProcessHandler.WortCopper;
                    selectedProcessEquipmentParameterValue =
                        wortCopper.Brew.GetProcessParameterValue(ProcessEquipment.WortCopper, parameterName);
                    break;
                case "Whirlpool":
                    Whirlpool whirlpool = brewingProcessHandler.Whirlpool;
                    selectedProcessEquipmentParameterValue =
                        whirlpool.Brew.GetProcessParameterValue(ProcessEquipment.Whirlpool, parameterName);
                    break;
            }
            selectedProcessEquipmentParameterName = parameterName;
            Notify();
        }

        public void ChangeProcessEquipmentParameterValue(string processEquipment, string parameterName, string parameterValue)
        {
            switch (processEquipment)
            {
                case "Mash Copper":
                    MashCopper mashCopper = brewingProcessHandler.MashCopper;
                    mashCopper.Brew.SetProcessParameterValue(ProcessEquipment.MashCopper, parameterName, parameterValue);
                    break;
                case "Mash Tun":
                    MashTun mashTun = brewingProcessHandler.MashTun;
                    mashTun.Brew.SetProcessParameterValue(ProcessEquipment.MashTun, parameterName, parameterValue); 
                    break;
                case "Mash Filter":
                    MashFilter mashFilter = brewingProcessHandler.MashFilter;
                    mashFilter.Brew.SetProcessParameterValue(ProcessEquipment.MashFilter, parameterName, parameterValue);
                    break;
                case "Holding Vessel":
                    HoldingVessel holdingVessel = brewingProcessHandler.HoldingVessel;
                    holdingVessel.Brew.SetProcessParameterValue(ProcessEquipment.HoldingVessel, parameterName, parameterValue);
                    break;
                case "Wort Copper":
                    WortCopper wortCopper = brewingProcessHandler.WortCopper;
                    wortCopper.Brew.SetProcessParameterValue(ProcessEquipment.WortCopper, parameterName, parameterValue);
                    break;
                case "Whirlpool":
                    Whirlpool whirlpool = brewingProcessHandler.Whirlpool;
                    whirlpool.Brew.SetProcessParameterValue(ProcessEquipment.Whirlpool, parameterName, parameterValue);
                    break;
            }
            Notify();
        }
    }
}