using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prezentacja;
using Prezentacja.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void ModelConstructorTest()
        {
            MainWindow mainWindow = new MainWindow();
            Model model = new Model();

        }
    }
}
