using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class WortCopperView : ProcessEquipmentView
    {
        private static string _myRunFileName = "wort_copper_running.png";
        private static string _myStopFileName = "wort_copper_idle.png";
        private static string _myProcessEquipment = "Wort Copper";

        public WortCopperView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
        }

        public WortCopperView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
