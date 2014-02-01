using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEx.Model;

namespace TaskEx
{
    public interface ITask : IDisposable, ISettings, IDisabled
    {
        TaskRunTime TaskRunTime { get; set; }
        ISpecification Specification { get; }
        string Name { get; }
        void Execute();
    }
}
