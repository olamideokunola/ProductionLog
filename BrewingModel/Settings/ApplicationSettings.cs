using System;
using System.Configuration;

namespace BrewingModel.Settings
{
    //[SettingsProvider("LocalFileSettingsProvider")]
    public class ApplicationSettings : ApplicationSettingsBase
    {
        public ApplicationSettings()
        {
        }

        [ApplicationScopedSetting()]
        //[DefaultSettingValue("/home/olamide/Projects/BrewLog/BrewLog/bin/Debug/brewing data/")]
        public string FileServerPath
        {
            get
            {
                return ((string)this["FileServerPath"]);
            }
            set
            {
                this["FileServerPath"] = (string)value;
            }
        }

        [ApplicationScopedSetting()]
        //[DefaultSettingValue("/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug")]
        public string ConnectionString
        {
            get
            {
                return ((string)this["ConnectionString"]);
            }
            set
            {
                this["ConnectionString"] = (string)value;
            }
        }

        [ApplicationScopedSetting()]
        //[DefaultSettingValue("/home/olamide/Projects/BrewLog/BrewingModel/bin/Debug/period_template.xlsx")]
        public string TemplateFilePath
        {
            get
            {
                return ((string)this["TemplateFilePath"]);
            }
            set
            {
                this["TemplateFilePath"] = (string)value;
            }
        }
    }
}
