using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEx.Configuration;
using TaskEx.Model;

namespace TaskEx
{
    public class TaskSpecification : ISpecification
    {

        private readonly SpecificationSettingsManager _settingsManager;
        private readonly TaskConfigurationManager _configurationManager;
        private readonly ITaskRunTimeRepository _taskRunTimeRepository;

        public TaskSpecification(string name, ITaskRunTimeRepository taskRunTimeRepository)
        {
            _configurationManager = new TaskConfigurationManager();
            _taskRunTimeRepository = taskRunTimeRepository;
            Tasks = new List<ITask>();
            UtcStartedAt = DateTime.UtcNow;
            Name = name;
            Configuration = _configurationManager.GetSpecification(Name);
            Settings = new Dictionary<string, object>();
            _settingsManager = new SpecificationSettingsManager(this);
            LoadTasks();
        }

        private void LoadTasks()
        {
            foreach (var task in Configuration.Tasks.Cast<TaskConfiguration>().OrderBy(i => i.Order))
            {
                var taskItem = task.GetTask(this);
                taskItem.TaskRunTime = _taskRunTimeRepository.GetFromStorage(Name, task.Name);
                Tasks.Add(taskItem);
            }
        }

        #region Property Implementation

        public SpecificationConfiguration Configuration { get; private set; }
        public bool Disabled { get; set; }
        public string Name { get; private set; }
        public DateTime UtcStartedAt { get; private set; }
        public IList<ITask> Tasks { get; private set; }
        public Dictionary<string, object> Settings { get; private set; }

        #endregion

        #region Method Implementation

        public void Dispose()
        {
            Tasks.Clear();
        }

        public T GetSetting<T>(string name)
        {
            return _settingsManager.GetSetting<T>(name);
        }

        public T TryGetSetting<T>(string name, T defaultValue = default(T))
        {
            return _settingsManager.TryGetSetting<T>(name, defaultValue);
        }

        public void ExecuteSpecification()
        {
            foreach (var task in Tasks.Where(i => !i.Disabled))
            {
                var config = GetTaskConfiguration(task.Name);
                var lastTime = task.TaskRunTime.UtcLastRunAt;
                var elapsedTime = UtcStartedAt.Subtract(lastTime).TotalSeconds;
                if (config.WaitTimeInSeconds > 0 && elapsedTime < config.WaitTimeInSeconds)
                    continue;
                task.Execute();
                _taskRunTimeRepository.Persist(new TaskRunTime()
                    {
                        SpecifiactionName = Name, TaskName = task.Name, UtcLastRunAt = UtcStartedAt
                    });
            }
        } 

        private TaskConfiguration GetTaskConfiguration(string taskName)
        {
            return Configuration.Tasks.Cast<TaskConfiguration>().Single(i => i.Name == taskName);
        }

        #endregion
    }
}
