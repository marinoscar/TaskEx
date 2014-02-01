using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEx.Configuration;

namespace TaskEx
{
    public class TaskRunner
    {

        private TaskConfigurationManager _taskConfigurationManager;


        public TaskRunner() : this(new TaskRunTimeRepository())
        {
            
        }

        public TaskRunner(ITaskRunTimeRepository taskRunTimeRepository)
        {
            Specifications = new List<ISpecification>();
            _taskConfigurationManager = new TaskConfigurationManager();
            foreach (var specificationConfiguration in _taskConfigurationManager.GetSpecifications())
            {
                Specifications.Add(new TaskSpecification(specificationConfiguration.Name, taskRunTimeRepository));
            }

        }
        public List<ISpecification> Specifications { get; set; }


        public void RunAll()
        {
            Specifications.Where(i => !i.Disabled).ToList().ForEach(i => i.ExecuteSpecification());
        }
    }
}
