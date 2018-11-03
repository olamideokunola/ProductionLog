﻿using System;
using BrewingModel;

namespace BrewMonitor
{
    public class CompleteMashCopperProcessStepCommand : LiveBrewCommand
    {
        string _brandName;
        string _brewNumber;
        string _fieldName;
        string _fieldValue;
        string _fieldSection;

        public CompleteMashCopperProcessStepCommand(string brandName, string brewNumber, string fieldName, string fieldValue, string fieldSection)
        {
            this._brandName = brandName;
            this._brewNumber = brewNumber;
            this._fieldName = fieldName;
            this._fieldValue = fieldValue;
            this._fieldSection = fieldSection;
            this.brewingProcessHandler = BrewingProcessHandler.GetInstance();
        }

        public override void Execute()
        {
            this.brewingProcessHandler.CompleteMashCopperProcessStep(_brewNumber, _fieldName, _fieldValue);
        }

        public override bool IsReversible()
        {
            return false;
        }

        public override void UnExecute()
        {

        }
    }
}