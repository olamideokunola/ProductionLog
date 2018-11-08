using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashTransferPumpView : ProcessEquipmentView
    {
        private static string _myRunFileName = "pump_top_to_right_running.png";
        private static string _myStopFileName = "pump_top_to_right_idle.png";
        private static string _myProcessEquipment = "Mash Pump";

        public MashTransferPumpView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
        }

        public MashTransferPumpView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
