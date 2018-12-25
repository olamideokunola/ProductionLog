using System;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui.ProcessEquipmentViews;
using BrewLogGui.Controllers;
using System.Collections.Generic;
using BrewingModel;
using Observer;
using Models;
using ProcessEquipmentParameters;

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
            new ProcessEquipmentParametersView();

        // MVC Elements
        // BrewingProcessHandler MVC elements
        private BrewingProcessHandler brewingProcessHandler;
        private Subject guiModel;

        // BrewParametersGuiModel MVC elements
        private BrewParametersGuiModel brewParametersGuiModel;
        private Subject guiProcessParametersModel;
        private IBrewLoggerGuiController guiController;

        private string _selectedParameterName = "";

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
                700,
                200);

            ListView parametersListView = processEquipmentParametersView.ProcessParametersListview;
            parametersListView.Activation = ItemActivation.OneClick;
            parametersListView.Click += ParametersListView_ItemActivate;

            Button submitButton = processEquipmentParametersView.EditParameterView.SubmitButton;
            submitButton.Click += SubmitButton_Click;
        }

        void SubmitButton_Click(object sender, EventArgs e)
        {
            MaskedTextBox maskedTextBox = processEquipmentParametersView.EditParameterView.ParameterValueTextbox;
            string processEquipment = guiController.ProcessEquipment;
            string parameterName = brewParametersGuiModel.SelectedProcessEquipmentParameterName;
            string parameterValue = maskedTextBox.Text;
            guiController.ChangeProcessEquipmentParameterValue(
                processEquipment,
                parameterName,
                parameterValue);
        }

        private void ParametersListView_ItemActivate(object sender, EventArgs e)
        {
            string processEquipment = guiController.ProcessEquipment;

            ListView parametersListView = processEquipmentParametersView.ProcessParametersListview;
            ListView.SelectedListViewItemCollection parameters = parametersListView.SelectedItems;
            string selectedParameterName = parameters[0].Text;
            guiController.SelectProcessEquipmentParameter(processEquipment, selectedParameterName);

            // Set mask for text box in case of edit
            SetTextBoxMask(selectedParameterName);
            SetButtonState(selectedParameterName);
        }

        private void SetButtonState(string parameterName)
        {
            string parameterType = GetParameterType(parameterName);
            Button submitButton = processEquipmentParametersView.EditParameterView.SubmitButton;
            switch (parameterType)
            {
                case "Time":
                    submitButton.Enabled = false;
                    break;
                case "Temperature":
                    submitButton.Enabled = true;
                    break;
            }
        }

        private void SetTextBoxMask(string parameterName)
        {
            string parameterType = GetParameterType(parameterName);
            MaskedTextBox maskedTextBox = processEquipmentParametersView.EditParameterView.ParameterValueTextbox;
            switch (parameterType)
            {
                case "Time":
                    //maskedTextBox.Mask = "00/00/0000";
                    maskedTextBox.Mask = "";
                    break;
                case "Temperature":
                    maskedTextBox.Mask = "000";
                    break;
            }
        }

        private string GetParameterType(string parameterName)
        {
            string parameterType = "";

            if (parameterName.EndsWith("Time") || parameterName.EndsWith("At"))
            {
                parameterType = "Time";
            }
            else if(parameterName.EndsWith("Temperature"))
            {
                parameterType = "Temperature";
            }

            return parameterType;
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
                UpdateSelectedParameterView();
            }

        }

        private void UpdateSelectedParameterView()
        {
            string selectedParameterName = brewParametersGuiModel.SelectedProcessEquipmentParameterName;
            string selectedParameterValue = brewParametersGuiModel.SelectedProcessEquipmentParameterValue;

            if (selectedParameterName.Length > 0 && _selectedParameterName != selectedParameterName)
            {
                EditParameterView editParameterView = processEquipmentParametersView.EditParameterView;
                editParameterView.ParameterNameTextbox.Text = selectedParameterName;
                editParameterView.ParameterValueTextbox.Text = selectedParameterValue;
                editParameterView.Show();
            }
        }

        private void UpdateParameterListBox()
        {
            IList<string> completeParameterList = new List<string>();

            completeParameterList = GetEquipmentParameters(guiController.ProcessEquipment);

            processEquipmentParametersView.UpdateParametersList(
                completeParameterList,
                brewParametersGuiModel.ProcessEquipmentParameters);
        }

        private IList<string> GetEquipmentParameters(string processEquipment)
        {
            IList<string> processParameters = new List<string>();

            switch (processEquipment)
            {
                case "Mash Copper":
                    processParameters.Add(MashCopperProcessParameters.MashingInStartTime.ToString());
                    processParameters.Add(MashCopperProcessParameters.MashingInEndTime.ToString());
                    processParameters.Add(MashCopperProcessParameters.ProteinRestEndTime.ToString());
                    processParameters.Add(MashCopperProcessParameters.ProteinRestTemperature.ToString());
                    processParameters.Add(MashCopperProcessParameters.HeatingUp1EndTime.ToString());
                    processParameters.Add(MashCopperProcessParameters.HeatingUp1Temperature.ToString());
                    processParameters.Add(MashCopperProcessParameters.HeatingUp2EndTime.ToString());
                    processParameters.Add(MashCopperProcessParameters.HeatingUp2Temperature.ToString());
                    processParameters.Add(MashCopperProcessParameters.Rest1EndTime.ToString());
                    processParameters.Add(MashCopperProcessParameters.Rest2EndTime.ToString());
                    processParameters.Add(MashCopperProcessParameters.TransferToMtEndTime.ToString());
                    break;
                case "Mash Tun":
                    processParameters.Add(MashTunProcessParameters.MashingInStartTime.ToString());
                    processParameters.Add(MashTunProcessParameters.MashingInEndTime.ToString());
                    processParameters.Add(MashTunProcessParameters.ProteinRestEndTime.ToString());
                    processParameters.Add(MashTunProcessParameters.ProteinRestTemperature.ToString());
                    processParameters.Add(MashTunProcessParameters.HeatingUpEndTime.ToString());
                    processParameters.Add(MashTunProcessParameters.HeatingUpTemperature.ToString());
                    processParameters.Add(MashTunProcessParameters.SacharificationRestEndTime.ToString());
                    processParameters.Add(MashTunProcessParameters.SacharificationRestTemperature.ToString());
                    processParameters.Add(MashTunProcessParameters.MashTunReadyAt.ToString());
                    break;
                case "Mash Filter":
                    processParameters.Add(MashFilterProcessParameters.PrefillingStartTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.PrefillingEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.WeakWortTransferEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.MainMashFiltrationEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.SpargingEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.SpargingToWWTEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.ExtraSpargingEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.DrippingEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.SpentGrainDischargeEndTime.ToString());
                    processParameters.Add(MashFilterProcessParameters.ReadyAtTime.ToString());
                    break;
                case "Holding Vessel":
                    processParameters.Add(HoldingVesselProcessParameters.FillingStartTime.ToString());
                    processParameters.Add(HoldingVesselProcessParameters.TransferToWcEndTime.ToString());
                    processParameters.Add(HoldingVesselProcessParameters.EmptyAtTime.ToString());
                    break;
                case "Wort Copper":
                    processParameters.Add(WortCopperProcessParameters.HeatingStartTime.ToString());
                    processParameters.Add(WortCopperProcessParameters.HeatingEndTime.ToString());
                    processParameters.Add(WortCopperProcessParameters.BoilingEndTime.ToString());
                    processParameters.Add(WortCopperProcessParameters.ExtraBoilingEndTime.ToString());
                    processParameters.Add(WortCopperProcessParameters.CastingStartTime.ToString());
                    processParameters.Add(WortCopperProcessParameters.CastingEndTime.ToString());
                    processParameters.Add(WortCopperProcessParameters.VolumeBeforeBoiling.ToString());
                    processParameters.Add(WortCopperProcessParameters.VolumeAfterBoiling.ToString());
                    break;
                case "Whirlpool":
                    processParameters.Add(WhirlpoolProcessParameters.CastingStartTime.ToString());
                    processParameters.Add(WhirlpoolProcessParameters.CastingEndTime.ToString());
                    processParameters.Add(WhirlpoolProcessParameters.RestingEndTime.ToString());
                    processParameters.Add(WhirlpoolProcessParameters.CoolingEndTime.ToString());
                    processParameters.Add(WhirlpoolProcessParameters.ReadyAtTime.ToString());
                    break;
            }
            return processParameters;
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
