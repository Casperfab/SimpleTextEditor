using SimpleTextEditor.Input.Interface;

namespace SimpleTextEditor.Input
{
    public class InputHandler : IInputHandler
    {
        private readonly PositionUpdates _positionUpdates;

        public InputHandler()
        {
            _positionUpdates = new PositionUpdates();
        }
        public (int, int) Backspace(List<InputValue> inputKeys, (int, int) cursorPosition)
        {
            (int,int) position = cursorPosition;
            if (inputKeys.Count != 0)
            {
                var keyToRemove = inputKeys.Find(x => x.ScreenPosition.Equals(cursorPosition));
                if (keyToRemove != null)
                {
                    position = keyToRemove.ScreenPosition;
                    inputKeys.Remove(keyToRemove);
                    _positionUpdates.UpdateScreenPositionAfterDelete(cursorPosition, inputKeys, keyToRemove);
                }
            }
            return position;
        }

        public (int, int) Down(List<InputValue> inputKeys, (int, int) cursorPosition)
        {
            (int, int) position = cursorPosition;
            if (Console.CursorTop < 10 && inputKeys.Count > 0)
            {
                var lowestPosition = inputKeys.Max(v => v.ScreenPosition.Item2);
                var currentLowestConsolePosition = inputKeys.Max(v => v.ScreenPosition.Item2) > Console.CursorTop + 1 ? Console.CursorTop + 1 : lowestPosition;
                var mostRightPositionOfNextLine = inputKeys.FindAll(x => x.ScreenPosition.Item2.Equals(currentLowestConsolePosition)).Max(v => v.ScreenPosition.Item1);
                if (Console.CursorTop < lowestPosition && Console.CursorLeft <= mostRightPositionOfNextLine)
                    position.Item2++;
            }
            return position;
        }

        public (int, int) Enter(List<InputValue> inputKeys, (int, int) cursorPosition)
        {
            (int, int) position = cursorPosition;
            var index = inputKeys.IndexOf(inputKeys.Find(x => x.ScreenPosition.Equals(cursorPosition)));
            if (index >= 0)
            {
                _positionUpdates.UpdateScreenPositionAfterEnterAddition(cursorPosition, inputKeys);
                inputKeys.Insert(index, new InputValue
                {
                    ConsoleKey = ConsoleKey.Enter,
                    ConsoleStringValue = "\n",
                    ScreenPosition = cursorPosition,
                });
            }
            else
            {
                inputKeys.Add(new InputValue
                {
                    ConsoleKey = ConsoleKey.Enter,
                    ConsoleStringValue = "\n",
                    ScreenPosition = cursorPosition,
                });
            }
            /*historicalKeys.Add(new InputValues
            {
                ConsoleKey = ConsoleKey.Enter,
                ConsoleStringValue = "\n",
                ScreenPosition = Console.GetCursorPosition(),
            });*/
            position.Item1 = 0;
            position.Item2++;
            return position;
        }

        public (int, int) Keys(List<InputValue> inputKeys, (int, int) cursorPosition, ConsoleKey keyPressed, ConsoleModifiers keyModiferPressed)
        {
            /*historicalKeys.Add(new InputValues
                        {
                            ConsoleKey = keyPressed,
                            ConsoleStringValue = keyPressed.ToString(),
                            ScreenPosition = Console.GetCursorPosition(),
                        });*/
            (int, int) position = cursorPosition;
            string keyString;
            if (keyModiferPressed.Equals(ConsoleModifiers.Shift))
                keyString = keyPressed.ToString();
            else
                keyString = keyPressed.ToString().ToLower();
            if (Enum.IsDefined(typeof(SupportedKeyStrokes), keyString))
            {
                var index2 = inputKeys.IndexOf(inputKeys.Find(x => x.ScreenPosition.Equals(cursorPosition)));
                if (index2 >= 0)
                {
                    _positionUpdates.UpdateScreenPositionAfterAddition(cursorPosition, inputKeys);
                    inputKeys.Insert(index2, new InputValue
                    {
                        ConsoleKey = keyPressed,
                        ConsoleStringValue = keyString,
                        ScreenPosition = cursorPosition,
                    });
                }
                else
                {
                    inputKeys.Add(new InputValue
                    {
                        ConsoleKey = keyPressed,
                        ConsoleStringValue = keyString,
                        ScreenPosition = cursorPosition,
                    });
                }
                position.Item1++;
            }
            return position;
        }

        public (int, int) Left(List<InputValue> inputKeys, (int, int) cursorPosition)
        {
            (int, int) position = cursorPosition;
            if (!cursorPosition.Item1.Equals(0))
            {
                position.Item1--;
            }
            return position;
        }

        public (int, int) Right(List<InputValue> inputKeys, (int, int) cursorPosition)
        {
            (int, int) position = cursorPosition;
            if (cursorPosition.Item1 < 30 && inputKeys.Count > 0)
            {
                var maxRightPos = inputKeys.FindAll(x => x.ScreenPosition.Item2.Equals(Console.CursorTop)).Max(v => v.ScreenPosition.Item1);
                if (Console.CursorLeft < maxRightPos)
                    position.Item1++;   
            }
            return position;
        }

        public (int, int) Space(List<InputValue> inputKeys, (int, int) cursorPosition)
        {
            (int, int) position = cursorPosition;
            var index1 = inputKeys.IndexOf(inputKeys.Find(x => x.ScreenPosition.Equals(cursorPosition)));
            if (index1 >= 0)
            {
                _positionUpdates.UpdateScreenPositionAfterAddition(cursorPosition, inputKeys);
                inputKeys.Insert(index1, new InputValue
                {
                    ConsoleKey = ConsoleKey.Spacebar,
                    ConsoleStringValue = " ",
                    ScreenPosition = cursorPosition,
                });
            }
            else
            {
                inputKeys.Add(new InputValue
                {
                    ConsoleKey = ConsoleKey.Spacebar,
                    ConsoleStringValue = " ",
                    ScreenPosition = cursorPosition,
                });
            }
            position.Item1++;
            return position;
        }

        public (int, int) Up(List<InputValue> inputKeys, (int, int) cursorPosition)
        {
            (int, int) position = cursorPosition;
            if (Console.CursorTop > 0 && inputKeys.Count > 0)
            {
                var mostRightPositionOfNextLine = inputKeys.FindAll(x => x.ScreenPosition.Item2.Equals(Console.CursorTop - 1)).Max(v => v.ScreenPosition.Item1);
                if (Console.CursorTop > 0 && Console.CursorLeft <= mostRightPositionOfNextLine)
                    position.Item2--;
            }
            return position;
        }
    }
}
