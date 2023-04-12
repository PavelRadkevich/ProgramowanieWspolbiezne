using System;
using System.Windows.Input;

namespace Prezentacja.ViewModel
{
    public class ButtonStart : ICommand
    {
        Model.Model model;
        public ButtonStart(Model.Model model)
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
