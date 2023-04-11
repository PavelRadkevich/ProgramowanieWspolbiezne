using System.Windows;

namespace Prezentacja.ViewModel
{
    internal class Implementations
    {
        public ButtonStart ButtonStart { get; set; }
        public ButtonStop ButtonStop { get; set; } 
        public Implementations() { 
            ButtonStart = new ButtonStart(this);
            ButtonStop = new ButtonStop(this);
        }
        public void ButtonStartClick()
        {
            MessageBox.Show("Button START is clicked");
        }
        
        public void ButtonStopClick()
        {
            MessageBox.Show("Button STOP is clicked");
        }
    }
}
