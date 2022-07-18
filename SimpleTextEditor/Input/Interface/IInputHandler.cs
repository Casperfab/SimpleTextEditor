namespace SimpleTextEditor.Input.Interface
{
    public interface IInputHandler
    {
        (int, int) Enter(List<InputValue> inputKeys, (int,int) cursorPosition);
        (int, int) Backspace(List<InputValue> inputKeys, (int, int) cursorPosition);
        (int, int) Space(List<InputValue> inputKeys, (int, int) cursorPosition);
        (int, int) Left(List<InputValue> inputKeys, (int, int) cursorPosition);
        (int, int) Right(List<InputValue> inputKeys, (int, int) cursorPosition);
        (int, int) Up(List<InputValue> inputKeys, (int, int) cursorPosition);
        (int, int) Down(List<InputValue> inputKeys, (int, int) cursorPosition);
        (int, int) Keys(List<InputValue> inputKeys, (int, int) cursorPosition, ConsoleKey consoleKey, ConsoleModifiers consoleModifiers);
    }
}
