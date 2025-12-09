namespace AoC2025.Utilities
{
    public static class OutputWriter
    {
        public static void WriteGridToFile(int day, List<string> grid)
        {
            string filepath = $"{Environment.CurrentDirectory}\\Outputs\\Day{day}Output.txt";
            using (StreamWriter sw = File.CreateText(filepath))
            {
                foreach (string row in grid)
                    sw.WriteLine(row);
            }
        }

        public static void WriteGridToFile(int day, Grid grid)
        {
            string filepath = $"{Environment.CurrentDirectory}\\Outputs\\Day{day}Output.txt";
            using (StreamWriter sw = File.CreateText(filepath))
            {
                for (int i = 0; i < grid.Height(); ++i)
                    sw.WriteLine(grid[i]);
            }
        }
    }
}
