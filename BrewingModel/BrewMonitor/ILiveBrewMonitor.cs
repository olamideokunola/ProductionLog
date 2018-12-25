using System;

namespace BrewingModel.BrewMonitor
{
    public interface ILiveBrewMonitor
    {
        void StartMonitoring(string filePath, string brandName, string brewNumber);
    }
}
