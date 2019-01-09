using System;
using System.Windows.Forms;
using System.Drawing;
using BrewingModel.BrewingProcessEquipment;

namespace BrewLogGui
{
    public class BrewsListView : AppView
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
            Initialize();
        }

        public void SetListViewBoundary(int x, int y, int width, int height)
        {
            this.SetPos(x, y);
            label.SetBounds(margin, margin, this.Width, 20);
            listView.SetBounds(margin, label.Top + label.Height, width, height);

            // Setup width & height
            this.Width =
                margin +
                listView.Width + margin;

            this.Height =
                margin +
                label.Height + padding +
                listView.Height + margin;
        }

        public override void Clear()
        {
        }

        public override void Render()
        {
            Controls.Add(label);
            Controls.Add(listView);
        }

        public override void SetupView()
        {
            // Setup label text
            label.Text = "Brews in process:";
            label.SetBounds(Margin, Margin, 150, 20);

            // Setup listView
            listView.SetBounds(Margin, label.Top + label.Height, 255, 100 - label.Height);

            // Setup width & height
            this.Width =
                margin +
                listView.Width + padding;

            this.Height =
                margin +
                label.Height + padding +
                listView.Height + margin;
        }
    }
}
