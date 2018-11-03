﻿using System;
using BrewingModel;
namespace BrewMonitor
{
    public class StartMashTunMashingInCommand : LiveBrewCommand
    {
        string _startDate;
        string _startTime;
        string _brandName;
        string _brewNumber;
        string _fieldName;
        string _fieldValue;
        string _fieldSection;

        public StartMashTunMashingInCommand(string startDate, string startTime, string brandName, string brewNumber, string fieldName, string fieldValue, string fieldSection)
        {
            this._startDate = startDate;
            this._startTime = startTime;
            this._brandName = brandName;
            this._brewNumber = brewNumber;
            this._fieldName = fieldName;
            this._fieldValue = fieldValue;
            this._fieldSection = fieldSection;
            this.brewingProcessHandler = BrewingProcessHandler.GetInstance();
        }

        public override void Execute()
        {
            this.brewingProcessHandler.StartMashTunMashingIn(_startTime, _brewNumber, _fieldName, _fieldValue);
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
