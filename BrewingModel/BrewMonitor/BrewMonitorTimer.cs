using System;
using System.Threading;
using BrewingModel.Settings;

namespace BrewingModel.BrewMonitor
{
    public class BrewMonitorTimer
    {
        //Singleton
        private static BrewMonitorTimer _uniqueInstance = null;

        //lazy construction of instance
        public static BrewMonitorTimer GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new BrewMonitorTimer();
            }
            return _uniqueInstance;
        }

        private BrewMonitorTimer()
        {

        }

        public static void Startup()
        {
            ThreadStart monitorRef = new ThreadStart(StartBrewMonitor);
            Thread monitorThread = new Thread(monitorRef);
            monitorThread.Start();
        }

        public static void StartBrewMonitor()
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            Action action = new Action();
            //Create timer
            Console.WriteLine("Starting...");

            System.Threading.Timer nTimer = new System.Threading.Timer(
                callback: action.DoThis,
                state: autoEvent,
                dueTime: 1000,
                period: 15000);
            autoEvent.WaitOne();
            // nTimer.Change(0, 500);
        }
    }

    class Action
    {
        MyAppSettings appSettings = MyAppSettings.GetInstance();

        public Action()
        {
        }

        //Callback
        public void DoThis(object state)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)state;
            Console.WriteLine("Doing this...");

            LiveBrewMonitor liveBrewMonitor = LiveBrewMonitor.GetInstance();
            liveBrewMonitor.MonitorBrews();
            // Thread.Sleep(1000);
        }
    }
}
