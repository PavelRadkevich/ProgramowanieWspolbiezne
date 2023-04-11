﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Prezentacja.ViewModel
{
    internal class ObserveTextBox : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
