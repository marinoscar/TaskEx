using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace TaskEx.Configuration
{
    public class TaskConfigurationManager
    {
        private TaskSpecificationSection _section;

        public TaskConfigurationManager()
        {
            _section = GetSection();
        }

        private TaskSpecificationSection GetSection()
        {
            var section = ConfigurationManager.GetSection("taskSpecificationSection");
            if(section == null)
                throw new InvalidOperationException("Section not specified");
            return (TaskSpecificationSection) section;
        }

        public SpecificationConfiguration GetSpecification(string name)
        {
            return _section.Specifications.Cast<SpecificationConfiguration>().FirstOrDefault(spec => spec.Name == name);
        }

        public TaskConfiguration GetTask(string specificationName, string name)
        {
            var spec = GetSpecification(specificationName);
            return spec.Tasks.Cast<TaskConfiguration>().FirstOrDefault(i => i.Name == name);
        }

        public Dictionary<string, object> GetTaskSettings(string specificationName, string name)
        {
            var task = GetTask(specificationName, name);
            return GetSettings(task.Settings);
        }

        public Dictionary<string, object> GetSpecificationSettings(string specificationName, string name)
        {
            var spec = GetSpecification(specificationName);
            return GetSettings(spec.Settings);
        } 

        public Dictionary<string, object> GetSettings(SettingConfigurationCollection settings)
        {
            var result = settings.Cast<SettingConfiguration>();
            return result.ToDictionary(setting => setting.Name, setting => setting.GetValue());
        } 
    }
}
