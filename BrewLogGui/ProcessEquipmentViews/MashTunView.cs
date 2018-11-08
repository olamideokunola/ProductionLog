using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashTunView : ProcessEquipmentView
    {
        private static string _myRunFileName = "mash_tun_running.png";
        private static string _myStopFileName = "mash_tun_idle.png";
        private static string _myProcessEquipment = "Mash Tun";

        public MashTunView() : this(_myProcessEquipment, _myStopFileName)
        {
            _processEquipment = _myProcessEquipment;
            _stopFileName = _myStopFileName;
            _runFileName = _myRunFileName;
            //
        }

        public MashTunView(string processEquipment, string stopFileName)
            : base(processEquipment, stopFileName)
        {

        }

    }
}
