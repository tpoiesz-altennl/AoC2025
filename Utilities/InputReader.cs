namespace AoC2025.Utilities
{
    public static class InputReader
    {
        public static List<string> ReadAsStringList(int day, bool test = false)
        {
            string filepath = $"{Environment.CurrentDirectory}\\Inputs\\Day{day}{(test ? "Test" : "")}.txt";
            List<string> resultLines = [];
            using (StreamReader sr = new(filepath))
            {
                string? line = sr.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    resultLines.Add(line);
                    line = sr.ReadLine();
                }
            }
            return resultLines;
        }

        public static Grid ReadAsGrid(int day, bool test = false)
        {
            return new Grid(ReadAsStringList(day, test));
        }

        public static List<List<Int64>> ReadAsNumberTuples(int day, string inputSeparator, string tupleSeparator = "", bool test = false)
        {
            string filepath = $"{Environment.CurrentDirectory}\\Inputs\\Day{day}{(test ? "Test" : "")}.txt";
            List<List<Int64>> results = [];
            using (StreamReader sr = new(filepath))
            {
                string? line = sr.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    string[] entry = line.Split(inputSeparator);
                    if (tupleSeparator != "")
                    {
                        foreach (string sub in entry)
                        {
                            string[] subEntry = sub.Split(tupleSeparator);
                            results.Add(subEntry.Select(s => Int64.Parse(s)).ToList());
                        }
                    }
                    else
                    {
                        results.Add([int.Parse(entry[0])]);
                    }
                    line = sr.ReadLine();
                }
            }
            return results;
        }

        public static Tuple<List<string>, List<string>> ReadAsTwoStringLists(int day, string listSeparator = "\n", bool test = false)
        {
            string filepath = $"{Environment.CurrentDirectory}\\Inputs\\Day{day}{(test ? "Test" : "")}.txt";
            Tuple<List<string>, List<string>> result = new([], []);
            using (StreamReader sr = new(filepath))
            {
                string? line = sr.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    result.Item1.Add(line);
                    line = sr.ReadLine();
                    if (line != null && line.Equals(listSeparator))
                        break;
                }
                line = sr.ReadLine();
                while (!String.IsNullOrEmpty(line))
                {
                    result.Item2.Add(line);
                    line = sr.ReadLine();
                }
            }
            return result;
        }
    }
}
