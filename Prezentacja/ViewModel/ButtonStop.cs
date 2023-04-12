using System;
using System.Windows.Input;

namespace Prezentacja.ViewModel
{
    public class ButtonStop : ICommand
    {
        Model.Model model;
        public ButtonStop(Model.Model model)
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
