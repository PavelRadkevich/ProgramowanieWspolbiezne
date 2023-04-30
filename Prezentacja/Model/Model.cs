using Dane;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Prezentacja.Model
{
    public class Model : ViewModel.ObserveTextBox
    {
        private int amount_;
        private int speed_;
        public ViewModel.ButtonStart ButtonStart { get; private set; }
        public ViewModel.ButtonStop ButtonStop { get; private set; }
        public Window Window { private get; set; }
        public Canvas canvas { private get; set; }
        private List<Ball> balls = new List<Ball>();
        public bool moving { get; set; }
        public int speed
        {
            get { return speed_; }
            set
            {
                speed_ = value;
                OnPropertyChanged();
                CheckSpeed();
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
        public Model()
        {
            ButtonStart = new ViewModel.ButtonStart(this);
            ButtonStop = new ViewModel.ButtonStop(this);
            moving = false;
            Task.Run(() =>
            {
                while (true)
                {
                    CheckEllipses();
                    MovingOfBalls();
                    Thread.Sleep(10);
                }
            });
        }
        public int GetCountEllipses()
        {
            return balls.Count;
        }
        public void ButtonStartClick()
        {
            moving = true;
            canvas = (Canvas)Window.FindName("CanvasMyWindow");
            Drawing();
        }
        public void ButtonStopClick()
        {
            moving = false;
        }

        public void Drawing()
        {
            Random rnd = new Random();
            for (int i = 0; i < amount_ - balls.Count; i++)
            {
                Ball e = Logika.Logika.NarysujKule(Window, rnd);
                balls.Add(e);
                e.checkSpeed(speed_);
            }
        }
        public void CheckEllipses()
        {
            if (amount_ < 0)
            {
                return;
            }
            if (balls.Count < amount_)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Drawing();
                });
            }
            else if (balls.Count > amount_)
            {
                while (balls.Count > amount_)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        canvas.Children.Remove(balls[0].ellipse);
                        balls.RemoveAt(0);
                    });
                }
            }
        }
        private void CheckSpeed()
        {
            foreach (Ball b in balls)
            {
                b.checkSpeed(speed_);
            }
        }
        public void DeleteEllipse(Ball ball)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                canvas.Children.Remove(ball.ellipse);
                balls.Remove(ball);
            });
        }
        public void MovingOfBall(Ball ball)
        {
            if (balls.Count != 0 & moving == true)
            {
                Logika.Logika.MovingOfBall(ball, ball.speed);
            }
        }

        public void MovingOfBalls()
        {
            Random rnd = new Random();
            if (balls.Count != 0 & moving == true)
            {
                foreach (Ball b in balls)
                {
                    Task.Run(() =>
                    {
                        MovingOfBall(b);
                    });
                }
                Thread.Sleep(10);
            }


        }

    }
}
