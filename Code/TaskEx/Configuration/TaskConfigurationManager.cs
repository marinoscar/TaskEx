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
            var section = ConfigurationManager.GetSection("taskConfiguration");
            if(section == null)
                throw new InvalidOperationException("Section not specified");
            return (TaskSpecificationSection) section;
        }

        public SpecificationConfiguration GetSpecification(string name)
        {
            return _section.Specifications.Cast<SpecificationConfiguration>().FirstOrDefault(spec => spec.Name == name);
        }

        public IEnumerable<SpecificationConfiguration> GetSpecifications()
        {
            return _section.Specifications.Cast<SpecificationConfiguration>();
        }

        public TaskConfiguration GetTask(string specificationName, string qualifiedName)
        {
            var spec = GetSpecification(specificationName);
            return spec.Tasks.Cast<TaskConfiguration>().FirstOrDefault(i => PrepareQualifiedNameForComparison(i.TaskQualifiedName) == PrepareQualifiedNameForComparison(qualifiedName));
        }

        private string PrepareQualifiedNameForComparison(string qualifiedName)
        {
            var newStr = new string(qualifiedName.ToCharArray());
            return newStr.Replace(" ", string.Empty).ToLowerInvariant();
        }

        public Dictionary<string, object> GetTaskSettings(string specificationName, string qualifiedName)
        {
            var task = GetTask(specificationName, qualifiedName);
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
