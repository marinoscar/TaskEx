using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    public class SpecificationConfiguration : DisabledConfiguration
    {
        [ConfigurationProperty("tasks", IsRequired = true, IsDefaultCollection = false)]
        public TaskConfigurationCollection Tasks
        {
            get
            {
                return (TaskConfigurationCollection)this["tasks"] ??
                       new TaskConfigurationCollection();
            }
        }

        [ConfigurationProperty("settings", IsRequired = true, IsDefaultCollection = false)]
        public SettingConfigurationCollection Settings
        {
            get
            {
                return (SettingConfigurationCollection)this["settings"] ??
                       new SettingConfigurationCollection();
            }
        }
    }
}
