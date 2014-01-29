using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx
{
    public class TaskRunner
    {
        public List<ISpecification> Specifications { get; set; }

        public void RunAll()
        {
            Specifications.Where(i => !i.Disabled).ToList().ForEach(i => i.ExecuteSpecification());
        }
    }
}
