using System;
using BrewingModel;

namespace BrewMonitor
{
    public class HoldingVesselCommandDispatcher : LiveBrewCommandDispatcher
    {
        private static HoldingVesselCommandDispatcher _uniqueInstance = null;

        //lazy construction of instance
        public static HoldingVesselCommandDispatcher GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new HoldingVesselCommandDispatcher();
            }

            return _uniqueInstance;
        }

        //hidden constructer to allow Singleton
        private HoldingVesselCommandDispatcher()
        {
        }
        public override LiveBrewCommand CreateLiveBrewCommand(string fieldName, string fieldValue, Brew brew, string fieldSection)
        {
            switch (fieldName)
            {
                case "Start Filling - Finish":
                    liveBrewCommand = new StartHoldingVesselFillingCommand(brew.StartDate, brew.StartTime, brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    break;
                case "Transfer Time (WC) - Finish":
                case "Holding Vessle empty at - Finish":
                    liveBrewCommand = new CompleteHoldingVesselProcessStepCommand(brew.BrandName, brew.BrewNumber, fieldName, fieldValue, fieldSection);
                    break;
            }

            return liveBrewCommand;
        }
    }
}

