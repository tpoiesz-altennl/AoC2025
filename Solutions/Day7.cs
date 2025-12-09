using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    using Coord = Grid.Coord;
    public class Day7
    {
        private Grid _input;
        public Day7(bool testInput)
        {
            _input = InputReader.ReadAsGrid(7, testInput);
        }

        public string GetSolution1(bool writeDebug = false)
        {
            Coord start = new(-1, -1);
            Grid inputCopy = new(_input);
            if (!inputCopy.GetFirstOccurrence(ref start, 'S'))
                return "404";

            Queue<Coord> searchSpace = [];
            searchSpace.Enqueue(start);
            int splitCount = 0;
            while (searchSpace.Count > 0)
            {
                Coord next = searchSpace.Dequeue().Next(Grid.Direction.South);
                if (inputCopy.IsValidIndex(next))
                {
                    if (inputCopy.Get(next) == '.')
                    {
                        searchSpace.Enqueue(next);
                        inputCopy.Set(next, '|');
                    }
                    else if (inputCopy.Get(next) == '^')
                    {
                        inputCopy.Set(next, '+');
                        if (inputCopy.Get(next.Next(Grid.Direction.West)) == '.')
                        {
                            searchSpace.Enqueue(next.Next(Grid.Direction.West));
                            inputCopy.Set(next.Next(Grid.Direction.West), '|');
                        }
                        if (inputCopy.Get(next.Next(Grid.Direction.East)) == '.')
                        {
                            searchSpace.Enqueue(next.Next(Grid.Direction.East));
                            inputCopy.Set(next.Next(Grid.Direction.East), '|');
                        }
                        ++splitCount;
                    }
                }
            }
            if (writeDebug)
                OutputWriter.WriteGridToFile(7, inputCopy);

            return $"Solution 1: {splitCount}";
        }

        private struct SplitNode
        {
            public SplitNode(Coord pos)
            {
                ResultPaths = 0;
                Position = pos;
            }
            public SplitNode(Coord pos, Int64 paths)
            {
                ResultPaths = paths;
                Position = pos;
            }

            public Coord Position;
            public Int64 ResultPaths;
        }

        public string GetSolution2(bool writeDebug = false)
        {
            SplitNode start = new(new Coord(-1, -1));
            Grid inputCopy = new(_input);
            if (!inputCopy.GetFirstOccurrence(ref start.Position, 'S'))
                return "404";
            Coord? firstSplitter = FindNextNode(start.Position, ref inputCopy, writeDebug);
            if (firstSplitter == null)
                return "404";
            List<SplitNode> history = [];
            start.ResultPaths = ExploreNode(firstSplitter, ref inputCopy, ref history, writeDebug);

            if (writeDebug)
            {
                foreach (SplitNode node in history)
                {
                    inputCopy.Set(node.Position, (char)(node.ResultPaths + '0'));
                }
                OutputWriter.WriteGridToFile(7, inputCopy);
            }
            return $"Solution 2: {start.ResultPaths}";
        }

        private Int64 ExploreNode(Coord current, ref Grid grid, ref List<SplitNode> history, bool tracePaths = false)
        {
            Int64 exitPaths = 0;
            Coord nextLeft = current.Next(Grid.Direction.West);
            if (grid.IsValidIndex(nextLeft))
            {
                Coord? nextNode = FindNextNode(nextLeft, ref grid, tracePaths);
                if (nextNode != null)
                {
                    if (history.Any(n => n.Position == nextNode))
                    {
                        exitPaths += history.Find(n => n.Position == nextNode).ResultPaths;
                    }
                    else
                    {
                        SplitNode n = new(nextNode, ExploreNode(nextNode, ref grid, ref history, tracePaths));
                        exitPaths += n.ResultPaths;
                        history.Add(n);
                    }
                }
                else
                {
                    // If we go off the grid, we've found an exit path
                    exitPaths += 1;
                }
            }

            Coord nextRight = current.Next(Grid.Direction.East);
            if (grid.IsValidIndex(nextRight))
            {
                Coord? nextNode = FindNextNode(nextRight, ref grid, tracePaths);
                if (nextNode != null)
                {
                    if (history.Any(n => n.Position == nextNode))
                    {
                        exitPaths += history.Find(n => n.Position == nextNode).ResultPaths;
                    }
                    else
                    {
                        SplitNode n = new(nextNode, ExploreNode(nextNode, ref grid, ref history, tracePaths));
                        exitPaths += n.ResultPaths;
                        history.Add(n);
                    }
                }
                else
                {
                    // If we go off the grid, we've found an exit path
                    exitPaths += 1;
                }
            }

            return exitPaths;
        }

        private Coord? FindNextNode(Coord current, ref Grid grid, bool tracePath = false)
        {
            while (grid.IsValidIndex(current) && grid.Get(current) != '^')
            {
                if (tracePath)
                {
                    grid.Set(current, '|');
                }
                current = current.Next(Grid.Direction.South);
            }

            if (grid.IsValidIndex(current))
                return current; // Found a node
            else
                return null; // Went off the grid
        }
    }
}
