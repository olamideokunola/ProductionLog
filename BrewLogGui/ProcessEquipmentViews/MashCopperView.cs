using System;
namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashCopperView : MashVesselView
    {
        private static string _myProcessEquipment = "Mash Copper";

        public MashCopperView() : this(_myProcessEquipment)
        {
            _processEquipment = _myProcessEquipment;
        }

        public MashCopperView(string processEquipment)
            : base(processEquipment)
        {

        }

    }
}
