using System;
using System.Windows.Forms;
using System.Drawing;

namespace BrewLogGui
{
    public class MyForm : Form
    {
        // Properties
        private Label label;
        private TextBox myName;
        private Button btn;

        public MyForm()
        {
            // Default Constructor
            Text = "Windows Forms app";

            this.Size = new Size(300, 350);
            render();
        }

        private void render() {
            label = new Label { Text = "Your name: ", Location = new Point(10, 35) };
            myName = new TextBox { Location = new Point(10, 60), Width = 150 };
            btn = new Button { Text = "Submit", Location = new Point(10, 100) };
            btn.Click += Btn_Click;

            //Attach these objects to the graphics window.
            this.Controls.Add(label);
            this.Controls.Add(myName);
            this.Controls.Add(btn);
        }

        // Handler
        void Btn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello, " + myName.Text + "!");
        }

    }
}
