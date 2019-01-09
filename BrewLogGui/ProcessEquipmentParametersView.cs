using System;
using System.Windows.Forms;
using System.Collections.Generic;

using System.Drawing;


namespace BrewLogGui
{
    public class ProcessEquipmentParametersView : AppView
    {
        private Label process_equipment_name_label = new Label();
        private Label process_parameters_label = new Label();

        private ListView processParametersListview = new ListView();
        private EditParameterView editParameterView = new EditParameterView();

        public ListView ProcessParametersListview
        {
            get
            {
                return processParametersListview;
            }
        }

        public EditParameterView EditParameterView
        {
            get
            {
                return editParameterView;
            }
        }

        // private IList<string> _defaultParameters = new List<string>();

        //const int margin = 5;
        //const int padding = 5;

        public ProcessEquipmentParametersView()
        {
            Initialize();
        }

        public override void SetupView()
        {
            // Setup label text
            TitleLabel.Text = "Selected Equipment:";
            TitleLabel.SetBounds(
                margin,
                margin,
                200,
                30);

            process_parameters_label.Text = "Process Parameters:";
            process_parameters_label.SetBounds(
                margin,
                TitleLabel.Top + TitleLabel.Height + padding,
                200,
                20);

            // Setup listview
            SetupProcessParametersListView();

            // Setup EditParameterView
            editParameterView.SetPos(
                processParametersListview.Left + processParametersListview.Width + padding,
                process_parameters_label.Top);
                //300,
                //200);

            // Setup width & height
            this.Width =
                margin +
                processParametersListview.Width + padding +
                editParameterView.Width;

            this.Height =
                margin +
                TitleLabel.Height + padding +
                process_parameters_label.Height + padding +
                processParametersListview.Height + margin;
        }

        private void SetupProcessParametersListView()
        {
            processParametersListview.Clear();
            processParametersListview.View = View.Details;
            processParametersListview.Columns.Add("Parameter", 150, HorizontalAlignment.Left);
            processParametersListview.Columns.Add("Value", 145, HorizontalAlignment.Left);
            processParametersListview.SetBounds(
                margin,
                process_parameters_label.Top + process_parameters_label.Height + padding,
                300,
                250);
        }

        public override void Render()
        {
            //Controls.Add(process_equipment_name_label);
            Controls.Add(TitleLabel);
            Controls.Add(process_parameters_label);
            Controls.Add(processParametersListview);
            Controls.Add(editParameterView);
        }

        public void UpdateParametersList(IList<string> parameterList, IDictionary<string, string> processParameters)
        {
            // Empty ListView
            SetupProcessParametersListView();

            // Create new parameters list
            IDictionary<string, string> newProcessParameters = CreateCompleteParameterList(parameterList, processParameters);

            // Add new process parameters and their values
            foreach (KeyValuePair<string, string> processParameter in newProcessParameters)
            {
                processParametersListview.Items.Add(processParameter.Key);
                int index = processParametersListview.Items.Count - 1;
                processParametersListview.Items[index].SubItems.Add(processParameter.Value);
            }
        }

        private IDictionary<string, string> CreateCompleteParameterList(IList<string> parameterNameList, IDictionary<string, string> processParameters)
        {
            IDictionary<string, string> newProcessParameters =
                new Dictionary<string, string>();

            foreach (string processParameterName in parameterNameList)
            {
                newProcessParameters.Add(processParameterName, "");
            }

            foreach (string processParameterName in parameterNameList)
            {
                if(processParameters.ContainsKey(processParameterName))
                {
                    if (processParameters[processParameterName].Length > 0)
                    {
                        newProcessParameters[processParameterName] = processParameters[processParameterName];
                    }
                }
            }
            return newProcessParameters;
        }

        public override void Clear()
        {

        }

    }
}
