using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class WhirlpoolView : ProcessEquipmentView
    {
        private static string _myRunFileName = "whirlpool_running.png";
        private static string _myStopFileName = "whirlpool_idle.png";
        private static string _myProcessEquipment = "Whirlpool";

        public WhirlpoolView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
        }

        public WhirlpoolView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
