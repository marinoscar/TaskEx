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
        protected TaskBase(ISpecification specification)
        {
            Specification = specification;
            var config = new TaskConfigurationManager();
            Settings = config.GetTaskSettings(Specification.Name, GetQualifiedName());
            Configuration = config.GetTask(Specification.Name, GetQualifiedName());
            Name = Configuration.Name;
            _taskSettingsManager = new TaskSettingsManager(this);

        }

        private readonly SettingsManager _taskSettingsManager;

        #region Properties
        
        public Dictionary<string, object> Settings { get; private set; }
        public bool Disabled { get; set; }
        public TaskRunTime TaskRunTime { get; set; }
        public ISpecification Specification { get; private set; }
        public virtual string Name { get; private set; }
        public TaskConfiguration Configuration { get; private set; }

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

        private string GetQualifiedName()
        {
            return TaskQualifiedName.GetInvariantName(this);
        }

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
