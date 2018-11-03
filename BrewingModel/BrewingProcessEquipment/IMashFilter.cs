using System;
using BrewingModel;

namespace BrewingModel.BrewingProcessEquipment
{
    public interface IMashFilterState
    {
        void InitBrew(MashFilter mashFilter, Brew brew);
        void StartMashingIn(string paramText, string startTime, MashFilter mashFilter, Brew brew);
        void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew);
        void OnEntry(MashFilter mashFilter, Brew brew);
        void SetProteinRestTemperature(string temperature, MashFilter mashFilter, Brew brew);
    }
}
