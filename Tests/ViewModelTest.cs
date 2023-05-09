using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            Mock<Model> model = new Mock<Model>();
            ButtonStart bstart = new ButtonStart(model.Object);
            ButtonStop bstop = new ButtonStop(model.Object);
            Assert.IsTrue(model.Object.moving == false);
            bstart.Execute("");
            Assert.IsTrue(model.Object.moving == true);
            bstop.Execute("");
            Assert.IsTrue(model.Object.moving == false);
        }
    }
}
