/*
 * An abstraction for a Brew
 */

using System;
using System.Collections.Generic;
using ProcessEquipmentParameters;
using ProcessFields.ProcessDurations;
using Util;

namespace BrewingModel
{
    public class Brew : IBrew
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

            CurrentState = _idleState;
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
            CurrentState = newState;
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

        public IBrewState CurrentState { get => _currentState; set => _currentState = value; }

        public string Year 
        {
            get
            {
                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                return stringDateWorker.GetYear(_startDate);
            }
        }

        public string Month
        {
            get
            {
                StringDateWorker stringDateWorker = StringDateWorker.GetInstance();
                return stringDateWorker.GetMonth(_startDate);
            }
        }
        
        //Getters & Setters for Process Equipment Fields
        public string GetProcessParameterValue(ProcessEquipment processEquipment, string parameterName)
        {
            string parameterValue = "";
            IDictionary<string, string> processParameter = new Dictionary<string, string>();
            processParameter = processEquipmentParameters[processEquipment];
            if (processParameter.ContainsKey(parameterName))
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
            }
            else
            {
                processParameter[parameterName] = parameterValue;
            }
        }


        // Mash Copper Process Duration Calculations
        public IDictionary<string, string> GetMashCopperProcessDurations()
        {
            IDictionary<string, string> mashCopperProcessDurations = new Dictionary<string, string>
            {
                { MashCopperProcessDurations.MashingInDuration.ToString(), GetMashCopperMashingInDuration() },
                { MashCopperProcessDurations.ProteinRestDuration.ToString(), GetMashCopperProteinRestDuration() },
                { MashCopperProcessDurations.HeatingUp1Duration.ToString(), GetMashCopperHeatingUp1Duration() },
                { MashCopperProcessDurations.Rest1Duration.ToString(), GetMashCopperRest1Duration() },
                { MashCopperProcessDurations.HeatingUp2Duration.ToString(), GetMashCopperHeatingUp2Duration() },
                { MashCopperProcessDurations.Rest2Duration.ToString(), GetMashCopperRest2Duration() }
            };

            return mashCopperProcessDurations;
        }

        private string GetMashCopperRest2Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.HeatingUp2EndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.Rest2EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashCopperHeatingUp2Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.Rest1EndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.HeatingUp2EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashCopperRest1Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.HeatingUp1EndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.Rest1EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashCopperHeatingUp1Duration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.ProteinRestEndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.HeatingUp1EndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashCopperProteinRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.MashingInEndTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.ProteinRestEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashCopperMashingInDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashCopper;
            string processStartTimeString = MashCopperProcessParameters.MashingInStartTime.ToString();
            string processEndTimeString = MashCopperProcessParameters.MashingInEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetProcessDuration(ProcessEquipment processEquipment, string processStartTime, string processEndTime)
        {
            DateTime startTime = DateHelper.GetProcessParameterDateTime(GetProcessParameterValue(processEquipment, processStartTime));
            DateTime endTime = DateHelper.GetProcessParameterDateTime(GetProcessParameterValue(processEquipment, processEndTime));

            TimeSpan processDuration = endTime - startTime;

            return processDuration.ToString(@"hh\:mm\:ss");
        }


        // Load data from Datasource
        public IDictionary<string, string> LoadProcessParameters()
        {
            IDictionary<string, string> processParameters = new Dictionary<string, string>();



            return processParameters;
        }

        //Event methods
        public void StartBrew(string startTime)
        {
            CurrentState.StartMashCopperMashingIn(startTime, this);
        }

        //Action Methods in state classes
        public void PrintCurrentState()
        {
            Console.WriteLine("-----------");
            Console.WriteLine("Current Brew State: " + CurrentState.GetType());
            Console.WriteLine("-----------");
        }

    }
}
