using System;
using System.Windows.Forms;
using System.Drawing;
using BrewingModel.BrewingProcessEquipment;

namespace BrewLogGui
{
    public class BrewsListView : PictureBox
    {
        private Label label = new Label();
        private ListView listView = new ListView();

        public ListView ListView
        {
            get
            {
                return listView;
            }
        }

        public BrewsListView()
        {
            SetupBrewsListView();
        }

        private void SetupBrewsListView()
        {
            // Setup label text
            label.Text = "Brews in process:";
            label.SetBounds(0, 0, this.Width, 20);

            // Setup listView
            listView.SetBounds(0, label.Height, this.Width, this.Height - label.Height);

            Render();
            Show();
        }

        public void SetBoundary(int x, int y, int width, int height)
        {
            this.SetBounds(x, y, width, height);
            listView.SetBounds(0, label.Height, this.Width, this.Height - label.Height);
            label.SetBounds(0, 0, this.Width, 20);
        }

        private void Render()
        {
            Controls.Add(label);
            Controls.Add(listView);
        }
    }
}
