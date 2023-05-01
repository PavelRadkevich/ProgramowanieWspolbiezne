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
        internal double weight { get; set; }
        private float density { get; set; }
        public double speed { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public Vector2 vector { get; set; }
        public Ball(Ellipse ellipse, double diametr)
        {
            this.ellipse = ellipse;
            this.diametr = diametr;
            density = 1;
            calculateWeight();

        }

        private void calculateWeight()
        {
            this.weight = density * Math.PI * Math.Pow(diametr, 2) / 4;
        }

        public void calculateSpeed()
        {
            this.speed = 1 / weight * 1000;
        }

    }
}
