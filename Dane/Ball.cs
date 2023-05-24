using System;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Serialization;

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
        public int numberOfBall { get; set; }
        private int numberOfLine { get; set; }
        private int lengthOfData { get; set; }
        private Task Logs;
        public Ball(Ellipse ellipse, double diametr, double x, double y, Vector2 direction, int numberOfBall)
        {
            vector = direction;
            this.x = x;
            this.y = y;
            this.ellipse = ellipse;
            this.diametr = diametr;
            this.numberOfBall = numberOfBall;
            radius = diametr / 2;
            weight = density * Math.PI * Math.Pow(diametr, 2) / 4;
            speed = 1 / weight * 250;
            sendDiagnostic();
            var Lines = File.ReadAllLines("H:\\Study\\IV Semestr\\2.2 Programowanie Wsppolbiezne\\Logs\\Ball" + numberOfBall + "Log.xml");
            lengthOfData = Lines.Length;
            numberOfLine = lengthOfData;
        }

        public void sendDiagnostic()
        {

                        DiagnosticData data = new DiagnosticData
                        {
                            numberOfBall = this.numberOfBall,
                            xCoordinate = this.x,
                            yCoordinate = this.y,
                            speed = this.speed,
                            timeOfLog = DateTime.Now,
                            vector = this.vector
                        };
                        XmlSerializer xml = new XmlSerializer(typeof(DiagnosticData));
                        using (TextWriter sw = new StreamWriter("H:\\Study\\IV Semestr\\2.2 Programowanie Wsppolbiezne\\Logs\\Ball" + numberOfBall + "Log.xml", true))
                        {
                            xml.Serialize(sw, data);
                        }
                         numberOfLine += lengthOfData;
            if (numberOfLine > 1000)
            {
                File.WriteAllText("H:\\Study\\IV Semestr\\2.2 Programowanie Wsppolbiezne\\Logs\\Ball" + numberOfBall + "Log.xml", string.Empty);
                numberOfLine  = 0;
            }
        }
        public Ball() {
            throw new NotImplementedException();
        }

        private static void Main()
        {
        }
        public class DiagnosticData
        {
            public int numberOfBall;
            public double xCoordinate;
            public double yCoordinate;
            public double speed;
            public DateTime timeOfLog;
            public Vector2 vector;
        }
            
    }
}
