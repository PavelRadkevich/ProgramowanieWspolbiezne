using Dane;
using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Logika
{
    public static class Logika
    {
        public static Ball NarysujKule(Window window, Random rnd)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = rnd.Next(20, 70);
            ellipse.Height = ellipse.Width;
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
            ellipse.Fill = brush;
            Canvas.SetLeft(ellipse, rnd.Next(5, 755 - (int)ellipse.Width));
            Canvas.SetTop(ellipse, rnd.Next(105, 491 - (int)ellipse.Width));
            Canvas canvas = (Canvas)window.FindName("CanvasMyWindow");
            canvas.Children.Add(ellipse);
            Ball b = new Ball(ellipse, ellipse.Width);
            Vector2 direction = GetRandomDirection(rnd);
            b.vector = direction;
            b.x = Canvas.GetLeft(b.ellipse);
            b.y = Canvas.GetTop(b.ellipse);
            return b;
        }

        private static Vector2 GetRandomDirection(Random rnd)
        {
            double angle = rnd.NextDouble() * Math.PI * 2; // случайный угол в радианах
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); // создаем вектор из угла направления
            return direction;
        }

        public static void MovingOfBall(Ball ball, double speed)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                double x = Canvas.GetLeft(ball.ellipse);
                double y = Canvas.GetTop(ball.ellipse);
                double xTest = x + ball.vector.X * speed;
                double yTest = y + ball.vector.Y * speed;
                if (x < 5 || x > 795 - ball.ellipse.Width)
                {
                    ball.vector = new Vector2(ball.vector.X * -1, ball.vector.Y);
                }
                if (y < 105 || y > 531 - ball.ellipse.Width)
                {
                    ball.vector = new Vector2(ball.vector.X, ball.vector.Y * -1);
                }
                Canvas.SetLeft(ball.ellipse, x + ball.vector.X * speed);
                Canvas.SetTop(ball.ellipse, y + ball.vector.Y * speed);
                ball.x = Canvas.GetLeft(ball.ellipse);
                ball.y = Canvas.GetTop(ball.ellipse);
            });

        }

        public static void CheckCollision(Node node1, Node node2)
        {
            if (node1 == null || node2 == null)
            {
                return;
            }
            double distance = Math.Sqrt(Math.Pow((node1.ball.x - node1.ball.radius) - (node2.ball.x - node2.ball.radius), 2) + Math.Pow((node1.ball.y - node1.ball.radius) - (node2.ball.y - node2.ball.radius), 2));
            if (distance < node1.ball.diametr / 2 + node2.ball.radius)
            {
                Console.WriteLine("Kolizja!");
                lock (node1)
                {
                    CollisionHandling(node1.ball, node2.ball, distance);
                }
            }
        }
        public static void CollisionHandling(Ball ball1, Ball ball2, double distance)
        {
            double x1 = ball1.x - ball1.radius;
            double x2 = ball2.x - ball2.radius;
            double y1 = ball1.y - ball1.radius;
            double y2 = ball2.y - ball2.radius / 2;
            Vector2 center1 = new Vector2((float)x1, (float)y1);
            Vector2 center2 = new Vector2((float)x2, (float)y2);

            Vector2 vector1 = Vector2.Normalize(Vector2.Subtract(center2, center1));
            Vector2 vector2 = Vector2.Normalize(Vector2.Subtract(center1, center2));

            Vector2 res1 = Vector2.Normalize(Vector2.Add(ball1.vector, vector2));
            Vector2 res2 = Vector2.Normalize(Vector2.Add(ball2.vector, vector1));

            ball1.vector = res1;
            ball2.vector = res2;
            /*ball1.x -= Vector2.Subtract(center2, center1).X - ball2.radius - ball1.radius;
            ball1.y -= Vector2.Subtract(center2, center1).Y - ball2.radius - ball1.radius;
            ball2.x -= Vector2.Subtract(center1, center2).X - ball2.radius - ball1.radius;
            ball2.y -= Vector2.Subtract(center1, center2).Y - ball2.radius - ball1.radius;*/
        }
    }
}
