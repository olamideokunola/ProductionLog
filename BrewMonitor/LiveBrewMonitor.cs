/*
 * Sends commands to BrewLogger based on data updates obtained from brews in process with data on file datasource
 */

using System;
using System.Timers;
using System.Collections.Generic;
using BrewingModel;

namespace BrewMonitor
{
    public class LiveBrewMonitor
    {
        private static LiveBrewMonitor _uniqueInstance = null;

        //lazy construction of instance
        public static LiveBrewMonitor GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new LiveBrewMonitor();
            }

            return _uniqueInstance;
        }

        private Boolean isMonitoring;

        public Boolean IsMonitoring
        {
            get
            {
                return isMonitoring;
            }
        }

        private BrewLoader _brewLoader;
        private BrewInProcess _brewInProcess;
        private IDictionary<string, BrewInProcess> _brewsInProcess = new Dictionary<string, BrewInProcess>();
        private RequiredBrewParameters _requiredBrewParameters;
        private IDictionary<string, IDictionary<string, string>> _brewFields;
        private IFileParser _brewFileParser;

        //hidden constructer to allow Singleton
        private LiveBrewMonitor()
        {
            isMonitoring = false;
            // _monitorTimer = new MonitorTimer(5000, this);
            SetupBrewFields();
            _brewFileParser = new FileParser();
            _brewLoader = new BrewLoader(_brewFileParser);
        }

        public void StartMonitoring(string filePath, string brandName, string brewNumber)
        {
            AddToWatchList(filePath, brandName, brewNumber);
            MonitorBrews();
            if (!isMonitoring)
            {
                isMonitoring = true;
            }
        }

        private void AddToWatchList(string filePath, string brandName, string brewNumber)
        {
            _brewLoader.CreateBrewInProcess(filePath, brewNumber);
            //Console.WriteLine("In AddToWatchList");
            BrewInProcess nBrewInProcess = _brewLoader.GetBrewInProcess(filePath, brewNumber);

            //Add to watchlist
            if(!_brewsInProcess.ContainsKey(nBrewInProcess.BrewNumber))
            {
                _brewsInProcess.Add(nBrewInProcess.BrewNumber, nBrewInProcess);
            }
        }

        public void MonitorBrews()
        {

            Console.WriteLine("In MonitorBrews!");
            foreach (KeyValuePair<string, BrewInProcess> brewInProcess in _brewsInProcess) 
            {
                Console.WriteLine("In _brewsInProcess count!");
                string brewNumber = brewInProcess.Key.Trim();
                _brewInProcess = brewInProcess.Value;
                string filePath = _brewInProcess.FilePath;

                Console.WriteLine("Before: Has new field? " + _brewInProcess.HasNewField().ToString());

                //Get updated brew in process
                _brewInProcess = _brewLoader.GetBrewInProcess(filePath, brewNumber);

                //Console.WriteLine("After: Has new field? " + _brewInProcess.HasNewField().ToString());

                if (_brewInProcess.HasNewField())
                {
                    Console.WriteLine("In HasNewField!");
                    foreach (KeyValuePair<string, IDictionary<string, string>> section in _brewInProcess.NewField)
                    {
                        IDictionary<string, string> sectionFields = new Dictionary<string, string>();
                        sectionFields = section.Value;
                        foreach(KeyValuePair<string, string> field in sectionFields)
                        {
                            string fieldName = field.Key;
                            string fieldValue = field.Value;
                            string fieldSection = section.Key;

                            Console.WriteLine("Has new field!");
                            Console.WriteLine(fieldSection + ": " + fieldSection);
                            Console.WriteLine(fieldName + ": " + fieldValue);

                            BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();
                            Brew brew = brewingProcessHandler.GetBrew(_brewInProcess.BrewNumber);

                            LiveBrewCommandDispatcherFactory liveBrewCommandDispatcherFactory = LiveBrewCommandDispatcherFactory.GetInstance();
                            LiveBrewCommandDispatcher liveBrewCommandDispatcher = liveBrewCommandDispatcherFactory.GetLiveBrewCommandDispatcher(fieldSection);

                            LiveBrewCommand liveBrewCommand = liveBrewCommandDispatcher.CreateLiveBrewCommand(fieldName, fieldValue, brew, fieldSection);
                            liveBrewCommand.Execute();
                        }
                    }
                }
                _brewInProcess.ResetNewField();
            }
        }

        private void SetupBrewFields()
        {
            _requiredBrewParameters = new RequiredBrewParameters();
            _brewFields = _requiredBrewParameters.GetRequiredBrewParameters();
        }
    }
}
