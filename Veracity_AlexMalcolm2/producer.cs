using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Veracity_AlexMalcolm
{
    public class Producer
    {
        // Public Members
        public const int const_maxInputs = 10; // in 1 second, for throttling
        public const int const_milliseconds = 1000; // in 1 second
        public const int const_targetRuntime = 10; // seconds

        // Public Functions
        public Producer(string[] inputList, string workingDirectory, bool throttle, bool guiEnabled)
        {
            InputQueue = new List<Input>();
            this.m_throttle = throttle;
            this.m_workingDirectory = workingDirectory;
            this.m_guiEnabled = guiEnabled;
            Init(inputList);
        }
        public List<Input> InputQueue { get; protected set; }
        public virtual void Produce()
        {
            // as part of the throttling/time limitation if the queue is too small we would get divide by zero. first calc must be >= 1.
            float timeBetweenConsumptions = (InputQueue.Count / const_targetRuntime);
            timeBetweenConsumptions = timeBetweenConsumptions < 1 ? 1 : timeBetweenConsumptions;
            timeBetweenConsumptions = const_milliseconds / timeBetweenConsumptions;

            if (m_throttle)
                timeBetweenConsumptions = const_milliseconds / const_maxInputs;

            foreach (Input input in InputQueue)
            {
                ConsumeInput(input);
                Thread.Sleep((int)timeBetweenConsumptions);
            }

            foreach (ConsumerBase consumer in m_consumers)
            {
                if (consumer != null)
                    consumer.CompleteConsumption();
            }
        }

        // Private Members
        private List<ConsumerBase> m_consumers = new List<ConsumerBase>();
        private bool m_guiEnabled;
        private bool m_throttle;
        private string m_workingDirectory;

        // Private Functions
        private void ConsumeInput(Input input)
        {
            if (input == null || !input.IsValid())
                ConsoleHelper.WriteLine(LogType.ERROR, "not valid input");

            foreach (ConsumerBase consumer in m_consumers)
            {
                if (consumer != null && consumer.GetConsumptionKeys().Contains(input.InputType))
                {
                    try
                    {
                        consumer.ConsumeInput(input);
                    }
                    catch (FileNotFoundException exception)
                    {
                        ConsoleHelper.WriteLine(LogType.ERROR, string.Format("Unable to access file for File Consumer - {0}", exception.Source));
                        ConsoleHelper.WriteLine(LogType.ERROR, "Removing File Consumer from process due to error.");
                        m_consumers.Remove(consumer);
                    }
                }
            }
        }
        private void Init(string[] inputList)
        {
            if ((inputList == null) || (inputList.Count() < 1))
                return;

            m_consumers.Add(new ConsumerConsole());

            if (Directory.Exists(m_workingDirectory))
                m_consumers.Add(new ConsumerFile(m_workingDirectory));
            else
                ConsoleHelper.WriteLine(LogType.ERROR, "Working directory not valid, no File consumer created");

            if (m_guiEnabled)
                m_consumers.Add(new ConsumerGui());

            ParseInputs(inputList);
        }
        private Input ParseInput(string input)
        {
            List<string> parts = input.Split(',').ToList();
            if (parts == null || parts.Count() < 2)
                return null;

            return new Input(parts[0], string.Join("", parts.GetRange(1, parts.Count() - 1)));
        }
        private void ParseInputs(string[] inputList)
        {
            for (int i = 0; i < inputList.Length; ++i)
            {
                Input newInput = ParseInput(inputList[i]);
                if (newInput != null && newInput.IsValid())
                    InputQueue.Add(newInput);
                else
                    ConsoleHelper.WriteLine(LogType.INFO, string.Format("Unable to parse input from ~{0}", i.ToString()));
            }

            ConsoleHelper.WriteLine(LogType.DEBUG, string.Format("Initial queue length: {0}", InputQueue.Count().ToString()));
        }
    }
}
