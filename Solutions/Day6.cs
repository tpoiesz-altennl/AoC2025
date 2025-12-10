using AoC2025.Utilities;
using System.Text.RegularExpressions;

namespace AoC2025.Solutions
{
    public class Day6
    {
        private List<string> _input;
        public Day6(bool testInput)
        {
            _input = InputReader.ReadAsStringList(6, testInput);
        }

        public string GetSolution1()
        {
            List<List<string>> inputNumbersCollection = [];
            for (int i = 0; i < _input.Count - 1; ++i)
            {
                List<string> col = _input[i].Split(' ').ToList();
                col.ForEach(s => Regex.Replace(s, @"\s+", ""));
                col.RemoveAll(s => string.IsNullOrEmpty(s));
                inputNumbersCollection.Add(col);
            }
            // Last line is a row of operators for the assignments
            List<string> operators = _input.Last().Split(' ').ToList();
            operators.ForEach(s => Regex.Replace(s, @"\s+", ""));
            operators.RemoveAll(s => string.IsNullOrEmpty(s));

            Int64 runningTotal = 0;
            for (int i = 0; i < operators.Count; ++i)
            {
                string op = operators[i];
                Int64 result = (op.Equals("*") ? 1 : 0);
                foreach (List<string> list in inputNumbersCollection)
                {
                    Int64 nextNumber = Int64.Parse(list[i]);
                    if (operators[i].Equals("*"))
                        result *= nextNumber;
                    else if (op.Equals("+"))
                        result += nextNumber;
                }
                runningTotal += result;
            }
            return $"Solution 1: {runningTotal}";
        }

        public string GetSolution2()
        {
            List<string> assignmentNumbers = _input;
            assignmentNumbers.RemoveAt(assignmentNumbers.Count - 1);
            List<string> assignmentOperators = _input.Last().Split(" ").ToList();
            assignmentOperators.ForEach(s => Regex.Replace(s, @"\s+", ""));
            assignmentOperators.RemoveAll(s => string.IsNullOrEmpty(s));

            Int64 runningTotal = 0;
            int opIdx = assignmentOperators.Count - 1;
            List<Int64> currAssignment = [];
            for (int backwardsIndex = assignmentNumbers[0].Length - 1; backwardsIndex >= 0; --backwardsIndex)
            {
                bool allWhiteSpace = true;
                Int64 newNumber = 0;
                int currentPower = 0;
                for (int i = 0; i < assignmentNumbers.Count; ++i)
                {
                    char current = assignmentNumbers[assignmentNumbers.Count - i - 1][backwardsIndex];
                    if (current == ' ')
                        continue;
                    else
                    {
                        allWhiteSpace = false;
                        newNumber += (current - '0') * (Int64)Math.Pow(10.0, currentPower);
                        ++currentPower;
                    }
                }
                if (allWhiteSpace)
                {
                    Int64 assignmentTotal = (assignmentOperators[opIdx].Equals("*") ? 1 : 0);
                    foreach (Int64 num in currAssignment)
                    {
                        if (assignmentOperators[opIdx].Equals("*"))
                            assignmentTotal *= num;
                        else if (assignmentOperators[opIdx].Equals("+"))
                            assignmentTotal += num;
                    }
                    runningTotal += assignmentTotal;
                    currAssignment.Clear();
                    --opIdx;
                }
                else
                {
                    currAssignment.Add(newNumber);
                }
            }
            // Don't forget to process the last assignment (ignored by the for-loop, because there's no whitespace column before it)
            {
                Int64 assignmentTotal = (assignmentOperators[opIdx].Equals("*") ? 1 : 0);
                foreach (Int64 num in currAssignment)
                {
                    if (assignmentOperators[opIdx].Equals("*"))
                        assignmentTotal *= num;
                    else if (assignmentOperators[opIdx].Equals("+"))
                        assignmentTotal += num;
                }
                runningTotal += assignmentTotal;
            }

            return $"Solution 2: {runningTotal}";
        }
    }
}
