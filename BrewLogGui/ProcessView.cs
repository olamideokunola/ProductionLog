using System;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui.ProcessEquipmentViews;
using BrewingModel.BrewingProcessEquipment;

namespace BrewLogGui
{
    public class ProcessView : ProcessViewPictureBox
    {
        MashCopperView mashCopperView;
        PumpView mashTransferPumpView;
        MashCoolerView mashCoolerView;
        MashTunView mashTunView;
        PumpView mashFiltrationPumpView;
        MashFilterView mashFilterView;
        HoldingVesselView holdingVesselView;
        PumpView wortPumpView;
        WortCopperView wortCopperView;
        PumpView castingPumpView;
        WhirlpoolView whirlpoolView;
        PumpView coolingPumpView;
        WortCoolerView wortCoolerView;

        // Equipment getters
        public MashCopperView MashCopperView
        {
            get
            {
                return mashCopperView;
            }
        }

        public PumpView MashTransferPumpView
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

        public PumpView MashFiltrationPumpView
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

        public PumpView WortPumpView
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

        public PumpView CastingPumpView
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

        public PumpView CoolingPumpView
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
        public ProcessView()
        {
            this.Size = new Size(1000, 500);
            //String imgPathRel = GetImagePath("house.jpg");

            //this.Image = new Bitmap(imgPathRel);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(229,229,229);
            this.Show();

            // Create ProcessEquipment
            SetUpProcessEquipmentViews();

        }

        private void SetUpProcessEquipmentViews()
        {
            wortCoolerView = new WortCoolerView();
            SetupProcessEquipmentView(wortCoolerView, 850, 200);

            coolingPumpView = new PumpView("Cooling Pump");
            SetupProcessEquipmentView(coolingPumpView, 800, 200);

            whirlpoolView = new WhirlpoolView();
            SetupProcessEquipmentView(whirlpoolView, 750, 0);

            castingPumpView = new PumpView("Casting Pump");
            SetupProcessEquipmentView(castingPumpView, 700, 200);

            wortCopperView = new WortCopperView();
            SetupProcessEquipmentView(wortCopperView, 625, 0);

            wortPumpView = new PumpView("Wort Pump");
            SetupProcessEquipmentView(wortPumpView, 600, 200);

            holdingVesselView = new HoldingVesselView();
            SetupProcessEquipmentView(holdingVesselView, 445, 0);

            mashFilterView = new MashFilterView();
            SetupProcessEquipmentView(mashFilterView, 300, 200);

            mashFiltrationPumpView = new PumpView("Mash Filtration Pump");
            SetupProcessEquipmentView(mashFiltrationPumpView, 250, 200);

            mashTunView = new MashTunView();
            SetupProcessEquipmentView(mashTunView, 175, 0);

            mashCoolerView = new MashCoolerView();
            SetupProcessEquipmentView(mashCoolerView, 85, 200);

            mashTransferPumpView = new PumpView("Mash Transfer Pump");
            SetupProcessEquipmentView(mashTransferPumpView, 0, 200);

            mashCopperView = new MashCopperView();
            SetupProcessEquipmentView(mashCopperView, 0,0);

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
