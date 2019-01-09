using System;
namespace BrewLogGui
{
    public interface IGui
    {
        void SetupView();
        void Render();
        void Clear();
        void SetPos(int x, int y);
    }
}
