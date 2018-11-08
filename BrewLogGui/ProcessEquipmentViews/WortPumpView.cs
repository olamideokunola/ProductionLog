using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class WortPumpView : ProcessEquipmentView
    {
        private static string _myRunFileName = "wort_pump_running.png";
        private static string _myStopFileName = "wort_pump_idle.png";
        private static string _myProcessEquipment = "Wort Pump";

        public WortPumpView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
        }

        public WortPumpView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
