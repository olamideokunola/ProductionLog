using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class PumpView : ProcessEquipmentView
    {
        private static string _myRunFileName = "pump_running.png";
        private static string _myStopFileName = "pump_idle.png";

        public PumpView(string processEquipment) 
            : base(processEquipment, _myStopFileName)
        {
            _runFileName = _myRunFileName;
            _stopFileName = _myStopFileName;
            _processEquipment = processEquipment;
            DisplayBrew(false);
            DisplayState(false);
            DisplayBrand(false);
        }

    }
}
