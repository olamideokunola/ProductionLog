﻿/*
 * A facade that provides access to BrewingModel subsystem
 * available as a Singleton
 */
using System;
using System.Collections.Generic;
using BrewingModel.BrewingProcessEquipment;

namespace BrewingModel
{
    public class BrewingProcessHandler
    {
        private string _filePath;
        private Brew _brew = new Brew();
        private IDictionary<string, Brew> _brews = new Dictionary<string, Brew>();

        //Process Equipment
        private MashCopper mashCopper;
        private MashTun mashTun;
        //private MashFilter mashFilter;
        //private HoldingVessel holdingVessel;
        //private WortCopper wortCopper;
        //private Whirlpool whirlpool;

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

        //LiveBrewMonitor Commands
        //MashCopper Commands
        public void StartMashCopperMashingIn(string startTime, string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);

            mashCopper.InitBrew(brew);
            mashCopper.StartMashingIn(fieldName, fieldValue);
        }

        public void CompleteMashCopperProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if(mashCopper.Brew.BrewNumber == brewNumber)
            {
                mashCopper.SetEndTime(fieldName, fieldValue);
            } else {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Mash Copper!");
            }
        }

        //MashTun Commands
        public void StartMashTunMashingIn(string startTime, string brewNumber, string fieldName, string fieldValue)
        {
            Brew brew = GetBrew(brewNumber);

            mashTun.InitBrew(brew);
            mashTun.StartMashingIn(fieldName, fieldValue);
        }

        public void CompleteMashTunProcessStep(string brewNumber, string fieldName, string fieldValue)
        {
            //Brew brew = GetBrew(brewNumber);

            if (mashTun.Brew.BrewNumber == brewNumber)
            {
                mashTun.SetEndTime(fieldName, fieldValue);
            }
            else
            {
                Console.WriteLine("Incorrect Brew! Dispatched Brew does not match brew in Mash Tun!");
            }
        }


        private void InitializeAllProcessEquipment()
        {
            mashCopper = new MashCopper();
            mashTun = new MashTun();
            mashCopper.SetState(new MashCopperIdleState());
            mashTun.SetState(new MashTunIdleState());
        }

        public void PrintCurrentState()
        {
            _brew.PrintCurrentState();
            mashCopper.PrintCurrentState();
            mashTun.PrintCurrentState();
        }

        //User Methods
        public void StartNewBrew(string startDate, string brandName, string brewNumber)
        {
            if(!_brews.ContainsKey(brewNumber))
            {
                Brew brew = new Brew(startDate, brandName, brewNumber);

                _brews.Add(brewNumber, brew);
            }
        }
    }
}