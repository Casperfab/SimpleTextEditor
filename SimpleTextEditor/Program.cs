using SimpleTextEditor.Input;

namespace SimpleTextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            List<InputValue> inputKeys = new();
            (int, int) focusKeyStroke = (0, 0);
            InputHandler inputHandler = new();
            while (true)
            {
                var key = Console.ReadKey(true);
                var keyPressed = key.Key;
                var keyModiferPressed = key.Modifiers;
                var cursorPosition = Console.GetCursorPosition();
                focusKeyStroke = keyPressed switch
                {
                    ConsoleKey.Enter => inputHandler.Enter(inputKeys, cursorPosition),
                    ConsoleKey.Backspace => inputHandler.Backspace(inputKeys, cursorPosition),
                    ConsoleKey.Spacebar => inputHandler.Space(inputKeys, cursorPosition),
                    ConsoleKey.LeftArrow => inputHandler.Left(inputKeys, cursorPosition),
                    ConsoleKey.RightArrow => inputHandler.Right(inputKeys, cursorPosition),
                    ConsoleKey.UpArrow => inputHandler.Up(inputKeys, cursorPosition),
                    ConsoleKey.DownArrow => inputHandler.Down(inputKeys, cursorPosition),
                    _ => inputHandler.Keys(inputKeys, cursorPosition, keyPressed, keyModiferPressed),
                };
                Console.Clear();
                inputKeys.ForEach(p => Console.Write(p.ConsoleStringValue));
                Console.SetCursorPosition(focusKeyStroke.Item1, focusKeyStroke.Item2);
            }
        }
    }
}