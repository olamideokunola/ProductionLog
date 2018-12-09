using System;
using System.Collections.Generic;

namespace BrewLogGui.Observer
{
    public class ObserverSubject : IObserverSubject
    {
        private IList<IObserver> _observers = new List<IObserver>();

        public ObserverSubject()
        {

        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach(IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

    }
}
