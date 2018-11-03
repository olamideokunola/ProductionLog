using System;
using BrewingModel;
using ProcessEquipmentParameters;

namespace BrewingModel.BrewingProcessEquipment
{
    public class MashFilterIdleState : MashFilterState, IMashFilterState, IStateDescription
    {
        public MashFilterIdleState()
        {
        }

        public string GetStateDescription()
        {
            return "MashFilterIdle";
        }

        public void InitBrew(MashFilter mashFilter, Brew brew)
        {
            string brewNumber = mashFilter.Brew.BrewNumber;
            if(brew != null)
            {
                if(brewNumber.Length == 0)
                {
                    mashFilter.InitBrew(brew);
                }
            }
        }

        public void OnEntry(MashFilter mashFilter, Brew brew)
        {

        }

        public void SetEndTime(string paramText, string endTime, MashFilter mashFilter, Brew brew)
        {

        }

        public void SetProteinRestTemperature(string temperature, MashFilter mashFilter, Brew brew)
        {
           
        }

        public void StartMashingIn(string paramText, string startTime, MashFilter mashFilter, Brew brew)
        {
            string brewNumber = brew.BrewNumber;
            string brandName = brew.BrandName;
            string mashFilterBrewNumber = mashFilter.Brew.BrewNumber;
            MashFilterProcessParameters param = MashFilterProcessParameters.MashingInStartTime;
            string paramName = param.ToString();

            //Start Mashing In9
            if (brandName.Length > 0 && 
               brewNumber.Length > 0 &&
               brewNumber == mashFilterBrewNumber &&
               paramText.Equals("Tranpsort Time - Finish"))
            {
                mashFilter.Brew.SetProcessParameterValue(ProcessEquipment.MashFilter, paramName, startTime);

                string mashingInTime = brew.GetProcessParameterValue(ProcessEquipment.MashFilter,
                                                                     MashFilterProcessParameters.MashingInStartTime.ToString());
                Console.WriteLine("Mash Filter mashingInTime: " + mashingInTime);

                //Set new state
                mashFilter.SetState(mashFilter.MashingInState);
            }

        }
    }
}