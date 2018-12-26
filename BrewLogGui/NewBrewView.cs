using System;
using System.Windows.Forms;

namespace BrewLogGui
{
    public class NewBrewView : PictureBox
    {
        // GUI Controls
        private Label brandLabel = new Label();
        private Label dateLabel = new Label();
        private Label brewNumberLabel = new Label();

        private TextBox brandNameTextBox = new TextBox();
        private MaskedTextBox dateTextBox = new MaskedTextBox();
        private TextBox brewNumberTextBox = new TextBox();
        private Button startNewBrewButton = new Button();

        // GUI element getters
        public TextBox BrandNameTextBox
        {
            get
            {
                return brandNameTextBox;
            }
        }

        public MaskedTextBox DateTextBox
        {
            get
            {
                return dateTextBox;
            }
        }

        public TextBox BrewNumberTextBox
        {
            get
            {
                return brewNumberTextBox;
            }
        }

        public Button StartNewBrewButton
        {
            get
            {
                return startNewBrewButton;
            }
        }

        // attributes
        const int margin = 5;
        const int padding = 5;

        public NewBrewView()
        {
            SetupView();
            Render();
            Show();
        }

        private void SetupView()
        {
            // Set GUI controls dimensions & positions
            // Setup labels
            brandLabel.Text = "Brand:";
            brandLabel.SetBounds(
                margin,
                margin,
                100,
                20);

            dateLabel.Text = "Date:";
            dateLabel.SetBounds(
                margin,
                brandLabel.Top + brandLabel.Height + padding,
                100,
                20);

            brewNumberLabel.Text = "BrewNumber:";
            brewNumberLabel.SetBounds(
                margin,
                dateLabel.Top + dateLabel.Height + padding,
                100,
                20);

            // Setup textboxes
            brandNameTextBox.SetBounds(
                brandLabel.Left + brandLabel.Width + padding,
                brandLabel.Top,
                150,
                20);

            dateTextBox.SetBounds(
                dateLabel.Left + dateLabel.Width + padding,
                dateLabel.Top,
                150,
                20);

            brewNumberTextBox.SetBounds(
                brewNumberLabel.Left + brewNumberLabel.Width + padding,
                brewNumberLabel.Top,
                150,
                20);

            // Setup button
            startNewBrewButton.Text = "Start Brew";
            startNewBrewButton.SetBounds(
                brewNumberTextBox.Left,
                brewNumberTextBox.Top + brewNumberTextBox.Height + padding,
                150,
                20);

            // Setup width & height
            this.Width = margin + brandLabel.Width + padding + brandNameTextBox.Width + margin;
            this.Height = margin + brandNameTextBox.Height + padding + dateTextBox.Height + padding + brewNumberTextBox.Height + padding + startNewBrewButton.Height + margin;
        }

        private void Render()
        {
            // Add members to Controls
            Controls.Add(brandLabel);
            Controls.Add(dateLabel);
            Controls.Add(brewNumberLabel);
            Controls.Add(brandNameTextBox);
            Controls.Add(dateTextBox);
            Controls.Add(brewNumberTextBox);
            Controls.Add(startNewBrewButton);
        }

        public void SetPos(int x, int y)
        {
            this.Left = x;
            this.Top = y;
        }

        public void Clear()
        {
            brandNameTextBox.Text = "";
            dateTextBox.Text = "";
            brewNumberTextBox.Text = "";
        }

    }
}
