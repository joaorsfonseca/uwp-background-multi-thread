using App2.ViewModels;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

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

        private bool _inError;
        public bool InError
        {
            get => _inError;
            set
            {
                Set(ref _inError, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        private bool _hasWarnings;
        public bool HasWarnings
        {
            get => _hasWarnings;
            set
            {
                Set(ref _hasWarnings, value);
                OnPropertyChanged(nameof(Color));
            }
        }

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

        public SolidColorBrush Color
        {
            get
            {
                //if(HasWarnings && !IsWaiting && !(this.Progress<100))
                if(HasWarnings)
                    return new SolidColorBrush(Colors.Orange);

                return new SolidColorBrush(Colors.Blue);
            }
        }

        public void Init()
        {
            IsWaiting = true;
            HasWarnings = false;
            Progress = 0;
            Message = "Pending";
        }

        public void Starting()
        {
            DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                IsWaiting = false;
                HasWarnings = false;
                Progress = 0;
                Message = "Running...";
            });
        }
    }
}
