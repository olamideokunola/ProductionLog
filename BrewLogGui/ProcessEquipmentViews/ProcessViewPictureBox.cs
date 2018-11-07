using System;
using System.Windows.Forms;
using System.Drawing;

namespace BrewLogGui.ProcessEquipmentViews
{
    public abstract class ProcessViewPictureBox : PictureBox
    {
        private static string imagesFolderPath;
        private static double processViewScale;

        public static string ImagesFolderPath
        {
            get
            {
                return imagesFolderPath;
            }

            set
            {
                imagesFolderPath = value;
            }
        }

        public static double ProcessViewScale
        {
            get
            {
                return processViewScale;
            }

            set
            {
                processViewScale = value;
            }
        }

        public static string GetImagePath(string imageName)
        {
            return imagesFolderPath + imageName;
        }

        protected static void ScaleImage(PictureBox pictureBox)
        {
            Image image = pictureBox.Image;
            Size newSize = new Size(Convert.ToInt32(image.Size.Width * processViewScale),
                                    Convert.ToInt32(image.Size.Height * processViewScale));
            pictureBox.Image = new Bitmap(image, newSize);
            pictureBox.Size = image.Size;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            //return image.Size;
        }
    }
}
