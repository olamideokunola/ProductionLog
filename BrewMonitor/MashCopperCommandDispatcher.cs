﻿using System;
using BrewingModel;

namespace BrewMonitor
{
    public class MashCopperCommandDispatcher : LiveBrewCommandDispatcher
    {
        private static MashCopperCommandDispatcher _uniqueInstance = null;

        //lazy construction of instance
        public static MashCopperCommandDispatcher GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new MashCopperCommandDispatcher();
            }

            return _uniqueInstance;
        }

        //hidden constructer to allow Singleton
        private MashCopperCommandDispatcher()
        {
        }

        public override LiveBrewCommand CreateLiveBrewCommand(string fieldName, string fieldValue, Brew brew, string fieldSection)
        {
            switch (fieldName)
            {
                case "Transport Time RAW Sorguum to WB MC - Finish":
                    liveBrewCommand = new StartMashCopperMashingInCommand(brew.StartDate, brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    break;
                case "Mash in Time - Finish":
                case "Protein Rest Time - Finish":
                case "Heating Time - Finish":
                case "Rest time - Finish":
                case "Heating Time - Finish 2":
                case "Rest Time - Finish 2":
                case "Mash Transfer to MT - Finish":
                case "Mash Copper empty at - Finish":
                    liveBrewCommand = new CompleteMashCopperProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    break;
            }

            return liveBrewCommand;
        }
    }
}

