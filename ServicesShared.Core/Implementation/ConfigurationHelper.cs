using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Serilog;

namespace ServicesShared.Core
{
    public class ConfigurationHelper
    {
        private IList<string> keys;

        public ConfigurationHelper(string assemblyPath = null)
        {
            Configuration =
                !assemblyPath.IsNullOrEmpty()
                    ? ConfigurationManager.OpenExeConfiguration(assemblyPath)
                    : ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        #region Properties
      

        /// <summary>
        /// Gets a configuration.
        /// </summary>
        private Configuration Configuration { get; }

        #endregion

        public string GetStringValue([CallerMemberName] string appKey = null)
        {
            string value = null;
            try
            {
                if (keys == null)
                {
                    keys = this.Configuration.AppSettings.Settings.AllKeys.ToList();
                }

                if(keys.Contains(appKey))
                {
                    value = this.Configuration.AppSettings.Settings[appKey].Value;
                }
            }
            catch (NullReferenceException exception)
            {
                Log.Error(string.Format("Value of appKey {1} is missing.{0}Error message:{0}{2}", Environment.NewLine, appKey, exception));
                throw;
            }
            catch (ConfigurationErrorsException exception)
            {
                Log.Error(string.Format("Failed to read {1} from the configuration file.{0}Error message:{0}{2}", Environment.NewLine, appKey, exception));
                throw;
            }

            return value;
        }

        public int GetIntValue([CallerMemberName] string appKey = null, int defaultValue = -1, bool absenceAllowed = false)
            => GetIntValue(appKey, () => defaultValue, absenceAllowed);

        public int GetIntValue(string appKey, Func<int> defaultValueFunc, bool absenceAllowed = false)
        {
            int result = default(int);

            string value = GetStringValue(appKey, absenceAllowed);
            if (!value.IsNullOrEmpty())
            {
                if (!int.TryParse(GetStringValue(appKey), out result) && !absenceAllowed)
                {
                    LogWrongValue(appKey);
                    if (defaultValueFunc != null)
                    {
                        result = defaultValueFunc();
                    }
                }
            }

            return result;
        }

        public bool GetBoolValue([CallerMemberName] string appKey = null, bool defaultValue = false, bool absenceAllowed = false)
        {
            bool result = default(bool);

            string value = GetStringValue(appKey, absenceAllowed);
            if (!value.IsNullOrEmpty())
            {
                if (!bool.TryParse(value, out result))
                {
                    LogWrongValue(appKey);
                    result = defaultValue;
                }
            }

            return result;
        }

        public DateTime? GetDateTimeValue([CallerMemberName] string appKey = null, DateTime defaultValue = default(DateTime), bool absenceAllowed = false)
        {
            DateTime? result = null;

            string value = GetStringValue(appKey, absenceAllowed);
            if (!value.IsNullOrEmpty())
            {
                DateTime date;
                if (!DateTime.TryParse(value, out date) && !absenceAllowed)
                {
                    LogWrongValue(appKey);
                    result = defaultValue;
                }
                else
                {
                    result = date;
                }
            }

            return result;
        }

        public string GetFileContent([CallerMemberName] string appKey = null)
        {
            string content = string.Empty;

            string file = GetStringValue(appKey, null);
            if (file != null && !file.IsNullOrEmpty())
            {
                try
                {
                    string directoryName = Path.GetDirectoryName(file);
                    if (directoryName != null && directoryName.Length == 0)
                    {
                        file = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName(file));
                    }

                    if (File.Exists(file))
                    {
                        content = File.ReadAllText(file);
                        if (!content.IsNullOrEmpty())
                        {
                            Log.Debug("Custom response content:");
                            Log.Debug(content);
                        }
                    }
                    else
                    {
                        Log.Debug($"Custom response file '{file}' doesn't exist or isn't accessible.");
                    }
                }
                catch (IOException exception)
                {
                    Log.Error(string.Format("Reading of '{1}' file content is failed.{0}Error message:{0}{2}", Environment.NewLine, appKey, exception));
                }
            }
            else
            {
                Log.Debug($"Settings '{appKey}' is empty.");
            }

            return content;
        }                

        private string GetStringValue(string appKey, bool? absenceAllowed)
        {
            string value = GetStringValue(appKey);
            if (value.IsNullOrEmpty() && absenceAllowed.HasValue && !absenceAllowed.Value)
            {
                Log.Information($"Value of appKey '{appKey}' is not set.");
            }

            return value;
        }
        private void LogWrongValue(string appKey) => Log.Error($"Value of appKey '{appKey}' is wrong.");
    }
}
