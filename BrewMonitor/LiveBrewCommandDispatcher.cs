using System;
using BrewingModel;

namespace BrewMonitor
{
    public abstract class LiveBrewCommandDispatcher
    {
        protected LiveBrewCommand liveBrewCommand;

        public void SendLiveBrewCommand(string fieldName, string fieldValue, Brew brew, string fieldSection)
        {
            liveBrewCommand = CreateLiveBrewCommand(fieldName, fieldValue, brew, fieldSection);
            liveBrewCommand.Execute();
            BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();
            brewingProcessHandler.PrintCurrentState();
        }

        public abstract LiveBrewCommand CreateLiveBrewCommand(String fieldName, string fieldValue, Brew brew, string fieldSection);
    }
}
