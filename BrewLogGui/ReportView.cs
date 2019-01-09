using System;
using System.Windows.Forms;

namespace BrewLogGui
{
    public class ReportView : AppView
    {
        // Declare GUI elements
        Label yearLabel = new Label();
        Label monthLabel = new Label();
        Label locationLabel = new Label();
        Label reportNameLabel = new Label();

        MaskedTextBox yearTextBox = new MaskedTextBox();
        MaskedTextBox monthTextBox = new MaskedTextBox();
        TextBox locationTextBox = new TextBox();
        TextBox reportNameTextBox = new TextBox();

        Button setLocationButton = new Button();
        Button createReportButton = new Button();

        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

        // Getters
        public MaskedTextBox YearTextBox
        {
            get
            {
                return yearTextBox;
            }
        }

        public MaskedTextBox MonthTextBox
        {
            get
            {
                return monthTextBox;
            }
        }


        public TextBox LocationTextBox
        {
            get
            {
                return locationTextBox;
            }
        }


        public TextBox ReportNameTextBox
        {
            get
            {
                return reportNameTextBox;
            }
        }

        public Button SetLocationButton
        {
            get
            {
                return setLocationButton;
            }
        }

        public Button CreateReportButton
        {
            get
            {
                return createReportButton;
            }
        }

        public FolderBrowserDialog FolderBrowser
        {
            get
            {
                return folderBrowser;
            }
        }

        // Constructor
        public ReportView()
        {
            Initialize();
        }

        // Methods
        public override void SetupView()
        {
            // Set GUI controls dimensions & positions
            // Setup labels
            TitleLabel.Text = "Create Report:";
            TitleLabel.SetBounds(
                margin,
                margin,
                150,
                30);

            yearLabel.Text = "Year:";
            yearLabel.SetBounds(
                margin,
                margin + TitleLabel.Height + padding,
                100,
                20);

            monthLabel.Text = "Month:";
            monthLabel.SetBounds(
                margin,
                yearLabel.Top + yearLabel.Height + padding,
                100,
                20);

            locationLabel.Text = "Location:";
            locationLabel.SetBounds(
                margin,
                monthLabel.Top + monthLabel.Height + padding,
                100,
                20);

            reportNameLabel.Text = "ReportName:";
            reportNameLabel.SetBounds(
                margin,
                locationLabel.Top + locationLabel.Height + padding,
                100,
                20);

            // Setup textboxes
            yearTextBox.SetBounds(
                yearLabel.Left + yearLabel.Width + padding,
                yearLabel.Top,
                50,
                20);

            monthTextBox.SetBounds(
                monthLabel.Left + monthLabel.Width + padding,
                monthLabel.Top,
                100,
                20);

            locationTextBox.SetBounds(
                locationLabel.Left + locationLabel.Width + padding,
                locationLabel.Top,
                150,
                20);

            reportNameTextBox.SetBounds(
                reportNameLabel.Left + reportNameLabel.Width + padding,
                reportNameLabel.Top,
                150,
                20);

            //Setup buttons
            setLocationButton.Text = "Set";
            setLocationButton.SetBounds(
                locationTextBox.Left + locationTextBox.Width + padding,
                locationTextBox.Top,
                50,
                20);

            createReportButton.Text = "Create";
            createReportButton.SetBounds(
                reportNameTextBox.Left,
                reportNameTextBox.Top + reportNameTextBox.Height + padding,
                150,
                20);

            //Setup Folder Dialog
            folderBrowser.Description = "Select the Directory where you want to save the report.";

            // Setup width & height
            this.Width = 
                margin + 
                locationLabel.Width + padding + 
                locationTextBox.Width + padding +
                setLocationButton.Width + margin;

            this.Height = 
                margin +
                TitleLabel.Height + padding +
                yearTextBox.Height + padding + 
                monthTextBox.Height + padding + 
                locationTextBox.Height + padding +
                reportNameTextBox.Height + padding +
                createReportButton.Height + margin;

        }

        public override void Render()
        {
            // Add members to Controls
            Controls.Add(titleLabel);
            Controls.Add(yearLabel);
            Controls.Add(monthLabel);
            Controls.Add(locationLabel);
            Controls.Add(reportNameLabel);
            Controls.Add(yearTextBox);
            Controls.Add(monthTextBox);
            Controls.Add(locationTextBox);
            Controls.Add(reportNameTextBox);
            Controls.Add(setLocationButton);
            Controls.Add(createReportButton);
        }

        public override void Clear()
        {
            yearTextBox.Text = "";
            monthTextBox.Text = "";
            locationTextBox.Text = "";
            reportNameTextBox.Text = "";
        }
    }
}
