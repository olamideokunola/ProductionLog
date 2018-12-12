using System;
using System.Windows.Forms;
using System.Collections.Generic;

using System.Drawing;


namespace BrewLogGui
{
    public class ProcessEquipmentParametersView : PictureBox
    {
        private Label process_equipment_name_label = new Label();
        private Label process_parameters_label = new Label();

        private ListView process_parameters_listview = new ListView();
        private EditParameterView edit_parameter_view = new EditParameterView();

        private IList<string> _defaultParameters = new List<string>();

        const int margin = 5;
        const int padding = 5;

        public ProcessEquipmentParametersView(IList<string> defaultParameters)
        {
            this._defaultParameters = defaultParameters;
            SetupProcessEquipmentParametersView();
        }

        private void SetupProcessEquipmentParametersView()
        {
            // Setup label text
            process_equipment_name_label.Text = "Process Equipment Name";
            process_equipment_name_label.SetBounds(
                margin,
                margin,
                200,
                30);

            process_parameters_label.Text = "Process Parameters:";
            process_parameters_label.SetBounds(
                margin,
                process_equipment_name_label.Top + process_equipment_name_label.Height + padding,
                200,
                20);

            // Setup listview
            SetupProcessParametersListView();

            // Setup EditParameterView
            edit_parameter_view.SetBounds(
                process_parameters_listview.Left + process_parameters_listview.Width + padding,
                process_parameters_listview.Top,
                200,
                200);

            Render();
            Show();
        }

        private void SetupProcessParametersListView()
        {
            process_parameters_listview.Clear();
            process_parameters_listview.View = View.Details;
            process_parameters_listview.Columns.Add("Parameter", 150, HorizontalAlignment.Left);
            process_parameters_listview.Columns.Add("Value", 145, HorizontalAlignment.Left);
            process_parameters_listview.SetBounds(
                margin,
                process_parameters_label.Top + process_parameters_label.Height + padding,
                300,
                100);

            PopulateWithProcessEquipmentParameters(_defaultParameters);
        }

        private void PopulateWithProcessEquipmentParameters(IList<string> defaultParameters)
        {
            foreach (string defaultParameter in defaultParameters)
            {
                process_parameters_listview.Items.Add(defaultParameter);
            }
        }

        private void Render()
        {
            Controls.Add(process_equipment_name_label);
            Controls.Add(process_parameters_label);
            Controls.Add(process_parameters_listview);
            Controls.Add(edit_parameter_view);
        }

        public void UpdateParametersList(IDictionary<string, string> processParameters)
        {
            // Empty ListView
            SetupProcessParametersListView();

            // Add new process parameters and their values
            foreach (KeyValuePair<string, string> processParameter in processParameters)
            {
                process_parameters_listview.Items.Add(processParameter.Key);
                int index = process_parameters_listview.Items.Count - 1;
                process_parameters_listview.Items[index].SubItems.Add(processParameter.Value);
            }
        }
    }
}
