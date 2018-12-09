using System;
namespace BrewLogGui.Observer
{
    public interface IObserverSubject
    {
        // Observer Pattern Interface
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }
}
