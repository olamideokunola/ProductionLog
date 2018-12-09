using System;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui.ProcessEquipmentViews;
using BrewingModel.BrewingProcessEquipment;

namespace BrewLogGui
{
    public class ProcessView : ProcessViewPictureBox
    {
        private MashCopperView mashCopperView;
        private MashTransferPumpView mashTransferPumpView;
        private MashCoolerView mashCoolerView;
        private MashTunView mashTunView;
        private MashFiltrationPumpView mashFiltrationPumpView;
        private MashFilterView mashFilterView;
        private HoldingVesselView holdingVesselView;
        private WortPumpView wortPumpView;
        private WortCopperView wortCopperView;
        private CastingPumpView castingPumpView;
        private WhirlpoolView whirlpoolView;
        private CoolingPumpView coolingPumpView;
        private WortCoolerView wortCoolerView;

        private static ProcessView _uniqueInstance = null;

        //lazy construction of instance
        public static ProcessView GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new ProcessView();
            }

            return _uniqueInstance; 
        }
        // Equipment getters
        public MashCopperView MashCopperView
        {
            get
            {
                return mashCopperView;
            }
        }

        public MashTransferPumpView MashTransferPumpView
        {
            get
            {
                return mashTransferPumpView;
            }
        }
        public MashTunView MashTunView
        {
            get
            {
                return mashTunView;
            }
        }

        public MashFiltrationPumpView MashFiltrationPumpView
        {
            get
            {
                return mashFiltrationPumpView;
            }
        }

        public MashCoolerView MashCoolerView
        {
            get
            {
                return mashCoolerView;
            }
        }

        public HoldingVesselView HoldingVesselView
        {
            get
            {
                return holdingVesselView;
            }
        }

        public WortPumpView WortPumpView
        {
            get
            {
                return wortPumpView;
            }
        }

        public WortCopperView WortCopperView
        {
            get
            {
                return wortCopperView;
            }
        }

        public CastingPumpView CastingPumpView
        {
            get
            {
                return castingPumpView;
            }
        }

        public MashFilterView MashFilterView
        {
            get
            {
                return mashFilterView;
            }
        }

        public WhirlpoolView WhirlpoolView
        {
            get
            {
                return whirlpoolView;
            }
        }

        public CoolingPumpView CoolingPumpView
        {
            get
            {
                return coolingPumpView;
            }
        }

        public WortCoolerView WortCoolerView
        {
            get
            {
                return wortCoolerView;
            }
        }

        // Constructor
        private ProcessView()
        {
            this.Size = new Size(1000, 500);
            String imgPathRel = GetImagePath("process_view.png");

            this.Image = new Bitmap(imgPathRel);
            ScaleImage(this);

            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(229,229,229);
            this.Show();

            // Create ProcessEquipment
            SetUpProcessEquipmentViews();

        }

        protected void SetUpProcessEquipmentViews()
        {


            wortCoolerView = new WortCoolerView();
            SetupProcessEquipmentView(wortCoolerView, 915, 247);

            coolingPumpView = new CoolingPumpView();
            SetupProcessEquipmentView(coolingPumpView, 859, 245);

            whirlpoolView = new WhirlpoolView();
            SetupProcessEquipmentView(whirlpoolView, 830, 90);

            castingPumpView = new CastingPumpView();
            SetupProcessEquipmentView(castingPumpView, 785, 245);

            wortCopperView = new WortCopperView();
            SetupProcessEquipmentView(wortCopperView, 678, 72);

            wortPumpView = new WortPumpView();
            SetupProcessEquipmentView(wortPumpView, 635, 240);

            holdingVesselView = new HoldingVesselView();
            SetupProcessEquipmentView(holdingVesselView, 555, 80);

            mashFilterView = new MashFilterView();
            SetupProcessEquipmentView(mashFilterView, 376, 243);

            mashFiltrationPumpView = new MashFiltrationPumpView();
            SetupProcessEquipmentView(mashFiltrationPumpView, 319, 245);

            mashTunView = new MashTunView();
            SetupProcessEquipmentView(mashTunView, 239, 50);

            mashCoolerView = new MashCoolerView();
            SetupProcessEquipmentView(mashCoolerView, 175, 247);

            mashTransferPumpView = new MashTransferPumpView();
            SetupProcessEquipmentView(mashTransferPumpView, 123, 245);

            mashCopperView = new MashCopperView();
            SetupProcessEquipmentView(mashCopperView, 85,50);
        }

        private void SetupProcessEquipmentView(ProcessEquipmentView processEquipmentView,
                                              int xPoint, int yPoint)
        {
            processEquipmentView.Location = new Point(xPoint, yPoint);
            Controls.Add(processEquipmentView);
            processEquipmentView.Show();
        }
    }
}
