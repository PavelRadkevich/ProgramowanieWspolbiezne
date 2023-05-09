using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prezentacja.Model;
using Prezentacja;
using System;
using System.Windows;
using Dane;
using System.Numerics;
using Moq;
using System.Reflection;

namespace Tests
{
    [TestClass]
    public class LogicTest
    {
        MainWindow MainWindow { get; set; }
        Application Application { get; set; }
        Model model;

        [TestInitialize]
        public void Init()
        {
            MainWindow = new MainWindow();
            if (Application.Current == null)
            { new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown }; }
            Application.Current.MainWindow = MainWindow;
        }
        [TestCleanup]
        public void Cleanup()
        {
            Application.Current.Shutdown();
            Application = null;
        }
        [TestMethod]
        public void DrawBallTest()
        {
            Random rnd = new Random();
            Assert.IsNotNull(Logika.Logika.DrawBall(MainWindow, rnd));
        }
        [TestMethod]
        public void GetRandomDirectionTest()
        {
            Random rnd1 = new Random(1);
            Random rnd2 = new Random(2);
            Assert.AreNotEqual(Logika.Logika.GetRandomDirection(rnd1), Logika.Logika.GetRandomDirection(rnd2));
        }
        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
        public void PointMovingOfBallTest()
        {
            Mock<Ball> ball = new Mock<Ball>();
            ///////////////////Right Upper grancice///////////////////////////////////////////////////////
            ball.Object.diametr = 10;
            ball.Object.x = 776;
            ball.Object.y = 512;
            ball.Object.vector = new Vector2((float) 0.5, (float) 0.5);
            Logika.Logika.PointMovingOfBall(ball.Object, 20);
            Assert.AreEqual(-0.5, ball.Object.vector.X);
            Assert.AreEqual(-0.5, ball.Object.vector.Y);
            ///////////////////Left Lower grancice///////////////////////////////////////////////////////
            ball.Object.diametr = 10;
            ball.Object.x = 14;
            ball.Object.y = 114;
            ball.Object.vector = new Vector2((float)-0.5, (float)-0.5);
            Logika.Logika.PointMovingOfBall(ball.Object, 20);
            Assert.AreEqual(0.5, ball.Object.vector.X);
            Assert.AreEqual(0.5, ball.Object.vector.Y);
            ///////////////////Left grancice///////////////////////////////////////////////////////
            ball.Object.diametr = 10;
            ball.Object.x = 14;
            ball.Object.y = 115;
            ball.Object.vector = new Vector2((float)-0.5, (float)-0.5);
            Logika.Logika.PointMovingOfBall(ball.Object, 20);
            Assert.AreEqual(0.5, ball.Object.vector.X);
            Assert.AreEqual(-0.5, ball.Object.vector.Y);
            ///////////////////Right grancice///////////////////////////////////////////////////////
            ball.Object.diametr = 10;
            ball.Object.x = 776;
            ball.Object.y = 115;
            ball.Object.vector = new Vector2((float)0.5, (float)0.5);
            Logika.Logika.PointMovingOfBall(ball.Object, 20);
            Assert.AreEqual(-0.5, ball.Object.vector.X);
            Assert.AreEqual(0.5, ball.Object.vector.Y);
            ///////////////////Upper grancice///////////////////////////////////////////////////////
            ball.Object.diametr = 10;
            ball.Object.x = 150;
            ball.Object.y = 512;
            ball.Object.vector = new Vector2((float)0.5, (float)0.5);
            Logika.Logika.PointMovingOfBall(ball.Object, 20);
            Assert.AreEqual(0.5, ball.Object.vector.X);
            Assert.AreEqual(-0.5, ball.Object.vector.Y);
            ///////////////////Lower grancice///////////////////////////////////////////////////////
            ball.Object.diametr = 10;
            ball.Object.x = 150;
            ball.Object.y = 114;
            ball.Object.vector = new Vector2((float)0.5, (float)-0.5);
            Logika.Logika.PointMovingOfBall(ball.Object, 20);
            Assert.AreEqual(0.5, ball.Object.vector.X);
            Assert.AreEqual(0.5, ball.Object.vector.Y);
        }
        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
        public void CheckCollisionsTest()
        {
            Mock<Ball> ball1 = new Mock<Ball>();
            Mock<Ball> ball2 = new Mock<Ball>();
            Mock<Node> node1 = new Mock<Node>();
            Mock<Node> node2 = new Mock<Node>();
            node1.Object.right = node2.Object;
            node1.Object.ball = ball1.Object;
            node2.Object.left = node1.Object;
            node2.Object.ball = ball2.Object;
            /////////////////  Distance: 10y  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 20;
            ball1.Object.y = 20;
            ball2.Object.x = 20;
            ball2.Object.y = 70;
            Assert.IsFalse(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: 0y  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 20;
            ball1.Object.y = 20;
            ball2.Object.x = 20;
            ball2.Object.y = 60;
            Assert.IsTrue(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: -1y  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 20;
            ball1.Object.y = 20;
            ball2.Object.x = 20;
            ball2.Object.y = 59;
            Assert.IsTrue(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: 10x  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 20;
            ball1.Object.y = 20;
            ball2.Object.x = 70;
            ball2.Object.y = 20;
            Assert.IsFalse(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: 0x  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 20;
            ball1.Object.y = 20;
            ball2.Object.x = 60;
            ball2.Object.y = 20;
            Assert.IsTrue(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: -1x  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 20;
            ball1.Object.y = 20;
            ball2.Object.x = 59;
            ball2.Object.y = 20;
            Assert.IsTrue(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: 4.7xy  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 23;
            ball1.Object.y = 22;
            ball2.Object.x = 43;
            ball2.Object.y = 62;
            Assert.IsFalse(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: 0.3xy  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 27;
            ball1.Object.y = 25;
            ball2.Object.x = 43;
            ball2.Object.y = 62;
            Assert.IsFalse(Logika.Logika.CheckCollision(node2.Object, node1.Object));
            /////////////////  Distance: -0.08xy  /////////////////
            ball1.Object.radius = 20;
            ball2.Object.radius = 20;
            ball1.Object.x = 28;
            ball1.Object.y = 25;
            ball2.Object.x = 43;
            ball2.Object.y = 62;
            Assert.IsTrue(Logika.Logika.CheckCollision(node2.Object, node1.Object));
        }
    }
}
