using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashCopperMashingInState : MashCopperState, IMashCopperState, IStateDescription
    {
        public MashCopperMashingInState()
        {
        }

        public string GetStateDescription()
        {
            return "MashCopperMashingIn";
        }

        public void InitBrew(MashCopper mashCopper, Brew brew)
        {

        }

        public void OnEntry(MashCopper mashCopper, Brew brew)
        {
           
        }

        public void SetEndTime(string paramText, string endTime, MashCopper mashCopper, Brew brew)
        {
            if (paramText.Equals("Mash in Time - Finish"))
            {
                MashCopperProcessParameters paramToCheck = MashCopperProcessParameters.MashingInStartTime;
                MashCopperProcessParameters paramToChange = MashCopperProcessParameters.MashingInEndTime;
                IMashCopperState newState = mashCopper.ProteinRestState;
                SetProcessStepEndTime(endTime, mashCopper, brew, paramToCheck, paramToChange, newState);
            }
        }

        public void SetProteinRestTemperature(string temperature, MashCopper mashCopper, Brew brew)
        {

        }

        public void StartMashingIn(string paramText, string startTime, MashCopper mashCopper, Brew brew)
        {
           
        }
    }
}
