using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    public class TaskConfiguration : DisabledConfiguration
    {
        [ConfigurationProperty("taskQualifiedName", IsRequired = true, IsKey = true)]
        public string TaskQualifiedName
        {
            get { return Convert.ToString(this["taskQualifiedName"]); }
            set { this["taskQualifiedName"] = value; }
        }

        [ConfigurationProperty("continueOnErrors", IsRequired = false, DefaultValue = false)]
        public bool ContinueOnErrors
        {
            get { return Convert.ToBoolean(this["continueOnErrors"]); }
            set { this["continueOnErrors"] = value; }
        }

        [ConfigurationProperty("waitTimeInSeconds", IsRequired = false, DefaultValue = 0)]
        public int WaitTimeInSeconds
        {
            get { return (int)this["waitTimeInSeconds"]; }
            set { this["waitTimeInSeconds"] = value; }
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

        public ITask GetTask()
        {
            if (string.IsNullOrWhiteSpace(TaskQualifiedName)) throw new ArgumentException("Invalid TaskQualifiedName value");
            var assemblyInfo = TaskQualifiedName.Split(",".ToCharArray());
            var assemblyType = assemblyInfo[0];
            if (assemblyInfo.Length <= 1) throw new ArgumentException("Invalid TaskQualifiedName value");
            var assemblyName = assemblyInfo[1];
            var objectHandle = Activator.CreateInstance(assemblyName, assemblyType);
            if (objectHandle == null) throw new ArgumentException("Invalid TaskQualifiedName value");
            var task = (ITask)objectHandle.Unwrap();
            return task;
        }
    }
}
