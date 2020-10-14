using System.Collections.Generic;
using System.Linq;

namespace Veracity_AlexMalcolm
{
    public class ProducerPrioritised
        : Producer
    {
        // Public functions
        public ProducerPrioritised(string priority, string[] inputList, string workingDirectory, bool throttle, bool enableGui)
            : base(inputList, workingDirectory, throttle, enableGui)
        {
            this.m_priority = priority;
        }
        public override void Produce()
        {
            SortInputs();
            base.Produce();
        }

        // Private members
        private string m_priority;

        // Private functions
        private void SortInputs()
        {
            ConsoleHelper.WriteLine(LogType.DEBUG, "sorting input order");
            IEnumerable<Input> inputSetPriority = InputQueue.Where(input => input.InputType == m_priority);
            IEnumerable<Input> inputSet = InputQueue.Where(input => input.InputType != m_priority);

            InputQueue = inputSetPriority.ToList();
            InputQueue.AddRange(inputSet);
        }
    }
}
