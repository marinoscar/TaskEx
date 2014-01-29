using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Model
{
    public class TaskRunTime
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UtcLastRunAt { get; set; }
    }
}
