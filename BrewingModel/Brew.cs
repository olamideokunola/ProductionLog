/*
 * An abstraction for a Brew
 */

using System;
using System.Collections.Generic;
using ProcessEquipmentParameters;

namespace BrewingModel
{
    public class Brew
    {
        private string _startDate;
        private string _startTime;
        private string _brandName;
        private string _brewNumber;

        //Delegate state
        private IBrewState _currentState;

        //Brew states
        private IBrewState _idleState;
        private IBrewState _inProcessState;
        private IBrewState _completedState;

        //Process Equipment Parameters Collections
        private IDictionary<ProcessEquipment, IDictionary<string, string>> processEquipmentParameters = 
            new Dictionary<ProcessEquipment, IDictionary<string, string>>();

        public IDictionary<ProcessEquipment, IDictionary<string, string>> ProcessEquipmentParameters
        {
            get
            {
                return processEquipmentParameters;
            }
        }

        public Brew(string startDate, string brandName, string brewNumber)
        {
            _startDate = startDate;
            _startTime = "";
            _brandName = brandName;
            _brewNumber = brewNumber;

            //Setup processEquipmentParameters
            processEquipmentParameters.Add(ProcessEquipment.MashCopper, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.MashTun, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.MashFilter, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.HoldingVessel, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.WortCopper, new Dictionary<string, string>());
            processEquipmentParameters.Add(ProcessEquipment.Whirlpool, new Dictionary<string, string>());

            //Setup states
            _idleState = new BrewIdleState();
            _inProcessState = new BrewInProcessState();
            _completedState = new BrewCompletedState();

            _currentState = _idleState;
        }

        public Brew()
        {

        }

        //getters
        public string StartDate
        {
            get
            {
                return _startDate;
            }
        }

        public string StartTime
        {
            get
            {
                return _startTime;
            }
        }

        public string BrandName
        {
            get
            {
                return _brandName;
            }
        }

        public string BrewNumber
        {
            get
            {
                return _brewNumber;
            }
        }


        //Methods
        public void SetState(IBrewState newState)
        {
            _currentState = newState;
            PrintCurrentState();
        }


        //Getters for states
        public IBrewState IdleState
        {
            get
            {
                return _idleState;
            }
        }

        public IBrewState InProcessState
        {
            get
            {
                return _inProcessState;
            }
        }

        public IBrewState CompletedState
        {
            get
            {
                return _completedState;
            }
        }

        //Getters & Setters for Process Equipment Fields
        public string GetProcessParameterValue(ProcessEquipment processEquipment, string parameterName)
        {
            string parameterValue = "";
            IDictionary<string, string> processParameter = new Dictionary<string, string>();
            processParameter = processEquipmentParameters[processEquipment];
            if(processParameter.ContainsKey(parameterName))
            {
                parameterValue = processParameter[parameterName];
            }
            return parameterValue;
        }

        public void SetProcessParameterValue(ProcessEquipment processEquipment, string parameterName, string parameterValue)
        {
            IDictionary<string, string> processParameter = new Dictionary<string, string>();
            processParameter = processEquipmentParameters[processEquipment];
            if (!processParameter.ContainsKey(parameterName))
            {
                processParameter.Add(parameterName, parameterValue);
            } else {
                processParameter[parameterName] = parameterValue;
            }
        }


        //Event methods
        public void StartBrew(string startTime)
        {
            _currentState.StartMashCopperMashingIn(startTime, this);
        }


        //Action Methods in state classes


        public void PrintCurrentState()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("Current Brew State: " + _currentState.GetType());
            Console.WriteLine("-----------");
        }

    }
}
