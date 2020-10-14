using System;
using System.Windows;
using System.Windows.Controls;

namespace Veracity_AlexMalcolm
{
    public partial class GuiConsumerControl : UserControl
    {
        // Public functions
        public GuiConsumerControl()
        {
            InitializeComponent();
        }
        public void AddInput(Input input)
        {
            if (!input.IsValid())
                return;

            Dispatcher.Invoke(new Action(() =>
            {
                Label newLabel = new Label();
                newLabel.Content = string.Format("[{0}] {1}", ConsoleHelper.currentTimeFormatted(), input.InputContent);
                contentList.Items.Add(newLabel);
                contentList.ScrollIntoView(newLabel);
            }));
        }

        // Private functions
        private void DismissClicked(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
