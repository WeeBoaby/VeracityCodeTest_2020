namespace Veracity_AlexMalcolm
{
    public abstract class ConsumerBase
    {
        // Public functions
        public ConsumerBase(string[] consumptionKeys)
        {
            this.m_consumptionKeys = consumptionKeys;
        }
        public string[] GetConsumptionKeys()
        {
            return m_consumptionKeys;
        }
        public virtual void ConsumeInput(Input input)
        {
            m_inputsConsumed++;
        }
        public abstract void CompleteConsumption();

        // Protected members
        protected int m_inputsConsumed = 0;

        // Private members
        private string[] m_consumptionKeys;
    }
}
