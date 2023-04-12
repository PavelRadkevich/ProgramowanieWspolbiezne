using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prezentacja.Tests
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void ButtonStartClickTest()
        {
            Model.Model model = new Model.Model
            {
                amount = 10
            };
            Assert.IsTrue(0 == model.GetCountEllipses());
        }
    }
}
