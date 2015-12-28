using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstiLibrary.Helper.Configuration
{
    public static class ConfigWrapper
    {
        public static string LogDirectoryName = "LogDirectory";
        public static string LogLevelName = "LogLevel";
        private static string AppSettingsName = "appSettings";
        public static string[] PossibleTypes = {"string","int", "long","decimal","double","float","bool"}; 

        /// <summary>
        /// Get the Value of the generic Configsetting. PossibleTypes you see in the PossibleTypes (string[]).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public static object GetSetting<T>(string settingName)
        {
            switch (typeof(T).ToString().ToLower())
            {
                case "string":
                    try { return ConfigurationManager.AppSettings[settingName]; } catch { return null; }
                case "int":
                    try { return ParseInt(ConfigurationManager.AppSettings[settingName]); } catch { return null; }
                case "long":
                    try { return ParseLong(ConfigurationManager.AppSettings[settingName]); } catch { return null; }
                case "decimal":
                    try { return ParseDecimal(ConfigurationManager.AppSettings[settingName]); } catch { return null; }
                case "double":
                    try { return ParseDouble(ConfigurationManager.AppSettings[settingName]); } catch { return null; }
                case "float":
                    try { return ParseFloat(ConfigurationManager.AppSettings[settingName]); } catch { return null; }
                case "bool":
                    try { return ParseBool(ConfigurationManager.AppSettings[settingName]); } catch { return null; }
                default:
                    return null;
            }
        }
        public static bool SetSetting<T>(string settingName, T value)
        {
            bool success = false;
            switch (typeof(T).ToString().ToLower())
            {
                case "string":
                    return SetSetting(settingName, value);
                case "int":
                    return SetSetting(settingName, value.ToString());
                case "long":
                    return SetSetting(settingName, value.ToString());
                case "double":
                    return SetSetting(settingName, value.ToString());
                case "decimal":
                    return SetSetting(settingName, value.ToString());
                case "float":
                    return SetSetting(settingName, value.ToString());
                case "bool":
                    return SetSetting(settingName, value.ToString());
            }
            return success;
        }

        private static bool SetSetting(string settingName, string value)
        {
            bool success = true;
            try
            {
                System.Configuration.ConfigurationManager.AppSettings[settingName] = value;
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[settingName].Value = value;
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection(AppSettingsName);
            }
            catch (Exception)
            {
                success = false;
            }
            
            return success;
        }

        public static string LogDirectory
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings[LogDirectoryName];
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings[LogDirectoryName] = value;
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[LogDirectoryName].Value = value;
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection(AppSettingsName);
            }
        }
        public static int LogLevel
        {
            get
            {
                return ParseInt(ConfigurationManager.AppSettings[LogLevelName]);
            }
            set
            {
                ConfigurationManager.AppSettings[LogLevelName] = value.ToString();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[LogLevelName].Value = value.ToString();
                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection(AppSettingsName);
            }
        }




        private static int ParseInt(string intValue)
        {
            int value = 0;
            Int32.TryParse(intValue, out value);
            return value;
        }
        private static long ParseLong(string intValue)
        {
            long value = 0;
            Int64.TryParse(intValue, out value);
            return value;
        }
        private static decimal ParseDecimal(string decimalValue)
        {
            decimal value = 0.0m;
            Decimal.TryParse(decimalValue, out value);
            return value;
        }
        private static double ParseDouble(string doubleValue)
        {
            double value = 0.0d;
            Double.TryParse(doubleValue, out value);
            return value;
        }
        private static float ParseFloat(string floatValue)
        {
            float value = 0.0f;
            float.TryParse(floatValue, out value);
            return value;
        }
        private static bool ParseBool(string boolValue)
        {
            bool value = false;
            Boolean.TryParse(boolValue,out value);
            return value;
        }

    }
}
