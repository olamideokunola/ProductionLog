using System;
using System.Collections.Generic;

namespace BrewingModel.BrewMonitor
{
    public interface IFileParser
    {
        void Initialize(string filePath, string brewNumber);
        IDictionary<string, IDictionary<string, string>> GetSectionFields();
        IDictionary<string, string> GetHeaderFields();
    }
}
