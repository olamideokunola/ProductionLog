using System;
using System.Windows.Forms;
using System.Drawing;
using BrewingModel.BrewingProcessEquipment;

namespace BrewLogGui.ProcessEquipmentViews
{
    public class ProcessEquipmentView : ProcessViewPictureBox
    {
        string _imageFileName;
        string _imgPathRel;
        protected string _runFileName = "";
        protected string _stopFileName = "";

        protected string _processEquipment;

        Label _nameLabel;
        Label _stateLabel;
        Label _brewNumberLabel;
        Label _brandNameLabel;

        string _brewNumber="";
        string _brandName="";
        string _currentStateText="Idle";

        int margin = 15;
        int lineSpacing = 15;
        protected int _top = 70;

        public string ImageFileName
        {
            get
            {
                return _imageFileName;
            }

            set
            {
                _imageFileName = value;
                _imgPathRel = GetImagePath(_imageFileName);
                Image = new Bitmap(_imgPathRel);
                SetUpProcessEquipmentView(_processEquipment, _imageFileName);
            }
        }

        public Label NameLabel
        {
            get
            {
                return _nameLabel;
            }
        }

        public Label StateLabel
        {
            get
            {
                return _stateLabel;
            }
        }

        public Label BrewNumberLabel
        {
            get
            {
                return _brewNumberLabel;
            }
        }

        public Label BrandNameLabel
        {
            get
            {
                return _brandNameLabel;
            }
        }

        public string BrewNumber
        {
            get
            {
                return _brewNumber;
            }
            set
            {
                _brewNumber = value;
            }
        }


        public string CurrentStateText
        {
            get
            {
                return _currentStateText;
            }
            set
            {
                CurrentStateText = value;
            }
        }


        public string BrandName
        {
            get
            {
                return _brandName;
            }
            set
            {
                _brandName = value;
            }
        }

        public int ViewTop
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
                Controls.Clear();
                SetUpProcessEquipmentView(_processEquipment, _imageFileName);
            }
        }

        //public int ViewTop { get => _top; set => _top = value; }

        public ProcessEquipmentView(string processEquipment,
                                    string imageFileName)
        {
            _processEquipment = processEquipment;
            SetUpProcessEquipmentView(_processEquipment, imageFileName);
        }

        public void SetUpProcessEquipmentView(string processEquipment, 
                                    string imageFileName)
        {
            _imageFileName = imageFileName;
            _imgPathRel = GetImagePath(imageFileName);

            Image = new Bitmap(_imgPathRel);

            ScaleImage(this);
            BackColor = Color.Transparent;

            _brewNumberLabel = new Label
            {
                Text = "Brew: " + _brewNumber,
                Location = new Point(margin, _top),
                //Location = AlignCenter(_brewNumberLabel),
                BackColor = Color.Transparent
            };
            _brewNumberLabel.AutoSize = true;
            AlignCenter(_brewNumberLabel);

            _brandNameLabel = new Label
            {
                Text = "Brand: " + _brandName,
                Location = new Point(margin, _brewNumberLabel.Top + lineSpacing),
                BackColor = Color.Transparent
            };
            _brandNameLabel.AutoSize = true;
            AlignCenter(_brandNameLabel);

            _stateLabel = new Label
            {
                Text = _currentStateText,
                Location = new Point(margin, _brandNameLabel.Top + lineSpacing),
                BackColor = Color.Transparent
            };
            _stateLabel.AutoSize = true;
            AlignCenter(_stateLabel);

            _nameLabel = new Label
            {
                Text = processEquipment,
                Location = new Point(margin, _stateLabel.Top + lineSpacing),
                BackColor = Color.Transparent,
            };
            _nameLabel.Font = new Font(_nameLabel.Font.FontFamily, 8, FontStyle.Bold);
            _nameLabel.AutoSize = true;
            AlignCenter(_nameLabel);

            Render();
            Show();
        }

        private void Render()
        {
            Controls.Add(_nameLabel);
            Controls.Add(_stateLabel);
            Controls.Add(_brandNameLabel);
            Controls.Add(_brewNumberLabel);
        }

        public void AlignCenter(Control control)
        {
            int x;
            int myWidth = this.Width;
            x = Convert.ToInt32((Image.Width - control.Width) / 2);
            control.Location = new Point(x,control.Top);
            control.Show();
        }

        public void AlignLeft(Control control)
        {
            control.Location = new Point(margin, control.Top);
            control.Show();
        }

        public void AlignRight(Control control)
        {
            int x;
            int myWidth = this.Width;
            x = Image.Width - control.Width - margin;
            control.Location = new Point(x, control.Top);
            control.Show();
        }

        private void AddLabelToControls(Label label)
        {
            if(!Controls.Contains(label))
            {
                Controls.Add(label);
            }
        }

        private void RemoveLabelFromControls(Label label)
        {
            if (Controls.Contains(label))
            {
                Controls.Remove(label);
            }
        }

        public void DisplayBrand(bool choice)
        {
            if(choice)
            {
                AddLabelToControls(_brandNameLabel);
            } 
            else
            {
                RemoveLabelFromControls(_brandNameLabel);
            }
        }

        public void DisplayBrew(bool choice)
        {
            if (choice)
            {
                AddLabelToControls(_brewNumberLabel);
            }
            else
            {
                RemoveLabelFromControls(_brewNumberLabel);
            }
        }

        public void DisplayState(bool choice)
        {
            if (choice)
            {
                AddLabelToControls(_stateLabel);
            }
            else
            {
                RemoveLabelFromControls(_stateLabel);
            }
        }

        public void RunEquipment()
        {
            ImageFileName = _runFileName;
            Show();
        }

        public void StopEquipment()
        {
            ImageFileName = _stopFileName;
            Show();
        }
    }
}
