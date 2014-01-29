using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEx.Configuration;
using TaskEx.Model;

namespace TaskEx
{
    public abstract class TaskBase : ITask
    {
        protected TaskBase()
        {
            var config = new TaskConfigurationManager();
            Settings = config.GetTaskSettings(Specification.Name, Name);
            _taskSettingsManager = new TaskSettingsManager(this);
        }

        private SettingsManager _taskSettingsManager;

        #region Properties
        
        public Dictionary<string, object> Settings { get; private set; }
        public bool Disabled { get; set; }
        public TaskRunTime TaskRunTime { get; private set; }
        public ISpecification Specification { get; private set; }
        public abstract string Name { get; } 

        #endregion

        #region Method Implementation

        public T GetSetting<T>(string name)
        {
            return _taskSettingsManager.GetSetting<T>(name);
        }

        public T TryGetSetting<T>(string name, T defaultValue = default(T))
        {
            return _taskSettingsManager.TryGetSetting<T>(name, defaultValue);
        }

        public abstract void Dispose();

        protected void OnExecutingTask()
        {
            
        }

        protected void OnExecutedTask()
        {
            
        }

        public abstract void ExecuteTask();

        public void Execute()
        {
            OnExecutingTask();
            ExecuteTask();
            OnExecutedTask();
        }

        #endregion
    }
}
