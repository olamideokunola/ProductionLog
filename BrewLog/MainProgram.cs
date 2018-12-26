using System;  
using System.Threading;
using BrewingModel;
using System.Windows.Forms;
using System.Drawing;
using BrewLogGui;
using BrewingModel.Datasources;
using BrewingModel.BrewMonitor;
using BrewingModel.Settings;

namespace BrewLog
{
    class MainProgram
    {

        static void Main()
        {
            BrewingProcessHandler brewingProcessHandler = BrewingProcessHandler.GetInstance();
            brewingProcessHandler.StartNewBrew("09/09/2018", "Maltina", "258");
            //ApplicationSettings appsettings = new ApplicationSettings();
            MyAppSettings appSettings = MyAppSettings.GetInstance();

            //string connectionString = appsettings.ConnectionString;
            //string templateFilePath = appsettings.TemplateFilePath;
            string connectionString = appSettings.ConnectionString;
            string templateFilePath = appSettings.TemplateFilePath;
            //string templateFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}period_template.xlsx";

            // Setup Datasource Handler
            Datasource datasource = new XlDatasource(connectionString, templateFilePath);
            DatasourceHandler datasourceHandler = DatasourceHandler.GetInstance(datasource);

            // Gui Thread
            ThreadStart guiRef = new ThreadStart(StartGui);
            Thread guiThread = new Thread(guiRef);
            guiThread.Start();

            // Main thread
            StartBrewMonitor();




            //TestDataSource();
        }

        static void StartBrewMonitor()
        {

            AutoResetEvent autoEvent = new AutoResetEvent(false);
            Action action = new Action();
            //Create timer
            Console.WriteLine("Starting...");

            System.Threading.Timer nTimer = new System.Threading.Timer(
                callback: action.DoThis, 
                state: autoEvent, 
                dueTime: 1000, 
                period:  15000);
            autoEvent.WaitOne();
            // nTimer.Change(0, 500);
        }

        static void StartGui()
        {
            Application.Run(new AppForm());
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
            //aps = new ApplicationSettings();
            //aps.FileServerPath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/";
            //aps.Save();
            //filePath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/2018/september/9/";
            string filePath = appSettings.FileServerPath + "/2018/september/9/";
            string brewNumber = "258";

            LiveBrewMonitor liveBrewMonitor = LiveBrewMonitor.GetInstance();
            liveBrewMonitor.StartMonitoring(filePath, "Maltina", brewNumber);
            // Thread.Sleep(1000);
        }
    }
}