using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day10
    {
        private List<string> _input;
        public Day10(bool testInput)
        {
            _input = InputReader.ReadAsStringList(10, testInput);
        }

        private struct InputItem(List<int> lights, List<List<int>> buttons, List<int> joltages)
        {
            public readonly List<int> Lights => lights;
            public readonly List<List<int>> Buttons => buttons;
            public readonly List<int> Joltages => joltages;
        }

        private List<InputItem> ParseInput()
        {
            List<InputItem> ret = [];
            foreach (string s in _input)
            {
                string[] subparts = s.Split(" ");

                // Strip square brackets from lights string, turn it into a list of ints with 1s at the positions of #s
                List<int> lights = subparts[0][1..^1].Select(c => c == '#' ? 1 : 0).ToList();

                List<List<int>> buttons = [];
                foreach (string buttonString in subparts[1..^1])
                {
                    // Make a lights-length list of 0s
                    buttons.Add(lights.Select(c => 0).ToList());
                    // Strip round brackets, parse to ints
                    int[] buttonNumbers = buttonString[1..^1].Split(",").Select(int.Parse).ToArray();
                    // Flip appropriate 0s to 1s
                    foreach (int n in buttonNumbers)
                        buttons[^1][n] = 1;
                }

                // Strip curly brackets from joltage input, split by commas, and parse each remaining int
                List<int> joltages = subparts[^1][1..^1].Split(",").Select(int.Parse).ToList();

                ret.Add(new(lights, buttons, joltages));
            }
            return ret;
        }

        private int RecursiveSolution1(Stack<List<int>> currentButtons, List<List<int>> availableButtons, int newButtonIdx, List<int> target, int bestCount = int.MaxValue)
        {
            if (currentButtons.Count >= bestCount)
                return int.MaxValue;

            if (currentButtons.Count > 0)
            {
                List<int> result = target.Select(i => 0).ToList();
                foreach (List<int> button in currentButtons)
                    for (int i = 0; i < button.Count; ++i)
                        result[i] = (result[i] + button[i]) % 2;
                bool isSolution = true;
                for (int i = 0; i < result.Count; ++i)
                {
                    if (result[i] != target[i])
                    {
                        isSolution = false;
                        break;
                    }
                }
                if (isSolution)
                    return currentButtons.Count;
            }

            if (newButtonIdx >= availableButtons.Count)
                return int.MaxValue;

            for (int i = newButtonIdx; i < availableButtons.Count; ++i)
            {
                currentButtons.Push(availableButtons[i]);
                int success = RecursiveSolution1(currentButtons, availableButtons, i + 1, target, bestCount);
                if (success < bestCount)
                    bestCount = success;
                currentButtons.Pop();
            }

            return bestCount;
        }

        private Int64 BruteForce1()
        {
            List<InputItem> processedInput = ParseInput();
            Int64 runningTotal = 0;
            foreach (InputItem item in processedInput)
            {
                runningTotal += RecursiveSolution1([], item.Buttons, 0, item.Lights);
            }
            return runningTotal;
        }

        public string GetSolution1()
        {
            return $"Solution 1: {BruteForce1()}";
        }


        private int RecursiveSolution2(Stack<List<int>> currentButtons, List<List<int>> availableButtons, List<int> target, int bestCount = int.MaxValue)
        {
            if (currentButtons.Count >= bestCount)
                return int.MaxValue;

            if (currentButtons.Count > 0)
            {
                List<int> result = target.Select(i => 0).ToList();
                foreach (List<int> button in currentButtons)
                    for (int i = 0; i < button.Count; ++i)
                        result[i] = (result[i] + button[i]);
                bool isSolution = true;
                for (int i = 0; i < result.Count; ++i)
                {
                    if (result[i] > target[i])
                        return int.MaxValue;
                    else if (result[i] < target[i])
                        isSolution = false;
                }
                if (isSolution)
                    return currentButtons.Count;
            }

            for (int i = 0; i < availableButtons.Count; ++i)
            {
                currentButtons.Push(availableButtons[i]);
                int success = RecursiveSolution2(currentButtons, availableButtons, target, bestCount);
                if (success < bestCount)
                    bestCount = success;
                currentButtons.Pop();
            }

            return bestCount;
        }

        private Int64 BruteForce2()
        {
            List<InputItem> processedInput = ParseInput();
            Int64 runningTotal = 0;
            foreach (InputItem item in processedInput)
            {
                runningTotal += RecursiveSolution2([], item.Buttons, item.Joltages);
            }
            return runningTotal;
        }

        public string GetSolution2()
        {
            return $"Solution 2: {BruteForce2()}";
        }
    }
}
