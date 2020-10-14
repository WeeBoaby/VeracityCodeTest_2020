namespace Veracity_AlexMalcolm
{
    public class Input
    {
        // Public members
        public string InputType { get; }
        public string InputContent { get; }

        // Public functions
        public Input(string type, string content)
        {
            InputType = type;
            InputContent = content;
        }
        public bool IsValid()
        {
            return InputType != null && InputType != string.Empty
                && InputContent != null && InputContent != string.Empty;
        }
    }
}
