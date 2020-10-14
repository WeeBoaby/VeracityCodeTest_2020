using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Veracity_AlexMalcolm;

namespace Veracity_AlexMalcolm_UnitTest
{
    [TestClass]
    public class ConsoleHelperTests
    {
        [TestMethod]
        public void TestDebugWriteLineEnabled()
        {
            Program.DEBUG_ENABLED = false;
            string message = GetConsoleHelperMessage();

            Assert.IsTrue(message == string.Empty, "Output message contained text");
        }

        [TestMethod]
        public void TestDebugWriteLineDisabled()
        {
            Program.DEBUG_ENABLED = true;
            string message = GetConsoleHelperMessage();

            Assert.IsTrue(message != string.Empty, "Output message was empty");
        }

        private string GetConsoleHelperMessage()
        {
            string message;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ConsoleHelper.WriteLine(LogType.DEBUG, "test message");
                message = sw.ToString();
                sw.Close();
            }

            return message;
        }
    }
}
