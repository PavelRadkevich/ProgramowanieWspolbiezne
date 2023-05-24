using Dane;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using System.Windows.Shapes;

namespace Tests
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void BallConstructorTest()
        {
            Ellipse e = new Ellipse();
            Vector2 vec = new Vector2();
            double diametr = 20;
            double x = 20;
            double y = 20;
            Ball ball = new Ball(e, diametr, x, y, vec);
            Assert.AreEqual(ball.ellipse, e);
            Assert.AreEqual(ball.diametr, diametr);
            Assert.AreEqual(ball.radius, diametr / 2);
            Assert.AreEqual(ball.x, x);
            Assert.AreEqual(ball.y, y);
            Assert.AreEqual(ball.vector, vec);
        }
    }
}