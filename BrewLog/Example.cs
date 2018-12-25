using System;
using System.Timers;
using BrewingModel.BrewMonitor;

public class Example
{
    private static System.Timers.Timer aTimer;
    private static LiveBrewMonitor liveBrewMonitor = LiveBrewMonitor.GetInstance();

    //public static void Main()
    //{
    //    SetTimer();

    //    Console.WriteLine("\nPress the Enter key to exit the application...\n");
    //    Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
    //    Console.ReadLine();
    //    aTimer.Stop();
    //    aTimer.Dispose();

    //    Console.WriteLine("Terminating the application...");
    //}

    private static void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(10000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = false;
        aTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        string filePath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/2018/september/9/";
        string brewNumber = "258";

        liveBrewMonitor.StartMonitoring(filePath, "Maltina", brewNumber);
        Console.WriteLine("Back in OnTimedEvent");
        Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                          e.SignalTime);
        Console.WriteLine("After message");
        aTimer.Start();
    }
}
