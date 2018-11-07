using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashCoolerView : ProcessEquipmentView
    {
        private static string _myRunFileName = "mash_cooler_running.png";
        private static string _myStopFileName = "mash_cooler_idle.png";
        private static string _myProcessEquipment = "Mash Cooler";

        public MashCoolerView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
            DisplayBrew(false);
            DisplayState(false);
            DisplayBrand(false);
            ViewTop = 10;
        }

        public MashCoolerView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
