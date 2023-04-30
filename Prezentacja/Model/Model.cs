using Dane;
using Logika;
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
                    Task ruch = Task.Factory.StartNew(() => MovingOfBalls());
                    ruch.Wait();
                    if (balls.Count != 0)
                    {
                        Node root = new Node
                        {
                            ball = balls[0]
                        };
                        for (int i = 1; i < balls.Count; i++)
                        {
                            lock (balls[i])
                            {
                                BinarySearchTree.Insert(root, balls[i]);
                            }

                        }
                        CheckCollisions(root);
                    }
                    Thread.Sleep(1);
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
        private void Drawing()
        {
            Random rnd = new Random();
            for (int i = 0; i < amount_ - balls.Count; i++)
            {
                Ball e = Logika.Logika.NarysujKule(Window, rnd);
                balls.Add(e);
                e.calculateSpeed();
            }
        }
        private void CheckEllipses()
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
        public void CheckCollisions(Node node)
        {
            if (node == null)
            {
                return;
            }
            Task left = Task.Factory.StartNew(() => CheckCollisions(node.left));
            Task right = Task.Factory.StartNew(() => CheckCollisions(node.right));
            left.Wait();
            right.Wait();
            Logika.Logika.CheckCollision(node, node.left);
            Logika.Logika.CheckCollision(node, node.right);
        }
        private void MovingOfBall(Ball ball)
        {
            if (balls.Count != 0 & moving == true)
            {
                Logika.Logika.MovingOfBall(ball, ball.speed * speed_);
            }
        }
        private void MovingOfBalls()
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
