/*
 * An abstraction for a Brew
 */

using System;
using System.Collections.Generic;
using ProcessEquipmentParameters;
using ProcessFields.ProcessDurations;
using Util;
using BrewingModel.Datasources;

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

        // Method foor getting process durations
        private string GetProcessDuration(ProcessEquipment processEquipment, string processStartTime, string processEndTime)
        {
            string startTimeString = GetProcessParameterValue(processEquipment, processStartTime);
            string endTimeString = GetProcessParameterValue(processEquipment, processEndTime);

            if (startTimeString != "" && endTimeString != "")
            {
                DateTime startTime = DateHelper.GetProcessParameterDateTime(startTimeString);
                DateTime endTime = DateHelper.GetProcessParameterDateTime(GetProcessParameterValue(processEquipment, processEndTime));

                TimeSpan processDuration = endTime - startTime;

                return processDuration.ToString(@"hh\:mm\:ss");
            }
            else
            {
                return "";
            }
        }

        // MashCopper Process Duration Calculations
        public IDictionary<string, string> GetMashCopperProcessDurations()
        {
            // MashCopper
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

        // MashTun Process Duration Calculations
        public IDictionary<string, string> GetMashTunProcessDurations()
        {
            // MashTun
            IDictionary<string, string> mashTunProcessDurations = new Dictionary<string, string>
            {
                { MashTunProcessDurations.MashingInDuration.ToString(), GetMashTunMashingInDuration() },
                { MashTunProcessDurations.ProteinRestDuration.ToString(), GetMashTunProteinRestDuration() },
                { MashTunProcessDurations.SaccharificationDuration.ToString(), GetMashTunSaccharificationDuration() },
                { MashTunProcessDurations.HeatingUpDuration.ToString(), GetMashTunHeatingUpDuration() },
            };

            return mashTunProcessDurations;
        }

        private string GetMashTunHeatingUpDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.SacharificationRestEndTime.ToString();
            string processEndTimeString = MashTunProcessParameters.HeatingUpEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashTunSaccharificationDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.ProteinRestEndTime.ToString();
            string processEndTimeString = MashTunProcessParameters.SacharificationRestEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashTunProteinRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.MashingInEndTime.ToString();
            string processEndTimeString = MashTunProcessParameters.ProteinRestEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashTunMashingInDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashTun;
            string processStartTimeString = MashTunProcessParameters.MashingInStartTime.ToString();
            string processEndTimeString = MashTunProcessParameters.MashingInEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        // MashFilter Process Duration Calculations
        public IDictionary<string, string> GetMashFilterProcessDurations()
        {
            // MashFilter
            IDictionary<string, string> mashFilterProcessDurations = new Dictionary<string, string>
            {
                { MashFilterProcessDurations.MainWortFiltrationDuration.ToString(), GetMashFilterMainWortFiltrationDuration() },
                { MashFilterProcessDurations.SpargingRestDuration.ToString(), GetMashFilterSpargingRestDuration() },
                { MashFilterProcessDurations.TotalFiltrationDuration.ToString(), GetMashFilterTotalFiltrationDuration() },
            };

            return mashFilterProcessDurations;
        }

        private string GetMashFilterTotalFiltrationDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashFilter;
            string processStartTimeString = MashFilterProcessParameters.PrefillingEndTime.ToString();
            string processEndTimeString = MashFilterProcessParameters.ExtraSpargingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashFilterSpargingRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashFilter;
            string processStartTimeString = MashFilterProcessParameters.MainMashFiltrationEndTime.ToString();
            string processEndTimeString = MashFilterProcessParameters.SpargingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetMashFilterMainWortFiltrationDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.MashFilter;
            string processStartTimeString = MashFilterProcessParameters.WeakWortTransferEndTime.ToString();
            string processEndTimeString = MashFilterProcessParameters.MainMashFiltrationEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }


        // WortCopper Process Duration Calculations
        public IDictionary<string, string> GetWortCopperProcessDurations()
        {
            // WortCopper
            IDictionary<string, string> mashFilterProcessDurations = new Dictionary<string, string>
            {
                { WortCopperProcessDurations.HeatToBoilDuration.ToString(), GetWortCopperHeatToBoilDuration() },
                { WortCopperProcessDurations.BoilingDuration.ToString(), GetWortCopperBoilingDuration() }
            };

            return mashFilterProcessDurations;
        }

        private string GetWortCopperBoilingDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.WortCopper;
            string processStartTimeString = WortCopperProcessParameters.HeatingEndTime.ToString();
            string processEndTimeString = WortCopperProcessParameters.BoilingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetWortCopperHeatToBoilDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.WortCopper;
            string processStartTimeString = WortCopperProcessParameters.HeatingStartTime.ToString();
            string processEndTimeString = WortCopperProcessParameters.HeatingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        // Whirlpool Process Duration Calculations
        public IDictionary<string, string> GetWhirlpoolProcessDurations()
        {
            // Whirlpool
            IDictionary<string, string> mashFilterProcessDurations = new Dictionary<string, string>
            {
                { WhirlpoolProcessDurations.CastingDuration.ToString(), GetWhirlpoolCastingDuration() },
                { WhirlpoolProcessDurations.RestDuration.ToString(), GetWhirlpoolRestDuration() },
                { WhirlpoolProcessDurations.CoolingDuration.ToString(), GetWhirlpoolCoolingDuration() }
            };

            return mashFilterProcessDurations;
        }

        private string GetWhirlpoolCoolingDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.Whirlpool;
            string processStartTimeString = WhirlpoolProcessParameters.RestingEndTime.ToString();
            string processEndTimeString = WhirlpoolProcessParameters.CoolingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetWhirlpoolRestDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.Whirlpool;
            string processStartTimeString = WhirlpoolProcessParameters.CastingEndTime.ToString();
            string processEndTimeString = WhirlpoolProcessParameters.RestingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
        }

        private string GetWhirlpoolCastingDuration()
        {
            ProcessEquipment processEquipment = ProcessEquipment.Whirlpool;
            string processStartTimeString = WhirlpoolProcessParameters.CastingStartTime.ToString();
            string processEndTimeString = WhirlpoolProcessParameters.CastingEndTime.ToString();

            return GetProcessDuration(processEquipment, processStartTimeString, processEndTimeString);
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

        // Load data from Datasource
        internal void LoadEquipmentProcessParameters(ProcessEquipment processEquipment)
        {
            IDictionary<string, string> loadedProcessEquipmentParameters = processEquipmentParameters[ProcessEquipment.MashCopper];

            //Datasource datasource = XlDatasource.GetInstance();
        }

        public void Save()
        {
            DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance();
            datasourceHandler.SaveBrew(this);
        }
    }
}
