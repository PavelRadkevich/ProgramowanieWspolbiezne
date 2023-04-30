using System;
using System.Windows.Shapes;

namespace Dane
{
    public class Ball
    {
        public Ellipse ellipse { get; set; }
        double diametr { get; set; }
        internal double weight { get; set; }
        private float density { get; set; }
        public double speed { get; set; }
        public double xStep { get; set; }
        public double yStep { get; set; }
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

        public void checkSpeed(double speed)
        {
            this.speed = speed / weight * 500;
        }

    }
}
