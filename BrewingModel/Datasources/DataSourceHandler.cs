using System;
using System.Collections.Generic;

namespace BrewingModel.Datasources
{
    public class DataSourceHandler
    {
        //Singleton
        private static DataSourceHandler _uniqueInstance = null;

        //lazy construction of instance
        public static DataSourceHandler GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new DataSourceHandler();
            }

            return _uniqueInstance;
        }

        private IDictionary<string, Period> _periods = new Dictionary<string, Period>();
        private Datasource _datasource;

        public Datasource Datasource { get => _datasource; set => _datasource = value; }

        private DataSourceHandler(Datasource datasource)
        {
            Datasource = datasource;
        }

        private DataSourceHandler()
        {
        }

        public void SaveBrew(IBrew brew)
        {
            _datasource.SaveBrew(brew);
        }

        public Brew GetBrewWithProcessParameters(IBrew brew)
        {
            return (BrewingModel.Brew)_datasource.GetBrewWithProcessParameters(brew);
        }

        public Period LoadPeriod(string year, Month month)
        {
            return _datasource.GetPeriod(year, month);
        }

        public void LoadPeriods()
        {
            _datasource.LoadPeriods();
        }
    }
}
