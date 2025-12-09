using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day4
    {
        private List<string> _input;
        private bool _test;
        public Day4(bool testInput)
        {
            _input = InputReader.ReadAsStringList(4, testInput);
            _test = testInput;
        }

        private int BruteForceSolution1(bool removeRolls = false)
        {
            int movableRolls = 0;
            for (int row = 0; row < _input.Count; ++row)
            {
                for (int col = 0; col < _input[row].Length; ++col)
                {
                    if (_input[row][col] == '@')
                    {
                        int adjacentCount = 0;
                        if (row > 0)
                        {
                            if (col > 0 && _input[row - 1][col - 1] == '@')
                                ++adjacentCount;
                            if (_input[row - 1][col] == '@')
                                ++adjacentCount;
                            if (col < _input[row].Length - 1 && _input[row - 1][col + 1] == '@')
                                ++adjacentCount;
                        }
                        if (col > 0 && _input[row][col - 1] == '@')
                            ++adjacentCount;
                        if (col < _input[row].Length - 1 && _input[row][col + 1] == '@')
                            ++adjacentCount;
                        if (row < _input.Count - 1)
                        {
                            if (col > 0 && _input[row + 1][col - 1] == '@')
                                ++adjacentCount;
                            if (_input[row + 1][col] == '@')
                                ++adjacentCount;
                            if (col < _input[row].Length - 1 && _input[row + 1][col + 1] == '@')
                                ++adjacentCount;
                        }

                        if (adjacentCount < 4)
                        {
                            ++movableRolls;
                            if (removeRolls)
                            {
                                char[] newRow = _input[row].ToCharArray();
                                newRow[col] = '.';
                                _input[row] = new string(newRow);
                                if (_test)
                                    OutputWriter.WriteGridToFile(4, _input);
                            }
                        }
                    }
                }
            }
            return movableRolls;
        }

        public string GetSolution1()
        {
            return $"Solution 1: {BruteForceSolution1()}";
        }

        private int BruteForceSolution2()
        {
            int previousRemovals = -1;
            int currentRemovals = 0;
            while (previousRemovals < currentRemovals)
            {
                previousRemovals = currentRemovals;
                currentRemovals += BruteForceSolution1(true);
            }
            return currentRemovals;
        }

        public string GetSolution2()
        {
            return $"Solution 2: {BruteForceSolution2()}";
        }
    }
}
