﻿using System;
using BrewingModel;

namespace BrewingModel.BrewMonitor.LiveBrewCommands
{
    public abstract class LiveBrewCommand
    {
        protected BrewingProcessHandler brewingProcessHandler;

        public abstract void Execute();
        public abstract void UnExecute();
        public abstract Boolean IsReversible();
    }
}
