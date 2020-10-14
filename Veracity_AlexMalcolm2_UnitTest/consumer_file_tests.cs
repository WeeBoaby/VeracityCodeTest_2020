using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Veracity_AlexMalcolm;

namespace Veracity_AlexMalcolm_UnitTest
{
    [TestClass]
    public class ConsumerFileTests
    {
        [TestMethod]
        public void TestFileInitialisation()
        {
            ConsumerFile consumer = new ConsumerFile("");
            Assert.ThrowsException<FileNotFoundException>(() => consumer.ConsumeInput(new Input("file", "test message")));

            consumer = new ConsumerFile(System.IO.Path.GetTempPath());
            consumer.ConsumeInput(new Input("File", "dummy test"));
            Assert.IsTrue(File.Exists(consumer.OutputFilePath), "Output file did not exist");
        }

        [TestMethod]
        public void TestCompleteConsumption()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ConsumerFile consumer = new ConsumerFile("");
                Program.DEBUG_ENABLED = true; // needs to be enabled to show completed message.
                consumer.CompleteConsumption();

                Assert.IsTrue(sw.ToString().Contains("File Consumer - Consumption completed"), "did not find the expect consumption completed message");
            }
        }
    }
}
