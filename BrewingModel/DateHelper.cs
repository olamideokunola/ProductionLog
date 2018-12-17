using System;
namespace BrewingModel
{
    public class DateHelper
    {
        //Singleton
        private static DateHelper _uniqueInstance = null;

        //lazy construction of instance
        public static DateHelper GetInstance()
        {
            if (_uniqueInstance == null)
            {
                _uniqueInstance = new DateHelper();
            }

            return _uniqueInstance;
        }

        // Other attributes
        private static string pattern = "dd.MM.yyyy HH:mm:ss";

        public DateHelper()
        {
        }

        public static DateTime GetProcessParameterDateTime(string processParameterDateTimeString)
        {
            DateTime processParameterDateTime;

            processParameterDateTime = ConvertStringToDateTime(processParameterDateTimeString);

            return processParameterDateTime;
        }

        public static DateTime ConvertStringToDateTime(string dateStr)
        {
            DateTime convertedDate;
            convertedDate = DateTime.ParseExact(dateStr, pattern, null);

            return convertedDate;
        }
    }
}
