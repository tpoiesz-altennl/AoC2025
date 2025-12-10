using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day3
    {
        private List<string> _input;
        public Day3(bool testInput)
        {
            _input = InputReader.ReadAsStringList(3, testInput);
        }

        public string GetSolution1()
        {
            int runningTotal = 0;
            foreach (string bank in _input)
            {
                char highest = '\0', secondHighest = '\0';
                int firstIndexOfHighest = 0;
                for (int i = 0; i < bank.Length; ++i)
                {
                    if (bank[i] > highest)
                    {
                        if (i != bank.Length - 1)
                            secondHighest = '\0';
                        else
                            secondHighest = highest;
                        highest = bank[i];
                        firstIndexOfHighest = i;
                    }
                    else if (bank[i] > secondHighest)
                    {
                        secondHighest = bank[i];
                    }
                    if (highest == '9' && secondHighest == '9') break;
                }

                string result = "";
                if (firstIndexOfHighest != bank.Length - 1)
                    result = $"{highest}{secondHighest}";
                else
                    result = $"{secondHighest}{highest}";
                runningTotal += int.Parse(result);
            }
            return $"Solution 1: {runningTotal}";
        }

        public string GetSolution2()
        {
            Int64 runningTotal = 0;
            foreach (string bank in _input)
            {
                List<char> highestDigits =
                [
                    '\0', '\0', '\0', // Represented as a 4x3 grid for legibility, no other reason
                    '\0', '\0', '\0',
                    '\0', '\0', '\0',
                    '\0', '\0', '\0'
                ];
                for (int bankIndex = 0; bankIndex < bank.Length; ++bankIndex)
                {
                    // Adjust the first applicable digit that's lower than bank[bankIndex];
                    // A digit is applicable only if it's position in the 'highestDigits' number is not further from highestDigits.End than bankIndex is from bank.End
                    for (int highestDigitsIndex = int.Max(0, highestDigits.Count - (bank.Length - bankIndex)); highestDigitsIndex < highestDigits.Count; ++highestDigitsIndex)
                    {
                        if (bank[bankIndex] > highestDigits[highestDigitsIndex])
                        {
                            highestDigits[highestDigitsIndex] = bank[bankIndex];
                            // Reset for-loop; can be partially skipped if we know we haven't gone and changed some highestDigits yet
                            for (int resetIndex = highestDigitsIndex + 1; resetIndex < highestDigits.Count - int.Max(0, 11 - bankIndex); ++resetIndex)
                                highestDigits[resetIndex] = '\0';
                            break;
                        }
                    }
                    if (highestDigits.All(x => x == '9'))
                        break;
                }

                string result = "";
                foreach (char c in highestDigits)
                    result += c;
                runningTotal += Int64.Parse(result);
            }
            return $"Solution 2: {runningTotal}";
        }
    }
}
