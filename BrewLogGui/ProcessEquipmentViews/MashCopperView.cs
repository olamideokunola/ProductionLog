using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashCopperView : ProcessEquipmentView
    {
        private static string _myRunFileName = "mash_copper_running.png";
        private static string _myStopFileName = "mash_copper_idle.png";
        private static string _myProcessEquipment = "Mash Copper";

        public MashCopperView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
        }

        public MashCopperView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
