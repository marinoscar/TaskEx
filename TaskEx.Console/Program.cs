using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskRunner = new TaskRunner();
            taskRunner.RunAll();
        }
    }
}
