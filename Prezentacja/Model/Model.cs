using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Prezentacja.Model
{
    public class Model : ViewModel.ObserveTextBox
    {
		private int amount_;
        private int speed_;
        public ViewModel.ButtonStart ButtonStart { get; private set; }
        public ViewModel.ButtonStop ButtonStop { get; private set; }
        public Logika.Logika Logika { get; private set; }
        public Window Window { private get; set; }
        public Canvas canvas { private get; set; }
        private List<Ellipse> ellipses = new List<Ellipse>();
        public int speed { 
            get { return speed_; }
            set
            {
                speed_= value;
                OnPropertyChanged();
            }
            }
        public int amount
        {
            get { return amount_; }
            set
            {
                amount_ = value;
                OnPropertyChanged();
            }
        }
        public int GetCountEllipses()
        {
            return ellipses.Count;
        }
        public void ButtonStartClick()
        {
            Logika.moving = true;
            canvas = (Canvas)Window.FindName("CanvasMyWindow");
            Drawing();
        }

        public void Drawing()
        {
            Random rnd = new Random();
            for (int i = 0; i < amount_ - ellipses.Count; i++)
            {
                ellipses.Add(Logika.NarysujKule(Window, rnd));
            }
        }

        public void ButtonStopClick()
        {
            Logika.moving = false;
        }

        public void CheckEllipses ()
        {
            if (ellipses.Count < amount_)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Drawing();
                });
            }
            else if (ellipses.Count > amount_)
            {
                while (ellipses.Count > amount_)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        canvas.Children.Remove(ellipses[0]);
                        ellipses.RemoveAt(0);
                    });
                }
            }
        }

        public void DeleteEllipse(Ellipse ellipse)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                canvas.Children.Remove(ellipse);
                ellipses.Remove(ellipse);
            });
        }

		public Model()
		{
            ButtonStart = new ViewModel.ButtonStart(this);
            ButtonStop = new ViewModel.ButtonStop(this);
            Logika = new Logika.Logika();
			Task.Run(() =>
			{
				while (true)
				{
                    CheckEllipses();
                    if (ellipses.Count != 0) {
                        Random rnd = new Random();
                        foreach (Ellipse ell in ellipses)
                        {
                            int i = Logika.MovingOfBall(ell, canvas, rnd, speed_, Window);
                            if (i == 1)
                            {
                                DeleteEllipse(ell);
                                break;
                            }
                        }
                        Thread.Sleep(100);
                    }
                }
			});
		}

	}
}
