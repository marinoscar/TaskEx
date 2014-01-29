using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEx.Configuration;

namespace TaskEx
{
    public abstract class SettingsManager : ISettings
    {

        protected SettingsManager(SettingConfigurationCollection settings)
        {
            var configManager = new TaskConfigurationManager();
            Settings = configManager.GetSettings(settings);
        }

        public Dictionary<string, object> Settings { get; private set; }

        public T GetSetting<T>(string name)
        {
            if (Settings.ContainsKey(name))
                return (T)Convert.ChangeType(Settings[name], typeof (T));
            throw new InvalidOperationException(string.Format("Setting {0} not found", name));
        }

        public T TryGetSetting<T>(string name, T defaultValue = default(T))
        {
            T value;
            try
            {
                value = GetSetting<T>(name);
            }
            catch (Exception)
            {
                return defaultValue;
            }
            return value;
        }
    }
}
