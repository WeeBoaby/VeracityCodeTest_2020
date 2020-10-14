using System;
using System.IO;

namespace Veracity_AlexMalcolm
{
    public class ConsumerFile : ConsumerBase
    {
        // Public functions
        public string OutputFilePath { get; private set; }
        public ConsumerFile(string workingDirectory) : base(new string[] { "File" })
        {
            this.m_workingDirectory = workingDirectory;
        }
        public override void ConsumeInput(Input input)
        {
            if (m_outputFile == null && !InitialiseFile())
                throw new FileNotFoundException();

            m_outputFile.WriteLine(string.Format("[{0}] {1}", ConsoleHelper.currentTimeFormatted(), input.InputContent));
            m_outputFile.Flush();
            base.ConsumeInput(input);
        }
        public override void CompleteConsumption()
        {
            ConsoleHelper.WriteLine(LogType.INFO, string.Format("File Consumer - Consumption completed. {0} inputs consumed", m_inputsConsumed.ToString()));
            CloseFile();
        }

        // Private members
        private StreamWriter m_outputFile;
        private string m_workingDirectory;

        // Private functions
        private bool InitialiseFile()
        {
            if (!Directory.Exists(m_workingDirectory))
                return false;

            if (m_outputFile == null)
            {
                OutputFilePath = Path.Combine(m_workingDirectory, string.Format("{0}{1}{2}", "FileConsumer_Output_", DateTime.Now.ToString("yyMMdd_HHmmss"), ".txt"));

                // add current date time to the file, so we don't overwrite
                m_outputFile = new StreamWriter(OutputFilePath);
                ConsoleHelper.WriteLine(LogType.INFO, string.Format("Outputting File consumption to - {0}", OutputFilePath));
                return true;
            }

            return false;
        }
        private void CloseFile()
        {
            if (m_outputFile != null)
                m_outputFile.Close();
        }
    }
}
