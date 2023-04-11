using System;
using System.Windows.Input;

namespace Prezentacja.ViewModel
{
    internal class ButtonStart : ICommand
    {
        Implementations model;
        public ButtonStart(Implementations model) 
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
            model.ButtonStartClick();
        }
    }
}
