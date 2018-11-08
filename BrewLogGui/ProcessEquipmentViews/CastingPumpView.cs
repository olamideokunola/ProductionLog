using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class CastingPumpView : ProcessEquipmentView
    {
        private static string _myRunFileName = "casting_pump_running.png";
        private static string _myStopFileName = "casting_pump_idle.png";
        private static string _myProcessEquipment = "Casting Pump";

        public CastingPumpView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
        }

        public CastingPumpView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
