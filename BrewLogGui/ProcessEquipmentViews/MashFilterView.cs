using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashFilterView : ProcessEquipmentView
    {
        private static string _myRunFileName = "mash_filter_running.png";
        private static string _myStopFileName = "mash_filter_idle.png";
        private static string _myProcessEquipment = "Mash Filter";

        public MashFilterView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
            ViewTop = 20;
        }

        public MashFilterView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
