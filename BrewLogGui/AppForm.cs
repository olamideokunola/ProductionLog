using System;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui.ProcessEquipmentViews;

namespace BrewLogGui
{
    public class AppForm : Form
    {
        private static string imagesFolderPath = "../../../BrewLogGui/Images/";
        private static double processViewScale = 0.4;

        //GUI elements
        private Button runBtn;
        private Button stopBtn;
        ProcessView processView;

        public AppForm()
        {
            //Initialize picturebox static parameters
            ProcessViewPictureBox.ImagesFolderPath = imagesFolderPath;
            ProcessViewPictureBox.ProcessViewScale = processViewScale;

            // Default Constructor
            Text = "Windows Forms app";



            this.Size = new Size(1200, 600);
            render();
        }
        private void render()
        {
            runBtn = new Button { Text = "Run Mash Copper", Location = new Point(100, 525) };
            runBtn.Click += RunBtn_Click;

            stopBtn = new Button { Text = "Stop Mash Copper", Location = new Point(200, 525) };
            stopBtn.Click += StopBtn_Click;

            //Attach these objects to the graphics window.
            processView = new ProcessView();
            //MyForm frm = new MyForm();

            this.Controls.Add(processView);
            this.Controls.Add(runBtn);
            this.Controls.Add(stopBtn);
        }

        void Btn_Click(object sender, EventArgs e)
        {
        }


        // Handler
        void RunBtn_Click(object sender, EventArgs e)
        {
            processView.WhirlpoolView.RunEquipment();
        }

        void StopBtn_Click(object sender, EventArgs e)
        {
            processView.WhirlpoolView.StopEquipment();
        }

    }
}
