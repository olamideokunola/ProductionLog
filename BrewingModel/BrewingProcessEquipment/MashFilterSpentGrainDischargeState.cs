using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterMashingInState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterMashingInState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterMashingIn";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {

        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {
            if (paramText.Equals("Mash in Time - Finish"))
            {
                MashFilterProcessParameters paramToCheck = MashFilterProcessParameters.MashingInStartTime;
                MashFilterProcessParameters paramToChange = MashFilterProcessParameters.MashingInEndTime;
                IMashFilterState newState = mashFilter.ProteinRestState;
                SetProcessStepEndTime(endTime, mashFilter, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashFilter mashFilter, Brew brew)
        {

        }

        public void StartMashingIn(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
           
        }
    }
}
