using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class WortCoolerView : ProcessEquipmentView
    {
        private static string _myRunFileName = "wort_cooler_running.png";
        private static string _myStopFileName = "wort_cooler_idle.png";
        private static string _myProcessEquipment = "Wort Cooler";

        public WortCoolerView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
            DisplayBrew(false);
            DisplayState(false);
            DisplayBrand(false);
            ViewTop = 35;
        }

        public WortCoolerView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
