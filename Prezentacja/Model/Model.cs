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
        private Node rootX { get; set; } = new Node ();
        private Node rootY { get; set; } = new Node ();
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
                                Task binaryTreeX = Task.Factory.StartNew(() => BinarySearchTree.InsertX(rootX, balls[i]));
                                binaryTreeX.Wait();
                                Task binaryTreeY = Task.Factory.StartNew(() => BinarySearchTree.InsertY(rootY, balls[i]));
                                binaryTreeY.Wait();
                            }
                        }
                        //Task collisionX = Task.Factory.StartNew(() => CheckCollisions(rootX));
                        //collisionX.Wait();
                        //Task collisionY = Task.Factory.StartNew(() => CheckCollisions(rootY));
                        //collisionY.Wait();
                        //Task coordinates = Task.Factory.StartNew(() => CheckCoordinates());
                        //coordinates.Wait();
                        Task ruch = Task.Factory.StartNew(() => MovingOfBalls());
                        ruch.Wait();
                    }
                    //Thread.Sleep(1);
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
            BuildTrees();
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
                Ball ball = Logika.Logika.NarysujKule(Window, rnd);
                balls.Add(ball);
                ball.calculateSpeed();
                SetCoordinates(ball);
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
        public void CheckCoordinates()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (Ball ball in balls)
                {
                    if (ball.x != Canvas.GetLeft(ball.ellipse))
                    {
                        Canvas.SetLeft(ball.ellipse, ball.x);
                    }
                    if (ball.y != Canvas.GetTop(ball.ellipse))
                    {
                        Canvas.SetTop(ball.ellipse, ball.y);
                    }
                }
            });
        }
        public void SetCoordinates(Ball ball)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ball.x != Canvas.GetLeft(ball.ellipse))
                {
                    Canvas.SetLeft(ball.ellipse, ball.x);
                }
                if (ball.y != Canvas.GetTop(ball.ellipse))
                {
                    Canvas.SetTop(ball.ellipse, ball.y);
                }
            });
        }
        private void MovingOfBalls()
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
                                //BuildTrees();
                        }
                        });
                }
                Thread.Sleep(1);
            }
        }
        private void BuildTrees()
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
                    binaryTreeX.Wait();
                    Task binaryTreeY = Task.Factory.StartNew(() => BinarySearchTree.InsertY(rootY, balls[i]));
                    binaryTreeY.Wait();
            }
        }

    }
}
