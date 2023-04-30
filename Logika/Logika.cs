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
                ball.x = x + ball.vector.X * speed;
                ball.y = y + ball.vector.Y * speed;
            });

        }

        public static void CheckCollision(Node node1, Node node2)
        {
            if (node1 == null || node2 == null)
            {
                return;
            }
            double distance = Math.Sqrt(Math.Pow((node1.ball.x - node1.ball.diametr / 2) - (node2.ball.x - node2.ball.diametr / 2), 2) + Math.Pow((node1.ball.y - node1.ball.diametr / 2) - (node2.ball.y - node2.ball.diametr / 2), 2));
            if (distance < node1.ball.diametr / 2 + node2.ball.diametr / 2)
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
            double Dx = ball1.x - ball1.diametr / 2 - ball2.x - ball2.diametr / 2;
            double Dy = ball1.y - ball1.diametr / 2 - ball2.y - ball2.diametr / 2;
            double hypotenuse = Math.Sqrt(Math.Pow(Dx, 2) + Math.Pow(Dy, 2)); //Теорема Пифагора
            if (hypotenuse == 0) hypotenuse = 0.0001;
            double sin = Dx / hypotenuse;
            double cos = Dy / hypotenuse;

            double Vn1 = ball2.vector.X * sin + ball2.vector.Y * cos;
            double Vn2 = ball1.vector.X * sin + ball1.vector.Y * cos;

            double wejście = (ball1.diametr / 2 + ball2.diametr / 2 - hypotenuse) / (Vn1 - Vn2);
            if (hypotenuse > 0.6) hypotenuse = 0.6;
            if (hypotenuse < -0.6) hypotenuse = -0.6;
            ball1.x -= ball1.vector.X * hypotenuse;
            ball1.y -= ball1.vector.Y * hypotenuse;
            ball2.x -= ball2.vector.X * hypotenuse;
            ball2.y -= ball2.vector.Y * hypotenuse;
            Dx = ball1.x - ball2.x;
            Dy = ball1.y - ball2.y;
            hypotenuse = Math.Sqrt(Dx * Dx + Dy * Dy); if (hypotenuse == 0) hypotenuse = 0.01;
            sin = Dx / hypotenuse; // sin
            cos = Dy / hypotenuse; // cos
            Vn1 = ball2.vector.X * sin + ball2.vector.Y * cos;
            Vn2 = ball1.vector.X * sin + ball1.vector.Y * cos;


            double Vt1 = ball2.vector.X * -1 * cos + ball2.vector.Y * sin;
            double Vt2 = ball1.vector.X * -1 * cos + ball1.vector.Y * sin;

            double o = Vn2;
            Vn2 = Vn1;
            Vn1 = o;
            Vector2 vector1 = new Vector2((float)(Vn2 * sin - Vt2 * cos), (float)(Vn2 * cos + Vt2 * sin));
            Vector2 res1 = Vector2.Normalize(vector1);
            //Vector2 vector1 = new Vector2();
            ball2.vector = new Vector2((float)(Vn1 * sin - Vt1 * cos), (float)(Vn1 * cos + Vt1 * sin));

            Vector2 vector2 = new Vector2((float)(Vn2 * sin - Vt2 * cos), (float)(Vn2 * cos + Vt2 * sin));
            Vector2 res2 = Vector2.Normalize(vector2);
            //Vector2 vector2 = new Vector2();
            ball2.vector = new Vector2((float)(ball2.x - Vn1 * sin - Vt1 * cos), (float)(Vn1 * cos + Vt1 * sin));
            ball2.vector = new Vector2((float)(ball2.x - Vn1 * sin - Vt1 * cos), (float)(Vn1 * cos + Vt1 * sin));




            /*Vector2 speed1 = new Vector2((float)(ball1.vector.X * ball1.speed), (float)(ball1.vector.X * ball1.speed));
            Vector2 speed2 = new Vector2((float)(ball2.vector.X * ball2.speed), (float)(ball2.vector.X * ball2.speed));
            Vector2 sumSpeed = speed1 - speed2;
            Vector2 pos1 = new Vector2((float)ball1.x, (float)ball1.y);
            Vector2 pos2 = new Vector2((float)ball2.x, (float)ball2.y);
            Vector2 normal = Vector2.Normalize(pos2 - pos1);
            float projection = Vector2.Dot(sumSpeed, normal);
            Vector2 v_refl = sumSpeed - 2 * projection * normal;
            Vector2 v1_res = speed1 - projection * normal + v_refl;
            Vector2 v1_unit = Vector2.Normalize(v1_res);
            ball1.x += distance;
            ball1.y += distance;
            //Vector2 v2_res = speed2 - projection * normal + v_refl;
            //Vector2 v2_unit = Vector2.Normalize(v2_res);
            ball1.vector = v1_unit;
            //ball2.vector = v2_unit;*/
        }
    }
}
