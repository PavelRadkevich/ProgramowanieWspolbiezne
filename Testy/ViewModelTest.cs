using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Prezentacja.ViewModel;

namespace Prezentacja.Tests
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void ButtonStartStopClickTest()
        {
            Model.Model model = new Model.Model();
            ButtonStart bstart = new ButtonStart(model);
            ButtonStop bstop = new ButtonStop(model);
            Assert.IsTrue(model.moving == false);
            bstart.Execute("");
            Assert.IsTrue(model.moving == true);
            bstop.Execute("");
            Assert.IsTrue(model.moving == false);
        }
    }
}
