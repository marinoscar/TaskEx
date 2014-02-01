using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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

        public ITask GetTask(ISpecification specification)
        {
            if (string.IsNullOrWhiteSpace(TaskQualifiedName)) throw new ArgumentException("Invalid TaskQualifiedName value");
            var assemblyInfo = TaskQualifiedName.Split(",".ToCharArray());
            var assemblyType = assemblyInfo[0].Trim();
            if (assemblyInfo.Length <= 1) throw new ArgumentException("Invalid TaskQualifiedName value");
            var assemblyName = assemblyInfo[1].Trim();
            var ass = AppDomain.CurrentDomain.Load(assemblyName);
            var assType = ass.GetType(assemblyType);
            var obj = Activator.CreateInstance(assType, new object[] { specification });
            if (obj == null) throw new ArgumentException("Invalid TaskQualifiedName value");
            var task = (ITask)obj;
            return task;
        }
    }
}
