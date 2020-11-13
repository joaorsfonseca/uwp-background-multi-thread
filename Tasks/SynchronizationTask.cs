using App2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Tasks
{
    public class SynchronizationTask : BindableBase
    {
        public string Name { get; set; }
        
        private bool _isWaiting;
        public bool IsWaiting
        {
            get => _isWaiting;
            set
            {
                Set(ref _isWaiting, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public bool InError { get; set; }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                Set(ref _progress, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        public bool IsEnabled => !IsWaiting && !(this.Progress < 100);

        public void Init()
        {
            IsWaiting = true;
            Message = "Pending";
        }
    }
}
