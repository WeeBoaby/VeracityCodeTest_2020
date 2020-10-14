using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Veracity_AlexMalcolm;

namespace Veracity_AlexMalcolm_UnitTest
{
    [TestClass]
    public class ConsumerConsoleTests
    {
        [TestMethod]
        public void TestCompleteConsumption()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ConsumerConsole consumer = new ConsumerConsole();
                Program.DEBUG_ENABLED = true; // needs to be enabled to show completed message.
                consumer.CompleteConsumption();

                Assert.IsTrue(sw.ToString().Contains("Console Consumer - Consumption completed"), "did not find the expect consumption completed message");
            }
        }

        [TestMethod]
        public void TestConsumption()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ConsumerConsole consumer = new ConsumerConsole();
                consumer.ConsumeInput(new Input("Console", "test message"));
                Assert.IsTrue(sw.ToString().Contains("test message"));
            }
        }
    }
}
