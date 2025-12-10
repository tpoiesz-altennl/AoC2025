using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day5
    {
        private Tuple<List<string>, List<string>> _input;
        public Day5(bool testInput)
        {
            _input = InputReader.ReadAsTwoStringLists(5, "\n", testInput);
        }

        private List<Tuple<Int64, Int64>> ParseRanges()
        {
            List<Tuple<Int64, Int64>> resultRanges = [];
            foreach (string rangeTuple in _input.Item1)
            {
                string[] ints = rangeTuple.Split('-');
                Tuple<Int64, Int64> newRange = new(Int64.Parse(ints[0]), Int64.Parse(ints[1]));
                // Check if it extends one of the existing ranges
                bool extendedExistingRange = false;
                int insertIndex = 0;
                // Update insertion index in loop increment; loop breaks when right index is found or existing range is updated
                for (int i = 0; i < resultRanges.Count; ++i, ++insertIndex)
                {
                    var existingRange = resultRanges[i];
                    if (newRange.Item1 >= existingRange.Item1 && newRange.Item2 <= existingRange.Item2)
                    {
                        // New range is fully enclosed by existing range
                        extendedExistingRange = true;
                        break;
                    }
                    if (newRange.Item1 <= existingRange.Item1 && newRange.Item2 >= existingRange.Item2)
                    {
                        extendedExistingRange = true;
                        // Update next range if necessary
                        if (i + 1 < resultRanges.Count && newRange.Item2 >= resultRanges[i + 1].Item1)
                        {
                            resultRanges[i] = new(newRange.Item1, resultRanges[i + 1].Item2);
                            resultRanges.RemoveAt(i + 1);
                        }
                        else
                            resultRanges[i] = newRange;
                        break;
                    }
                    else if (newRange.Item1 < existingRange.Item1 && newRange.Item2 >= existingRange.Item1)
                    {
                        extendedExistingRange = true;
                        resultRanges[i] = new(newRange.Item1, existingRange.Item2);
                        // No need to update next range
                        break;
                    }
                    else if (newRange.Item1 <= existingRange.Item2 && newRange.Item2 > existingRange.Item2)
                    {
                        extendedExistingRange = true;
                        // Update next range if necessary
                        if (i + 1 < resultRanges.Count && newRange.Item2 >= resultRanges[i + 1].Item1)
                        {
                            resultRanges[i] = new(existingRange.Item1, resultRanges[i + 1].Item2);
                            resultRanges.RemoveAt(i + 1);
                        }
                        else
                            resultRanges[i] = new(existingRange.Item1, newRange.Item2);
                        break;
                    }
                    else if (newRange.Item2 < resultRanges[i].Item1)
                        break; // Found the right position to insert new range
                }
                if (!extendedExistingRange)
                {
                    resultRanges.Insert(insertIndex, newRange);
                }
            }
            return resultRanges;
        }

        public string GetSolution1()
        {
            List<Int64> inventory = _input.Item2.Select(s => Int64.Parse(s)).ToList();
            inventory.Sort();
            List<Tuple<Int64, Int64>> ranges = ParseRanges();
            int freshCount = 0;
            foreach (Int64 item in inventory)
            {
                // Index of lowest range that any unchecked item could fall into (items are sorted in ascending order)
                int lowestRangeIndex = 0;
                for (int i = lowestRangeIndex; i < ranges.Count; ++i)
                {
                    var range = ranges[i];
                    if (item >= range.Item1 && item <= range.Item2)
                    {
                        ++freshCount;
                        break;
                    }
                    else if (item < range.Item1) // item is past any range it could fit into
                    {
                        lowestRangeIndex = i;
                        break;
                    }
                }
            }
            return $"Solution 1: {freshCount}";
        }

        public string GetSolution2()
        {
            List<Tuple<Int64, Int64>> ranges = ParseRanges();
            Int64 idCount = 0;
            foreach (var range in ranges)
            {
                idCount += range.Item2 - range.Item1 + 1;
            }
            return $"Solution 2: {idCount}";
        }
    }
}
