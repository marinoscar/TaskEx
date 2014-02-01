using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx
{
    public class TaskQualifiedName
    {
        private readonly ITask _task;

        public TaskQualifiedName(ITask task)
        {
            _task = task;
        }

        public string GetName()
        {
            return GetName(_task);
        }

        public string GetInvariantName()
        {
            return GetInvariantName(_task);
        }

        public static string GetName(ITask task)
        {
            var assembly = task.GetType().Assembly.FullName.Split(",".ToCharArray())[0];
            var type = task.GetType().FullName;
            return string.Format("{0},{1}", type, assembly);
        }

        public static string GetInvariantName(ITask task)
        {
            return GetName(task).ToLowerInvariant().Replace(" ", "");
        }
    }
}
