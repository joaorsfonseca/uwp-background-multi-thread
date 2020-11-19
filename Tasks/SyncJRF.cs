using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App2.Tasks
{
    public class SyncJRF : SynchronizationTask, ISynchronizationTask
    {
        public SyncJRF() { }
        public SyncJRF(string Name) : this()
        {
            this.Name = Name;
        }

        public void Run()
        {
            base.Starting();

            Debug.WriteLine($"Running {this.Name}");
            DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Message = "Running";
            });

            Thread.Sleep(5000);
            DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                HasWarnings = this.Name == "JRF1";
            });
            Debug.WriteLine($"Waking {this.Name}");

            DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                InError = this.Name == "JRF2";
            });

            DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                IsWaiting = false;
                Progress = 100;
                Message = "Finished...";
            });
        }
    }
}
