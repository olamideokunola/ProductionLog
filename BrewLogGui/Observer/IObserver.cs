using System;
using BrewLogGui.Models;

namespace BrewLogGui.Observer
{
    public interface IObserver
    {
        // Observer Pattern Interface
        void Update(IObserverSubject observerSubject);
    }
}
