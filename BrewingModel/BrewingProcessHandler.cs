/*
 * A facade that provides access to BrewingModel subsystem
 * available as a Singleton
 */
using System;
using System.Collections.Generic;
using BrewingModel.BrewingProcessEquipment;
using Observer;

namespace BrewingModel
{
    public class BrewingProcessHandler : Subject
    {
        private string _filePath;
        private Brew _brew = new Brew();
        private IDictionary<string, Brew> _brews = new Dictionary<string, Brew>();

        public IDictionary<string, Brew> Brews
        {
            get
            {
                return _brews;
            }
        }

        //Process Equipment
        private MashCopper mashCopper;
        private MashTun mashTun;
        private MashFilter mashFilter;
        private HoldingVessel holdingVessel;
        private WortCopper wortCopper;
        private Whirlpool whirlpool;

        //Singleton
        private static BrewingProcessHandler _uniqueInstance = null;

        //lazy construction of instance
        public static BrewingProcessHandler GetInstance()
        {
            if(_uniqueInstance == null)
            {
                _uniqueInstance = new BrewingProcessHandler();
            }

            return _uniqueInstance;
        }

        private BrewingProcessHandler()
        {
            InitializeAllProcessEquipment();
        }


        //Methods
        public Brew GetBrew(string brewNumber)
        {   
            Brew brew = new Brew();
            return _brews.ContainsKey(brewNumber) ? _brews[brewNumber] : brew;
        }

        //Process Equipment Getters
        public MashCopper MashCopper
        {
            get
            {
                return mashCopper;
            }
        }

        public MashTun MashTun
        {
            get
            {
                return mashTun;
            }
        }

        public MashFilter MashFilter
        {
            get
            {
                return mashFilter;
            }
        }

        public HoldingVessel HoldingVessel
        {
            get
            {
                return holdingVessel;
            }
        }

        public WortCopper WortCopper
        {
            get
            {
                return wortCopper;
            }
        }

        public Whirlpool Whirlpool
        {
            get
            {
                return whirlpool;
            }
        }

        //LiveBrewMonitor Commands
        //MashCopper Commands
        public void StartMashCopperMashingIn(string startTime, string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);

            mashCopper.InitBrew(brew);
            mashCopper.StartMashingIn(fieldName, fieldValue);
            Notify();
        }

        public void CompleteMashCopperProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if(mashCopper.Brew.BrewNumber == brewNumber)
            {
                mashCopper.SetEndTime(fieldName, fieldValue);
                Notify();
            } else {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Mash Copper!");
            }
        }

        public void SetMashCopperProteinRestTemperature(string temperature)
        {
            //Brew brew = GetBrew(brewNumber);

            //mashCopper.InitBrew(brew);
            mashCopper.SetProteinRestTemperature(temperature);
            Notify();
        }
        //MashTun Commands
        public void StartMashTunMashingIn(string startTime, string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);

            mashTun.InitBrew(brew);
            mashTun.StartMashingIn(fieldName, fieldValue);
            Notify();
        }

        public void CompleteMashTunProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if (mashTun.Brew.BrewNumber == brewNumber)
            {
                mashTun.SetEndTime(fieldName, fieldValue);
                Notify();
            }
            else
            {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Mash Tun!");
            }
        }

        public void SetSacharificationRestTemperature(string temperature)
        {
            mashTun.SetSacharificationRestTemperature(temperature);
            Notify();
        }

        public void SetHeatingUpTemperature(string temperature)
        {
            mashTun.SetHeatingUpTemperature(temperature);
            Notify();
        }

        //MashFilter Commands
        public void StartMashFilterPrefilling(string startTime, string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);

            mashFilter.InitBrew(brew);
            mashFilter.StartPrefilling(fieldName, fieldValue);
            Notify();
        }

        public void CompleteMashFilterProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if (mashFilter.Brew.BrewNumber == brewNumber)
            {
                mashFilter.SetEndTime(fieldName, fieldValue);
                Notify();
            }
            else
            {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Mash Filter!");
            }
        }


        //HoldingVessel Commands
        public void StartHoldingVesselFilling(string startTime, string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);

            holdingVessel.InitBrew(brew);
            holdingVessel.StartFilling(fieldName, fieldValue);
            Notify();
        }

        public void CompleteHoldingVesselProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if (holdingVessel.Brew.BrewNumber == brewNumber)
            {
                holdingVessel.SetEndTime(fieldName, fieldValue);
                Notify();
            }
            else
            {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Holding Vessel!");
            }
        }

        // WortCopper Commands
        public void StartWortCopperHeating(string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);

            wortCopper.InitBrew(brew);
            wortCopper.StartHeating(fieldName, fieldValue);
            Notify();
        }

        public void StartWortCopperCasting(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);
            if (wortCopper.Brew.BrewNumber == brewNumber)
            {
                wortCopper.StartCasting(fieldName, fieldValue);
                Notify();
            }
            else
            {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Wort Copper!");
            }
        }

        public void CompleteWortCopperProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if (wortCopper.Brew.BrewNumber == brewNumber)
            {
                wortCopper.SetEndTime(fieldName, fieldValue);
                Notify();
            }
            else
            {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Wort Copper!");
            }
        }

        public void SetVolumeBeforeBoiling(string volume)
        {
            wortCopper.SetVolumeBeforeBoiling(volume);
            Notify();
        }

        public void SetVolumeAfterBoiling(string volume)
        {
            wortCopper.SetVolumeAfterBoiling(volume);
            Notify();
        }

        // Whirlpool Commands
        public void StartWhirlpoolCasting(string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);
            whirlpool.InitBrew(brew);
            whirlpool.StartCasting(fieldName, fieldValue);
            Notify();
        }

        public void CompleteWhirlpoolProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if (whirlpool.Brew.BrewNumber == brewNumber)
            {
                whirlpool.SetEndTime(fieldName, fieldValue);
                Notify();
            }
            else
            {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Whirlpool!");
            }
        }



        //Other methods
        private void InitializeAllProcessEquipment()
        {
            mashCopper = new MashCopper();
            mashTun = new MashTun();
            mashFilter = new MashFilter();
            holdingVessel = new HoldingVessel();
            wortCopper = new WortCopper();
            whirlpool = new Whirlpool();
            mashCopper.SetState(new MashCopperIdleState());
            mashTun.SetState(new MashTunIdleState());
            mashFilter.SetState(new MashFilterIdleState());
            holdingVessel.SetState(new HoldingVesselIdleState());
            wortCopper.SetState(new WortCopperIdleState());
            whirlpool.SetState(new WhirlpoolIdleState());
            Notify();
        }

        public void PrintCurrentState()
        {
            _brew.PrintCurrentState();
            mashCopper.PrintCurrentState();
            mashTun.PrintCurrentState();
            mashFilter.PrintCurrentState();
            holdingVessel.PrintCurrentState();
            wortCopper.PrintCurrentState();
            whirlpool.PrintCurrentState();
        }

        //User Methods
        public void StartNewBrew(string startDate, string brandName, string brewNumber)
        {
            if(!_brews.ContainsKey(brewNumber))
            {
                Brew brew = new Brew(startDate, brandName, brewNumber);

                _brews.Add(brewNumber, brew);
                Notify();
            }
        }
    }
}
