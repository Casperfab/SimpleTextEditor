namespace SimpleTextEditor.Input.Interface
{
    public interface IInputValue
    {
        ConsoleKey ConsoleKey { get; set; }
        string? ConsoleStringValue { get; set; }
        (int, int) ScreenPosition { get; set; }
    }
}
