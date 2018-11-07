using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashVesselView : ProcessEquipmentView
    {
        private static string _myRunFileName = "mash_vessel_running.png";
        private static string _myStopFileName = "mash_vessel_idle.png";

        public MashVesselView(string processEquipment) 
            : base(processEquipment, _myStopFileName)
        {
            _runFileName = _myRunFileName;
            _stopFileName = _myStopFileName;
            _processEquipment = processEquipment;
        }

    }
}
