namespace Veracity_AlexMalcolm
{
    public class ConsumerConsole
        : ConsumerBase
    {
        // Public functions
        public ConsumerConsole() : base(new string[] { "Console" })
        {
        }
        public override void ConsumeInput(Input input)
        {
            ConsoleHelper.WriteLine(LogType.CONSOLE_CONSUMER, input.InputContent);
            base.ConsumeInput(input);
        }
        public override void CompleteConsumption()
        {
            ConsoleHelper.WriteLine(LogType.INFO, string.Format("Console Consumer - Consumption completed. {0} inputs consumed", m_inputsConsumed.ToString()));
        }
    }
}
