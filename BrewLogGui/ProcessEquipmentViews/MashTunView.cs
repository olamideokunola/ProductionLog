using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashTunView : MashVesselView
    {
        private static string _myProcessEquipment = "Mash Tun";

        public MashTunView() : this(_myProcessEquipment)
        {
            _processEquipment = _myProcessEquipment;
        }

        public MashTunView(string processEquipment)
            : base(processEquipment)
        {

        }

    }
}
