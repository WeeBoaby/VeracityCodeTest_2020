using Microsoft.VisualStudio.TestTools.UnitTesting;
using Veracity_AlexMalcolm;

namespace Veracity_AlexMalcolm_UnitTest
{
    [TestClass]
    public class InputTests
    {
        [TestMethod]
        public void TestConstructorValid()
        {
            Input testInputFile = new Input("File", "Dummy");
            Assert.IsTrue(testInputFile.IsValid(), "testInputFile was not valid");

            Input testInputConsole = new Input("Console", "Dummy");
            Assert.IsTrue(testInputConsole.IsValid(), "testInputConsole was not valid");
        }

        [TestMethod]
        public void TestConstructorInvalid()
        {
            Input testInputNoType = new Input("", "Dummy");
            Assert.IsTrue(!testInputNoType.IsValid(), "testInputNoType was valid");

            Input testInputNoContent = new Input("Console", "");
            Assert.IsTrue(!testInputNoContent.IsValid(), "testInputNoContent was valid");

            Input testInputNoContentFile = new Input("File", "");
            Assert.IsTrue(!testInputNoContentFile.IsValid(), "testInputNoContent was valid");

            Input testInputNothing = new Input("", "");
            Assert.IsTrue(!testInputNothing.IsValid(), "testInputNothing was valid");
        }
    }
}
