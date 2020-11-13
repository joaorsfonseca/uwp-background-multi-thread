using App2.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App2.ViewModels
{
    public class SyncResumePageViewModel : BindableBase
    {
        public List<ISynchronizationTask> Tasks { get; } = new List<ISynchronizationTask>();

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                Set(ref _isRunning, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public bool IsEnabled => !IsRunning;

        private string _number;
        public string Number
        {
            get => _number;
            set
            {
                Set(ref _number, value);
                OnPropertyChanged(nameof(NumberDisplay));
            }
        }

        public string NumberDisplay => string.IsNullOrEmpty(Number) ? null : "#" + Number;

        public SyncResumePageViewModel()
        {
            Tasks.Add(new SyncJRF("JRF1")
            {
                Message = "JRF1 Message"
            });
            Tasks.Add(new SyncJRF("JRF2")
            {
                Message = "JRF2 Message"
            });
        }

        public void StartSync()
        {
            IsRunning = true;
            foreach (var t in Tasks)
            {
                t.Progress = 0;
                t.IsWaiting = true;
                t.Message = "Pending";
            }

            Thread th = new Thread(new ThreadStart(() =>
            {
                foreach (var t in Tasks)
                    t.Run();

                DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    IsRunning = false;
                });
            }));
            th.Start();
        }

        public void StartTask(SynchronizationTask task)
        {
            task.IsWaiting = true;
            task.Progress = 0;
            task.Message = "Pending";

            Thread th = new Thread(new ThreadStart(() =>
            {
                (task as ISynchronizationTask).Run();
            }));
            th.Start();
        }
    }
}
