using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prezentacja.Model;
using Prezentacja;
using System;
using System.Windows;
using Dane;
using System.Numerics;
using System.Windows.Shapes;
using Moq;

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
        public void PointMovingOfBallTest()
        {
            ///////////////////Right Upper grancice///////////////////////////////////////////////////////
            Ball ball = new Ball(new Ellipse(), 10, 776, 512, new Vector2((float) 0.5, (float) 0.5));
            Logika.Logika.PointMovingOfBall(ball, 20);
            Assert.AreEqual(-0.5, ball.vector.X);
            Assert.AreEqual(-0.5, ball.vector.Y);
            ///////////////////Left Lower grancice///////////////////////////////////////////////////////
            ball = new Ball(new Ellipse(), 10, 14, 114, new Vector2((float)-0.5, (float)-0.5));
            Logika.Logika.PointMovingOfBall(ball, 20);
            Assert.AreEqual(0.5, ball.vector.X);
            Assert.AreEqual(0.5, ball.vector.Y);
            ///////////////////Left grancice///////////////////////////////////////////////////////
            ball = new Ball(new Ellipse(), 10, 14, 115, new Vector2((float)-0.5, (float)-0.5));
            Logika.Logika.PointMovingOfBall(ball, 20);
            Assert.AreEqual(0.5, ball.vector.X);
            Assert.AreEqual(-0.5, ball.vector.Y);
            ///////////////////Right grancice///////////////////////////////////////////////////////
            ball = new Ball(new Ellipse(), 10, 776, 115, new Vector2((float)0.5, (float)0.5));
            Logika.Logika.PointMovingOfBall(ball, 20);
            Assert.AreEqual(-0.5, ball.vector.X);
            Assert.AreEqual(0.5, ball.vector.Y);
            ///////////////////Upper grancice///////////////////////////////////////////////////////
            ball = new Ball(new Ellipse(), 10, 150, 512, new Vector2((float)0.5, (float)0.5));
            Logika.Logika.PointMovingOfBall(ball, 20);
            Assert.AreEqual(0.5, ball.vector.X);
            Assert.AreEqual(-0.5, ball.vector.Y);
            ///////////////////Lower grancice///////////////////////////////////////////////////////
            ball = new Ball(new Ellipse(), 10, 150, 114, new Vector2((float)0.5, (float)-0.5));
            Logika.Logika.PointMovingOfBall(ball, 20);
            Assert.AreEqual(0.5, ball.vector.X);
            Assert.AreEqual(0.5, ball.vector.Y);
        }
    }
}
