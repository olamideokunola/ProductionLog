using System;  
using System.Threading;
using BrewMonitor;
using BrewingModel;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui;
using BrewingModel.Datasource;

namespace BrewLog
{
    class MainProgram
    {

        static void Main()
        {
            BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();
            brewingProcessHandler.StartNewBrew("01/01/2016", "Maltina", "258");

            ThreadStart guiRef = new ThreadStart(StartGui);
            Thread guiThread = new Thread(guiRef);
            guiThread.Start();
            StartBrewMonitor();

            //TestDataSource();
        }

        static void StartBrewMonitor()
        {
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            Action action = new Action();
            //Create timer
            Console.WriteLine("Starting...");

            System.Threading.Timer nTimer = new System.Threading.Timer(action.DoThis, autoEvent, 1000, 5000);
            autoEvent.WaitOne();
            nTimer.Change(0, 500);
        }

        static void StartGui()
        {
            Application.Run(new AppForm());
        }


    }

    class Action
    {   
        public Action()
        {

        }
        //Callback
        public void DoThis(object state)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)state;
            Console.WriteLine("Doing this...");
            string filePath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/2018/september/9/";
            string brewNumber = "258";

            LiveBrewMonitor liveBrewMonitor = LiveBrewMonitor.GetInstance();
            liveBrewMonitor.StartMonitoring(filePath, "Maltina", brewNumber);
            Thread.Sleep(1000);
        }
    }
}