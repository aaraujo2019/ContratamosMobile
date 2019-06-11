using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Contratamos.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        string subtitle = string.Empty;

        public string Subtitle
        {
            get { return subtitle; }
            set { SetProperty(ref subtitle, value); }
        }

        string icon = string.Empty;

        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value); }
        }

        bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
            }
        }

        bool isNotBusy = true;
        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set
            {
                SetProperty(ref isNotBusy, value);
            }
        }

        protected bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
