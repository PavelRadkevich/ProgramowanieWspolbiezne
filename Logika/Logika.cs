using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace Logika
{
    public class Logika
    {
        public bool moving {  get; set; }
        public Logika() {
        } 

        public Ellipse NarysujKule(Window window, Random rnd)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 40;
            ellipse.Height = 40;
            ellipse.Fill = Brushes.Pink;
            Canvas.SetLeft(ellipse, rnd.Next(5, 755));
            Canvas.SetTop(ellipse, rnd.Next(105, 491));
            Canvas canvas = (Canvas)window.FindName("CanvasMyWindow");
            canvas.Children.Add(ellipse);
            return ellipse;
        }

        private Vector GetRandomDirection(Random rnd)
        {
            double angle = rnd.NextDouble() * Math.PI * 2; // случайный угол в радианах
            Vector direction = new Vector(Math.Cos(angle), Math.Sin(angle)); // создаем вектор из угла направления
            return direction;
        }

        public int MovingOfBall (Ellipse ellipse, Canvas canvas, Random rnd, int speed, Window window)
        {
            int count = 0;
            if (moving)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    double x = Canvas.GetLeft(ellipse);
                    double y = Canvas.GetTop(ellipse);
                    bool place = false;
                    while (place == false)
                    {
                        place = true;
                        Vector direction = GetRandomDirection(rnd);
                        x += direction.X * speed;
                        y += direction.Y * speed;
                        if (x < 5 || x > 755)
                        {
                            x *= -1;
                        }
                        if (y < 105 || y > 491)
                        {
                            y *= -1;
                        }
                        if (x < 5 || x > 755 || y < 105 || y > 491)
                        {
                            place = false;
                        }
                        count++;
                        if (count == 40)
                        {
                            break;
                        }
                        rnd = new Random();
                    }
                    if (count <= 40)
                    {
                        Canvas.SetLeft(ellipse, x);
                        Canvas.SetTop(ellipse, y);
                    }
                });
            }
            if (count >= 40) return 1;
            else return 0;
        }
    }
}
