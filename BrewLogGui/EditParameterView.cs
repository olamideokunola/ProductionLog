using System;
using System.Windows.Forms;

namespace BrewLogGui
{
	public class EditParameterView : PictureBox
    {
        private Label parameter_name_label = new Label();
        private Label parameter_value_label = new Label();

        private TextBox parameterNameTextbox = new TextBox();
        private MaskedTextBox parameterValueTextbox = new MaskedTextBox();

        private Button submitButton = new Button();

        public Button SubmitButton
        {
            get
            {
                return submitButton;
            }
        }

        public TextBox ParameterNameTextbox
        {
            get
            {
                return parameterNameTextbox;
            }
        }

        public MaskedTextBox ParameterValueTextbox
        {
            get
            {
                return parameterValueTextbox;
            }
        }



        public EditParameterView()
        {
            SetupEditParameterView();
        }

        private void SetupEditParameterView()
        {
            // Setup label text
            parameter_name_label.Text = "Parameter:";
            parameter_name_label.SetBounds(
                0, 
                0, 
                100, 
                20);

            parameter_value_label.Text = "Value:";
            parameter_value_label.SetBounds(
                parameter_name_label.Left,
                parameter_name_label.Top + parameter_name_label.Height + 5, 
                100, 
                20);


            // Setup text boxes
            parameterNameTextbox.SetBounds(
                parameter_name_label.Width + 5,
                parameter_name_label.Top, 
                150, 
                20);

            parameterValueTextbox.SetBounds(
                parameterNameTextbox.Left,
                parameter_value_label.Top,
                150,
                20);

            // Setup button
            submitButton.Text = "Save";
            submitButton.SetBounds(
                parameterValueTextbox.Left,
                parameterValueTextbox.Top + parameterValueTextbox.Height + 5,
                150,
                20);

            Render();
            Show();
        }

        private void Render()
        {
            Controls.Add(parameter_name_label);
            Controls.Add(parameter_value_label);
            Controls.Add(parameterNameTextbox);
            Controls.Add(parameterValueTextbox);
            Controls.Add(submitButton);
        }
    }
}
