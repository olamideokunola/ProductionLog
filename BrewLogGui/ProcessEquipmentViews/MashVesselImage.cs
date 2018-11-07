using System;
using System.Windows.Forms;
using System.Drawing;

namespace BrewLogGui.ProcessEquipmentViews
{
    public class MashVesselImage : ProcessViewPictureBox
    {
        private static String imgPathRel = GetImagePath("mash_vessel_idle.png");

        private static MashVesselImage _uniqueInstance = null;

        //lazy construction of instance
        public static MashVesselImage GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new MashVesselImage();
            }

            return _uniqueInstance;
        }

        //hidden constructer to allow Singleton
        private MashVesselImage()
        {
            Image = new Bitmap(imgPathRel);
            ScaleImage(this);
            Show();
        }
    }
}
