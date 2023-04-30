using Dane;
using System;
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
            ellipse.Width = rnd.Next(10, 70);
            ellipse.Height = ellipse.Width;
            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, (byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256)));
            ellipse.Fill = brush;
            Canvas.SetLeft(ellipse, rnd.Next(5, 755 - (int)ellipse.Width));
            Canvas.SetTop(ellipse, rnd.Next(105, 491 - (int)ellipse.Width));
            Canvas canvas = (Canvas)window.FindName("CanvasMyWindow");
            canvas.Children.Add(ellipse);
            Ball b = new Ball(ellipse, ellipse.Width);
            Vector direction = GetRandomDirection(rnd);
            b.xStep = direction.X;
            b.yStep = direction.Y;
            return b;
        }

        private static Vector GetRandomDirection(Random rnd)
        {
            double angle = rnd.NextDouble() * Math.PI * 2; // случайный угол в радианах
            Vector direction = new Vector(Math.Cos(angle), Math.Sin(angle)); // создаем вектор из угла направления
            return direction;
        }

        public static void MovingOfBall(Ball ball, double speed)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                double x = Canvas.GetLeft(ball.ellipse);
                double y = Canvas.GetTop(ball.ellipse);
                double xTest = x + ball.xStep * speed;
                double yTest = y + ball.yStep * speed;
                if (x < 5 || x > 795 - ball.ellipse.Width)
                {
                    ball.xStep *= -1;
                }
                if (y < 105 || y > 531 - ball.ellipse.Width)
                {
                    ball.yStep *= -1;
                }
                Canvas.SetLeft(ball.ellipse, x + ball.xStep * speed);
                Canvas.SetTop(ball.ellipse, y + ball.yStep * speed);
            });

        }
    }
}
