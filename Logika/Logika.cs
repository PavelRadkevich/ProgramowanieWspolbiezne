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
        public static Ball DrawBall(Window window, Random rnd, int numberOfBall)
        {
            Double HW = rnd.Next(20, 70);
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
            Ellipse ellipse = new Ellipse
            {
                Width = HW,
                Height = HW,
                Fill = brush
            };
            Canvas.SetLeft(ellipse, rnd.Next(5, 755 - (int)ellipse.Width));
            Canvas.SetTop(ellipse, rnd.Next(105, 491 - (int)ellipse.Width));
            Canvas canvas = (Canvas)window.FindName("CanvasMyWindow");
            canvas.Children.Add(ellipse);
            Vector2 direction = GetRandomDirection(rnd);
            Ball b = new Ball(ellipse, ellipse.Width, Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse), direction, numberOfBall);
            return b;
        }

        public static Vector2 GetRandomDirection(Random rnd)
        {
            double angle = rnd.NextDouble() * Math.PI * 2; // случайный угол в радианах
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)); // создаем вектор из угла направления
            return direction;
        }

        public static Point PointMovingOfBall(Ball ball, double speed)
        {
                double x = ball.x;
                double y = ball.y;
                double xTest = x + ball.vector.X * speed;
                double yTest = y + ball.vector.Y * speed;
                if (xTest < 5 || xTest > 795 - ball.diametr)
                {
                    ball.vector = new Vector2(ball.vector.X * -1, ball.vector.Y);
                }
                if (yTest < 105 || yTest > 531 - ball.diametr)
                {
                    ball.vector = new Vector2(ball.vector.X, ball.vector.Y * -1);
                }
            Point point = new Point(x + ball.vector.X * speed, y + ball.vector.Y * speed);
            return point;
        }
        public static void MovingOfBall(Ball ball, double speed, Point point)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Canvas.SetLeft(ball.ellipse, point.X);
                Canvas.SetTop(ball.ellipse, point.Y);
                ball.x = Canvas.GetLeft(ball.ellipse);
                ball.y = Canvas.GetTop(ball.ellipse);
            }));
        }

        public static bool CheckCollision(Node node1, Node node2)
        {
            if (node1 == null || node2 == null)
            {
                return false;
            }
            double distance = Math.Sqrt(Math.Pow((node1.ball.x + node1.ball.radius) - (node2.ball.x + node2.ball.radius), 2) + Math.Pow((node1.ball.y + node1.ball.radius) - (node2.ball.y + node2.ball.radius), 2));
            if (distance <= node1.ball.radius + node2.ball.radius)
            {
                Console.WriteLine("Kolizja!");
                lock (node1)
                {
                    CollisionHandling(node1.ball, node2.ball, distance);
                    return true;
                }
            }
            return false;
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

            double speed1 = ((ball1.weight - ball2.weight) * ball1.speed + 2 * ball2.weight * ball2.speed) / (ball1.weight + ball2.weight);
            double speed2 = ((ball2.weight - ball1.weight) * ball2.speed + 2 * ball1.weight * ball1.speed) / (ball1.weight + ball2.weight);

            ball1.speed = speed1;
            ball2.speed = speed2;
        }

        static void Main(string[] args) { }
    }
}
