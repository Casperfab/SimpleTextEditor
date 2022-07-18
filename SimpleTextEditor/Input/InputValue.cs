using SimpleTextEditor.Input.Interface;

namespace SimpleTextEditor.Input
{
    public class InputValue : IInputValue
    {
        public ConsoleKey ConsoleKey { get; set; }
        public string? ConsoleStringValue { get; set; }
        public (int, int) ScreenPosition { get; set; }
    }
}
