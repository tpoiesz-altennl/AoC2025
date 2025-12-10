using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day2
    {
        private List<List<Int64>> _input;
        public Day2(bool testInput)
        {
            _input = InputReader.ReadAsNumberTuples(2, ",", "-", testInput);
        }

        public string GetSolution1()
        {
            Int64 runningTotal = 0;
            foreach (var tuple in _input) // Tuple is a pair of integers (necessarily so, based on input)
            {
                for (Int64 i = tuple[0]; i <= tuple[1]; ++i)
                {
                    if (i.ToString().Length % 2 != 0)
                    {
                        i = (Int64)Math.Pow((Int64)10, (i.ToString().Length));
                        continue;
                    }
                    bool repeatedNumber = true;
                    for (int c = 0; c < i.ToString().Length / 2; ++c)
                    {
                        if (i.ToString()[c] != i.ToString()[c + (i.ToString().Length / 2)])
                        {
                            repeatedNumber = false;
                            break;
                        }
                    }
                    if (repeatedNumber)
                    {
                        runningTotal += i;
                    }
                }
            }
            return $"Solution 1: {runningTotal}";
        }

        public string GetSolution2()
        {
            Int64 runningTotal = 0;
            foreach (var tuple in _input) // Tuple is a pair of integers (necessarily so, based on input)
            {
                for (Int64 i = tuple[0]; i <= tuple[1]; ++i)
                {
                    bool repeatedNumber = false;
                    for (int patternLength = 1; patternLength <= i.ToString().Length / 2; ++patternLength)
                    {
                        string pattern = i.ToString()[0..patternLength];
                        bool patternMatches = true;
                        if (i.ToString().Length % patternLength == 0)
                        {
                            for (int sub = 1; sub < i.ToString().Length / patternLength; ++sub)
                            {
                                if (!string.Equals(i.ToString()[(sub * patternLength)..((sub + 1) * patternLength)], pattern))
                                {
                                    patternMatches = false;
                                    break;
                                }
                            }
                            if (patternMatches)
                            {
                                repeatedNumber = true;
                                break;
                            }
                        }
                    }
                    if (repeatedNumber)
                    {
                        runningTotal += i;
                    }
                }
            }
            return $"Solution 2: {runningTotal}";
        }
    }
}
