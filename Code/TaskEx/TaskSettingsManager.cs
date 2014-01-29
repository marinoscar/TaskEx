using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEx.Configuration;

namespace TaskEx
{
    public class TaskSettingsManager : SettingsManager
    {
        public TaskSettingsManager(ITask task)
            : base(GetSettings(task))
        {
        }

        private static SettingConfigurationCollection GetSettings(ITask task)
        {
            var configManager = new TaskConfigurationManager();
            var taskConfig = configManager.GetTask(task.Specification.Name, task.Name);
            return taskConfig.Settings;
        }
    }
}
