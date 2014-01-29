using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx
{
    public interface ISpecification : IDisposable, ISettings, IDisabled
    {
        string Name { get; }
        DateTime UtcStartedAt { get; }
        IList<ITask> Tasks { get; }
        void ExecuteSpecification();

    }
}
