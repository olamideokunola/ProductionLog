using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
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

        public string ProcessEquipment
        {
            get
            {
                return _processEquipment;
            }
        }

        public string RunFileName
        {
            get
            {
                return _runFileName;
            }
        }

        public string StopFileName
        {
            get
            {
                return _stopFileName;
            }
        }

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
                SetUpProcessEquipmentView();
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
                RefreshBrewNumber();
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
                _currentStateText = value;
                RefreshCurrentStateText();
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
                RefreshBrandName();
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

                SetUpProcessEquipmentView();
            }
        }

        //public int ViewTop { get => _top; set => _top = value; }

        public ProcessEquipmentView(string processEquipment,
                                    string imageFileName)
        {
            _imageFileName = imageFileName;
            _processEquipment = processEquipment;
            SetUpProcessEquipmentView(); //_processEquipment, imageFileName);
        }

        public void SetUpProcessEquipmentView()
        {
            //_imageFileName = imageFileName;
            _imgPathRel = GetImagePath(_imageFileName);

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
                Text = _processEquipment,
                Location = new Point(margin, _stateLabel.Top + lineSpacing),
                BackColor = Color.Transparent,
            };
            _nameLabel.Font = new Font(_nameLabel.Font.FontFamily, 8, FontStyle.Bold);
            _nameLabel.AutoSize = true;
            AlignCenter(_nameLabel);

            if (Controls.Count > 0){ Controls.Clear(); }
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

        // Delegate for Thread safe call 
        delegate void ReturningVoidDelegate();

        // Thread safe RunEquipment
        public void RunEquipment()
        {
            if (this.InvokeRequired)
            {
                ReturningVoidDelegate d = new ReturningVoidDelegate(RunEquipment);
                this.Invoke(d, new object[] { });
            }
            else
            {
                ImageFileName = _runFileName;
                Show();
            }
        }

        // Thread safe RunEquipment
        public void StopEquipment()
        {
            if (this.InvokeRequired)
            {
                ReturningVoidDelegate d = new ReturningVoidDelegate(StopEquipment);
                this.Invoke(d, new object[] { });
            }
            else
            {
                ImageFileName = _stopFileName;
                Show();
            }
        }


        // Thread safe RefreshBrewNumber
        private void RefreshBrewNumber()
        {
            if (this.InvokeRequired)
            {
                ReturningVoidDelegate d = new ReturningVoidDelegate(RefreshBrewNumber);
                this.Invoke(d, new object[] { });
            }
            else
            {
                _brewNumberLabel.Text = "Brew: " + _brewNumber;
                AlignCenter(_brewNumberLabel);
                Show();
            }
        }

        // Thread safe RefreshCurrentStateText
        private void RefreshCurrentStateText()
        {
            if (this.InvokeRequired)
            {
                ReturningVoidDelegate d = new ReturningVoidDelegate(RefreshCurrentStateText);
                this.Invoke(d, new object[] { });
            }
            else
            {
                _stateLabel.Text = "State: " + _currentStateText;
                AlignCenter(_stateLabel);
                Show();
            }
        }

        // Thread safe RefreshBrandName
        private void RefreshBrandName()
        {
            if (this.InvokeRequired)
            {
                ReturningVoidDelegate d = new ReturningVoidDelegate(RefreshBrandName);
                this.Invoke(d, new object[] { });
            }
            else
            {
                _brandNameLabel.Text = "Brand: " + _brandName;
                AlignCenter(_brandNameLabel);
                Show();
            }
        }

        public void StartFlashing()
        {
            //StartFlashTimer();
        }

        public void StopFlashing()
        {

        }

        void StartFlashTimer()
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            Action action = new Action(this);
            //Create timer
            //Console.WriteLine("Starting...");

            System.Threading.Timer nTimer = new System.Threading.Timer(action.DoThis, autoEvent, 1000, 10000);
            // When autoEvent signals, 
            autoEvent.WaitOne();
            //nTimer.Change(0, 500);
            //Controls.Clear();
            SetUpProcessEquipmentView();
        }   
    }

    class Action
    {
        ProcessEquipmentView _parent;
        public Action(ProcessEquipmentView parent)
        {
            _parent = parent;
        }
        //Callback
        public void DoThis(object state)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)state;
            //Console.WriteLine("Doing this...");
            SwitchImage();
            autoEvent.Set();
            //Thread.Sleep(1000);
        }
        private void SwitchImage()
        {
            //Console.WriteLine("In  SwitchImage...");
            //Console.WriteLine("_parent.ImageFileName: " + _parent.ImageFileName);
            _parent.ImageFileName = _parent.ImageFileName == _parent.RunFileName ? _parent.StopFileName : _parent.RunFileName;

        }
    }
}
