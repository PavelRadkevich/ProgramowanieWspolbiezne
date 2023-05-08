using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Prezentacja;
using Prezentacja.Exceptions;
using Prezentacja.Model;
using System;
using System.Runtime.Hosting;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Tests
{
    [TestClass]
    public class ModelTest
    {
        MainWindow MainWindow { get; set; }
        Application Application { get; set; }
        Model model;

        [TestInitialize] 
        public void Init() {
            MainWindow = new MainWindow();
            if (Application.Current == null)
            { new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown }; }
            Application.Current.MainWindow = MainWindow;
            model = new Model();
            model.Window = Application.Current.MainWindow;
        }
        [TestCleanup]
        public void Cleanup()
        {
            Application.Current.Shutdown();
            Application = null;
            model = null;
        }
        [TestMethod]
        public void ModelConstructorTest()
        {
            Assert.IsTrue(model.ButtonStart != null);
            Assert.IsTrue(model.ButtonStop != null);
            Assert.IsFalse(model.moving);
        }
        [TestMethod]
        public void DrawingTest()
        {
            model.amount = 10;
            model.Drawing();
            Assert.IsTrue(model.balls.Count == model.amount);
        }
        [TestMethod]
        [ExpectedException(typeof(NegativeAmountException))]
        public void CheckEllipsesTest()
        {
            model.amount = 10;
            model.CheckEllipses();
            Assert.IsTrue(model.balls.Count == model.amount);
            model.amount = 5;
            model.CheckEllipses();
            Assert.IsTrue(model.balls.Count == model.amount);
            model.amount = -1;
            model.CheckEllipses();
        }
        [TestMethod]
        public void CheckCollisionsTest()
        {
            model.amount = 2;
            model.CheckEllipses();
            /////////////////  Distance: 10y  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 20;
            model.balls[0].y = 20;
            model.balls[1].x = 20;
            model.balls[1].y = 70;
            model.BuildTrees();
            Assert.IsFalse(model.CheckCollisions(model.rootX));
            Assert.IsFalse(model.CheckCollisions(model.rootY));
            /////////////////  Distance: 0y  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 20;
            model.balls[0].y = 20;
            model.balls[1].x = 20;
            model.balls[1].y = 60;
            model.BuildTrees();
            Assert.IsTrue(model.CheckCollisions(model.rootX));
            Assert.IsTrue(model.CheckCollisions(model.rootY));
            /////////////////  Distance: -1y  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 20;
            model.balls[0].y = 20;
            model.balls[1].x = 20;
            model.balls[1].y = 59;
            model.BuildTrees();
            Assert.IsTrue(model.CheckCollisions(model.rootX));
            Assert.IsTrue(model.CheckCollisions(model.rootY));
            /////////////////  Distance: 10x  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 20;
            model.balls[0].y = 20;
            model.balls[1].x = 70;
            model.balls[1].y = 20;
            model.BuildTrees();
            Assert.IsFalse(model.CheckCollisions(model.rootX));
            Assert.IsFalse(model.CheckCollisions(model.rootY));
            /////////////////  Distance: 0x  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 20;
            model.balls[0].y = 20;
            model.balls[1].x = 60;
            model.balls[1].y = 20;
            model.BuildTrees();
            Assert.IsTrue(model.CheckCollisions(model.rootX));
            Assert.IsTrue(model.CheckCollisions(model.rootY));
            /////////////////  Distance: -1x  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 20;
            model.balls[0].y = 20;
            model.balls[1].x = 59;
            model.balls[1].y = 20;
            model.BuildTrees();
            Assert.IsTrue(model.CheckCollisions(model.rootX));
            Assert.IsTrue(model.CheckCollisions(model.rootY));
            /////////////////  Distance: 4.7xy  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 23;
            model.balls[0].y = 22;
            model.balls[1].x = 43;
            model.balls[1].y = 62;
            model.BuildTrees();
            Assert.IsFalse(model.CheckCollisions(model.rootX));
            Assert.IsFalse(model.CheckCollisions(model.rootY));
            /////////////////  Distance: 0.3xy  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 27;
            model.balls[0].y = 25;
            model.balls[1].x = 43;
            model.balls[1].y = 62;
            model.BuildTrees();
            Assert.IsFalse(model.CheckCollisions(model.rootX));
            Assert.IsFalse(model.CheckCollisions(model.rootY));
            /////////////////  Distance: -0.08xy  /////////////////
            model.balls[0].radius = 20;
            model.balls[1].radius = 20;
            model.balls[0].x = 28;
            model.balls[0].y = 25;
            model.balls[1].x = 43;
            model.balls[1].y = 62;
            model.BuildTrees();
            Assert.IsTrue(model.CheckCollisions(model.rootX));
            Assert.IsTrue(model.CheckCollisions(model.rootY));
        }
    }
}
