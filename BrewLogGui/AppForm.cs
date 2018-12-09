using System;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui.ProcessEquipmentViews;
using BrewLogGui.Models;
using BrewLogGui.Controllers;

namespace BrewLogGui
{
    public class AppForm : Form, IBrewLoggerGuiView
    {
        private static string imagesFolderPath = "../../../BrewLogGui/Images/";
        private static double processViewScale = 0.4;

        //GUI elements
        private Button runBtn;
        private Button stopBtn;
        ProcessView processView;
        private BrewsListView brewsListView;

        // MVC Elements
        private IBrewLoggerGuiController guiController;

        public AppForm()
        {
            //Initialize picturebox static parameters
            ProcessViewPictureBox.ImagesFolderPath = imagesFolderPath;
            ProcessViewPictureBox.ProcessViewScale = processViewScale;

            // Default Constructor
            Text = "Windows Forms app";

            // MVC Controller
            guiController = new AppFormController();

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
            processView = ProcessView.GetInstance();
            //MyForm frm = new MyForm();

            //Position brewsListView
            brewsListView = new BrewsListView();
            brewsListView.SetBounds(300, 400, 300, 400);

            //Add members to Controls
            this.Controls.Add(processView);
            this.Controls.Add(runBtn);
            this.Controls.Add(stopBtn);
            this.Controls.Add(brewsListView);
        }

        void Btn_Click(object sender, EventArgs e)
        {

        }


        // Handler
        void RunBtn_Click(object sender, EventArgs e)
        {
            processView.WhirlpoolView.RunEquipment();
            //processView.WhirlpoolView.StartFlashing();
        }

        void StopBtn_Click(object sender, EventArgs e)
        {
            processView.WhirlpoolView.StopEquipment();
        }


        // Observer Interface Implementation
        public void Update(IBrewLoggerGuiModel guiModel)
        {

        }
    }
}
