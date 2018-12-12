using System;
using System.Windows.Forms;

namespace BrewLogGui
{
	public class EditParameterView : PictureBox
    {
        private Label parameter_name_label = new Label();
        private Label parameter_value_label = new Label();

        private TextBox parameter_name_textbox = new TextBox();
        private TextBox parameter_value_textbox = new TextBox();

        private Button submit_button = new Button();

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
            parameter_name_textbox.SetBounds(
                parameter_name_label.Width + 5,
                parameter_name_label.Top, 
                100, 
                20);

            parameter_value_textbox.SetBounds(
                parameter_name_textbox.Left,
                parameter_value_label.Top,
                100,
                20);

            // Setup button
            submit_button.Text = "Submit";
            submit_button.SetBounds(
                parameter_value_textbox.Left,
                parameter_value_textbox.Top + parameter_value_textbox.Height + 5,
                100,
                20);

            Render();
            Show();
        }

        private void Render()
        {
            Controls.Add(parameter_name_label);
            Controls.Add(parameter_value_label);
            Controls.Add(parameter_name_textbox);
            Controls.Add(parameter_value_textbox);
            Controls.Add(submit_button);
        }
    }
}
