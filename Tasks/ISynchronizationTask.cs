using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Tasks
{
    public interface ISynchronizationTask
    {
        string Name { get; set; }

        bool IsWaiting { get; set; }

        bool InError { get; set; }

        string Message { get; set; }

        int Progress { get; set; }

        void Run();
    }
}
