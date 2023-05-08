using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prezentacja.Model;
using Prezentacja.ViewModel;

namespace Tests
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void ButtonStartStopClickTest()
        {
            Model model = new Model();
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
