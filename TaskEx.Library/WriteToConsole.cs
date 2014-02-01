using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Library
{
    public class WriteToConsole : TaskBase
    {
        public WriteToConsole(ISpecification specification) : base(specification)
        {
        }

        public override void Dispose()
        {
        }

        public override void ExecuteTask()
        {
            var message = Specification.TryGetSetting("ConsoleOut", string.Empty);
            Console.WriteLine(message);
        }
    }
}
