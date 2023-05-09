using System;
using System.Numerics;
using System.Windows.Shapes;

namespace Dane
{
    public class Ball
    {
        public Ellipse ellipse { get; set; }
        public double diametr { get; set; }
        public double radius { get; set; }
        public double weight { get; set; }
        private float density { get; set; } = 1;
        public double speed { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public Vector2 vector { get; set; }
        public Ball(Ellipse ellipse, double diametr, double x, double y, Vector2 direction)
        {
            this.vector = direction;
            this.x = x;
            this.y = y;
            this.ellipse = ellipse;
            this.diametr = diametr;
            radius = diametr / 2;
            weight = density * Math.PI * Math.Pow(diametr, 2) / 4;
            speed = 1 / weight * 250;

        }
        static void Main()
        {
        }

        public void NullMethod()
        {
            throw new NotImplementedException();
        }
        public Ball() {
            throw new NotImplementedException();
        }
    }
}
