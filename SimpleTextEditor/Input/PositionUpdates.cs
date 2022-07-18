using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SimpleTextEditor.Input
{
    public class PositionUpdates
    {
        public List<InputValue> UpdateScreenPositionAfterDelete((int, int) screenPosition, List<InputValue> inputValues, [Optional]InputValue input)
        {
            List<InputValue> result = inputValues;
            result.ForEach(p => Debug.WriteLine("Before delete update: " + p.ConsoleStringValue + " at position: " + p.ScreenPosition));
            result.FindAll(x => x.ScreenPosition.Item1 > screenPosition.Item1).ForEach(x => x.ScreenPosition = new(x.ScreenPosition.Item1 - 1, x.ScreenPosition.Item2));
            if (input != null && input.ConsoleKey.Equals(ConsoleKey.Enter))
            {
                foreach (var positions in result.FindAll(x => x.ScreenPosition.Item2 == screenPosition.Item2 + 1))
                {
                    var index = result.FindAll(x => x.ScreenPosition.Item2.Equals(screenPosition.Item2)).Max(v => v.ScreenPosition.Item1);
                    positions.ScreenPosition = new(index + 1, positions.ScreenPosition.Item2 - 1);
                }
            }
            result.ForEach(p => Debug.WriteLine("After delete update values: " + p.ConsoleStringValue + " at position: " + p.ScreenPosition));
            return result;
        }

        public List<InputValue> UpdateScreenPositionAfterAddition((int, int) screenPosition, List<InputValue> inputValues)
        {
            List<InputValue> result = inputValues;
            result.FindAll(x => x.ScreenPosition.Item1 >= screenPosition.Item1).ForEach(x => x.ScreenPosition = new(x.ScreenPosition.Item1 + 1, x.ScreenPosition.Item2));
            return result;
        }
        public List<InputValue> UpdateScreenPositionAfterEnterAddition((int, int) screenPosition, List<InputValue> inputValues)
        {
            List<InputValue> result = inputValues;
            int i = 0;
            result.ForEach(p => Debug.WriteLine("Before position update: " + p.ConsoleStringValue + " at position: " + p.ScreenPosition));
            foreach (var positions in result.FindAll(x => x.ScreenPosition.Item2 == screenPosition.Item2).FindAll(x => x.ScreenPosition.Item1 >= screenPosition.Item1))
            {
                positions.ScreenPosition = new(0 + i, positions.ScreenPosition.Item2 + 1);
                i++;
            }
            result.ForEach(p => Debug.WriteLine("After position update values: " + p.ConsoleStringValue + " at position: " + p.ScreenPosition));
            return result;
        }
    }
}
