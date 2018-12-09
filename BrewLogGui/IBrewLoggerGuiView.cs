using System;
using BrewLogGui.Models;
namespace BrewLogGui
{
    public interface IBrewLoggerGuiView
    {
        // Observer Pattern Interface
        void Update(IBrewLoggerGuiModel guiModel);
    }
}
