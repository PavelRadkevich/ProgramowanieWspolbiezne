using Dane;
using Logika;
using Prezentacja.Exceptions;
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
        public List<Ball> balls = new List<Ball>();
        public bool moving { get; set; }
        public Node rootX { get; set; } = new Node ();
        public Node rootY { get; set; } = new Node ();
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
        private int numberOfBall = 0;
        public Model()
        {
            ButtonStart = new ViewModel.ButtonStart(this);
            ButtonStop = new ViewModel.ButtonStop(this);
            moving = false;
            Task Logs = Task.Factory.StartNew(() => sendDiagnostics());
            Task.Run(() =>
            {
                while (true)
                {
                    Task checkEll = Task.Factory.StartNew(() => CheckEllipses());
                    checkEll.Wait();
                    if (balls.Count != 0 && moving == true)
                    {
                        rootX = new Node
                        {
                            ball = balls[0]
                        };
                        rootY = new Node
                        {
                            ball = balls[0]
                        };
                        for (int i = 1; i < balls.Count; i++)
                        {
                            lock (balls[i])
                            {
                                BuildTrees();
                            }
                        }
                        Task ruch = Task.Factory.StartNew(() => MovingOfBalls());
                        ruch.Wait();
                    }
                }
            });
        }
        public void Drawing()
        {
            Random rnd = new Random();
            canvas = (Canvas)Window.FindName("CanvasMyWindow");
            for (int i = balls.Count; i < amount_; i++)
            {
                
                Ball ball = (Ball)Logika.Logika.DrawBall(Window, rnd, numberOfBall);
                balls.Add(ball);
                numberOfBall++;
            }
        }
        public void CheckEllipses()
        {
            if (amount_ < 0)
            {
                throw new NegativeAmountException("Amount(" + amount + ") is negative number");
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
        public bool CheckCollisions(Node node)
        {
            if (node == null)
            {
                return false;
            }
            Task left = Task.Factory.StartNew(() => CheckCollisions(node.left));
            Task right = Task.Factory.StartNew(() => CheckCollisions(node.right));
            left.Wait();
            right.Wait();
            if (Logika.Logika.CheckCollision(node, node.left)) return true;
            if (Logika.Logika.CheckCollision(node, node.right)) return true;
            return false;
        }
        public void MovingOfBalls()
        {
            Random rnd = new Random();
            foreach (Ball b in balls)
            {
                if (balls.Count != 0 & moving == true)
                {
                    Task.Run(() =>
                        {
                        lock (b)
                        {
                            Point point = Logika.Logika.PointMovingOfBall(b, b.speed * speed_);
                            b.x = point.X;
                            b.y = point.Y;
                                if (CheckCollisions(rootX)) {
                                    Logika.Logika.MovingOfBall(b, b.speed * speed_, point);
                                }
                                else
                                {
                                    CheckCollisions(rootY);
                                    Logika.Logika.MovingOfBall(b, b.speed * speed_, point);
                                }
                        }
                        });
                }
                Thread.Sleep(1);
            }
        }
        public void BuildTrees()
        {
            rootX = new Node
            {
                ball = balls[0]
            };
            rootY = new Node
            {
                ball = balls[0]
            };
            for (int i = 1; i < balls.Count; i++)
            {
                Task binaryTreeX = Task.Factory.StartNew(() => BinarySearchTree.InsertX(rootX, balls[i]));
                Task binaryTreeY = Task.Factory.StartNew(() => BinarySearchTree.InsertY(rootY, balls[i]));
                binaryTreeX.Wait();
                binaryTreeY.Wait();
            }
        }
        private async void sendDiagnostics()
        {
            while (true)
            {
                foreach (Ball b in balls)
                {
                    b.sendDiagnostic();
                }
                await Task.Delay(1000);
            }
        }

    }
}
