using System;
using NUnit.Framework;
using BrewMonitor;
using System.Collections.Generic;
namespace BrewMonitorTest
{
    [TestFixture()]
    public class FileParserTest
    {
        public FileParserTest()
        {
        }

        //[Test]
        //public void GetRawHeaderLinesTest()
        //{
        //    string filePath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/2018/september/9/";
        //    string brewNumber = "258";
        //    FileParser fileParser = new FileParser(filePath, brewNumber);

        //    IDictionary<int, string> rawHeaderLines = new Dictionary<int, string>();
        //    rawHeaderLines = fileParser.GetRawHeaderLines();

        //    Assert.True(rawHeaderLines.Count == 8);
        //}

        [Test]
        public void GetHeaderFieldsTest()
        {
            string filePath = "/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/2018/september/9/";
            string brewNumber = "258";
            FileParser fileParser = new FileParser();
            fileParser.Initialize(filePath, brewNumber);

            IDictionary<string, string> headerFields = new Dictionary<string, string>();
            headerFields = fileParser.GetHeaderFields();

            Assert.True(headerFields.Count == 8);
        }
    }
}
