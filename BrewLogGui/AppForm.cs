using System;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui.ProcessEquipmentViews;
using BrewLogGui.Controllers;
using System.Collections.Generic;
using BrewingModel;
using Observer;
using Models;

namespace BrewLogGui
{
    public class AppForm : Form, IBrewLoggerGuiView, IObserver
    {
        private static string imagesFolderPath = "../../../BrewLogGui/Images/";
        private static double processViewScale = 0.4;
        const int margin = 5;
        const int padding = 5;

        //GUI elements
        private Button runBtn;
        private Button stopBtn;
        private ProcessView processView;
        private BrewsListView brewsListView = new BrewsListView();
        private ProcessEquipmentParametersView processEquipmentParametersView = 
            new ProcessEquipmentParametersView(new List<string>());

        // MVC Elements
        // BrewingProcessHandler MVC elements
        private BrewingProcessHandler brewingProcessHandler;
        private Subject guiModel;

        // BrewParametersGuiModel MVC elements
        private BrewParametersGuiModel brewParametersGuiModel;
        private Subject guiProcessParametersModel;
        private IBrewLoggerGuiController guiController;

        //private IProcessViewGuiModel processViewModel;

        public AppForm()
        {
            //Initialize picturebox static parameters
            ProcessViewPictureBox.ImagesFolderPath = imagesFolderPath;
            ProcessViewPictureBox.ProcessViewScale = processViewScale;

            // Default Constructor
            Text = "Windows Forms app";

            // Model & Controller for process parameters views
            brewingProcessHandler = BrewingProcessHandler.GetInstance();
            guiModel = brewingProcessHandler;
            // Register with BrewingProcessHandler Subject
            guiModel.Attach(this);

            brewParametersGuiModel = new BrewParametersGuiModel();
            guiProcessParametersModel = brewParametersGuiModel;
            guiController = new BrewLoggerGuiController(guiProcessParametersModel, this);
            // Register with BrewParametersGuiModel Subject
            guiProcessParametersModel.Attach(this);

            // Model for process equipment view
            //processViewModel = new ProcessViewModel();
            //processViewModel.AddObserver(this);

            this.Size = new Size(1200, 600);

            //Setup GUI elements
            SetupProcessView();
            SetupBrewsListView();
            SetupProcessEquipmentParametersView();

            render();
        }

        private void SetupProcessView()
        {
            //Attach these objects to the graphics window.
            processView = ProcessView.GetInstance();

            // Set Click Hander for all Process Equipment views
            processView.MashCopperView.Click += MashCopperView_Click;
            processView.MashTunView.Click += MashTunView_Click;
            processView.MashFilterView.Click += MashFilterView_Click;
            processView.HoldingVesselView.Click += HoldingVesselView_Click;
            processView.WortCopperView.Click += WortCopperView_Click;
            processView.WhirlpoolView.Click += WhirlpoolView_Click;
        }

        private void SetupBrewsListView()
        {
            //Position and set size of brewsListView
            brewsListView.SetBoundary(
                processView.Left + 300,
                500, 
                150, 
                150);

            ListView listView = brewsListView.ListView;

            // Load BrewsListData
            foreach (KeyValuePair<string, Brew> brew in brewingProcessHandler.Brews)
            {
                listView.Items.Add(brew.Key);
            }
        }

        private void SetupProcessEquipmentParametersView()
        {
            processEquipmentParametersView.SetBounds(
                brewsListView.Left + brewsListView.Width + padding,
                brewsListView.Top,
                500,
                200);
        }

        private void render()
        {
            runBtn = new Button { Text = "Run Mash Copper", Location = new Point(processView.Left, 500) };
            runBtn.Click += RunBtn_Click;

            stopBtn = new Button { Text = "Stop Mash Copper", Location = new Point(processView.Left, runBtn.Top + runBtn.Height + padding) };
            stopBtn.Click += StopBtn_Click;

            //Add members to Controls
            Controls.Add(processView);
            Controls.Add(runBtn);
            Controls.Add(stopBtn);
            Controls.Add(brewsListView);
            Controls.Add(processEquipmentParametersView);
        }


        // Click Events for Process Equipment Views 
        void WhirlpoolView_Click(object sender, EventArgs e)
        {
            guiController.SetProcessEquipment("Whirpool");
        }


        void WortCopperView_Click(object sender, EventArgs e)
        {
            guiController.SetProcessEquipment("Wort Copper");
        }


        void HoldingVesselView_Click(object sender, EventArgs e)
        {
            guiController.SetProcessEquipment("Holding Vessel");
        }


        void MashFilterView_Click(object sender, EventArgs e)
        {
            guiController.SetProcessEquipment("Mash Filter");
        }


        void MashTunView_Click(object sender, EventArgs e)
        {
            guiController.SetProcessEquipment("Mash Tun");
        }


        void MashCopperView_Click(object sender, EventArgs e)
        {
            guiController.SetProcessEquipment("Mash Copper");
        }

        private void SetupBrewParametersView()
        {

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
        public void Update(Subject changedSubject)
        {
            // For brewProcessHandler
            if (changedSubject.Equals(brewingProcessHandler))
            {
                UpdateMashCopperView();
            }

            // For brewParametersGuiModel portion of the view
            if (changedSubject.Equals(brewParametersGuiModel))
            {
                UpdateParameterListBox();
            }

        }

        private void UpdateParameterListBox()
        {
            processEquipmentParametersView.UpdateParametersList(
                brewParametersGuiModel.ProcessEquipmentParameters);
        }

        private void UpdateMashCopperView()
        {
            processView.MashCopperView.BrewNumber =
                brewingProcessHandler.MashCopper.Brew.BrewNumber;
            processView.MashCopperView.BrandName =
                brewingProcessHandler.MashCopper.Brew.BrandName;
            processView.MashCopperView.CurrentStateText =
                brewingProcessHandler.MashCopper.CurrentStateString();

            // Stop or Run the equipment
            if (brewingProcessHandler.MashCopper.CurrentState == brewingProcessHandler.MashCopper.IdleState)
            {
                processView.MashCopperView.StopEquipment();
            } else {
                processView.MashCopperView.RunEquipment();
            }
        }

}
}
