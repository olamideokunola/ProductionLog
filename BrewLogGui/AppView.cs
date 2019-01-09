using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrewLogGui
{
    public abstract class AppView : PictureBox, IGui
    {
        // attributes
        protected const int margin = 10;
        protected const int padding = 5;

        public new int Margin => margin;
        public new int Padding => padding;

        protected Label titleLabel = new Label();

        public Label TitleLabel
        {
            get
            {
                return titleLabel;
            }
        }

        public void Initialize()
        {
            SetupTitle();
            SetupView();
            Render();
            BackColor = Color.FromArgb(229, 229, 229);
            Show();
        }

        private void SetupTitle()
        {
            float currentSize = titleLabel.Font.Size;
            currentSize += 3.0F;
            titleLabel.Font = new Font(titleLabel.Font.Name, currentSize,
                                titleLabel.Font.Style, titleLabel.Font.Unit);
        }

        public abstract void Clear();

        public abstract void Render();

        public void SetPos(int x, int y)
        {
            this.Left = x;
            this.Top = y;
        }

        public abstract void SetupView();
    }
}
