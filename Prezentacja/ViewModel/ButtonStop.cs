using System;
using System.Windows.Input;

namespace Prezentacja.ViewModel
{
    internal class ButtonStop : ICommand
    {
        Implementations model;
        public ButtonStop (Implementations model)
        {
            this.model = model;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            model.ButtonStopClick();
        }
    }
}
