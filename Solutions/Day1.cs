using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day1
    {
        private List<string> _input;
        public Day1(bool testInput)
        {
            _input = InputReader.ReadAsStringList(1, testInput);
        }

        public string GetSolution1()
        {
            int runningTotal = 50;
            int zeroCounter = 0;
            foreach (string turn in _input)
            {
                if (turn[0] == 'L')
                    runningTotal -= int.Parse(turn[1..]);
                else
                    runningTotal += int.Parse(turn[1..]);
                if (runningTotal >= 100 || runningTotal <= -100)
                {
                    runningTotal %= 100;
                }
                if (runningTotal == 0)
                    zeroCounter++;
            }
            return "Solution 1: " + zeroCounter;
        }

        public string GetSolution2()
        {
            int runningTotal = 50;
            int zeroCounter = 0;
            foreach (string turn in _input)
            {
                bool checkSignFlip = runningTotal != 0;
                bool prevSignPositive = runningTotal > 0;

                if (turn[0] == 'L')
                    runningTotal -= int.Parse(turn[1..]);
                else
                    runningTotal += int.Parse(turn[1..]);
                if (checkSignFlip && prevSignPositive != runningTotal > 0)
                    zeroCounter++;
                else if (runningTotal == 0) // Sign flips and ending on exactly 0 are mutually exclusive
                    zeroCounter++;
                if (runningTotal >= 100 || runningTotal <= -100)
                {
                    zeroCounter += Math.Abs(runningTotal / 100);
                    runningTotal %= 100;
                }

            }
            return "Solution 1: " + zeroCounter;
        }
    }
}
