using System;
using System.Threading;
using System.Windows;

namespace Veracity_AlexMalcolm
{
    public class ConsumerGui
        : ConsumerBase
    {
        // Public functions
        public ConsumerGui() : base(new string[] { "Console", "File", "Gui" })
        {
            CreateGuiOnThread();
        }
        public override void CompleteConsumption()
        {
            ConsoleHelper.WriteLine(LogType.INFO, string.Format("GUI Consumer - Consumption completed. {0} inputs consumed", m_inputsConsumed.ToString()));
        }
        public override void ConsumeInput(Input input)
        {
            if (m_consumerControl != null)
            {
                m_consumerControl.AddInput(input);
                base.ConsumeInput(input);
            }
        }

        // Private members
        private GuiConsumerControl m_consumerControl;

        // Private functions
        private void CreateGuiOnThread()
        {
            Thread consumerThread = new Thread(new ThreadStart(ThreadEntry));
            consumerThread.SetApartmentState(ApartmentState.STA);
            consumerThread.IsBackground = true;
            consumerThread.Start();
            Thread.Sleep(1000);
        }
        private void ConsumerClosed(object sender, EventArgs e)
        {
            m_consumerControl = null;
        }
        private void ThreadEntry()
        {
            m_consumerControl = new GuiConsumerControl();
            Window window = new Window
            {
                Title = "Alex Malcolm - Veracity - GUI Consumer",
                Content = m_consumerControl
            };
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Closed += new EventHandler(ConsumerClosed);
            window.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
