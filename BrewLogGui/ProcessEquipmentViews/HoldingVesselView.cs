using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class HoldingVesselView : ProcessEquipmentView
    {
        private static string _myRunFileName = "holding_vessel_running.png";
        private static string _myStopFileName = "holding_vessel_idle.png";
        private static string _myProcessEquipment = "Holding Vessel";

        public HoldingVesselView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
            ViewTop = 40;
        }

        public HoldingVesselView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
