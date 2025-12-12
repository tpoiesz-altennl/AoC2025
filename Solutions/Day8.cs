using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day8
    {
        private List<List<Int64>> _input;
        private bool _test;
        public Day8(bool testInput)
        {
            _input = InputReader.ReadAsNumberTuples(8, "", ",", testInput);
            _test = testInput;
        }

        private class CustomSortedList(int capacity) : List<Tuple<Int64, Tuple<Vec3, Vec3>>>(capacity)
        {
            public void AddSorted(Tuple<Int64, Tuple<Vec3, Vec3>> item, bool fixedCapacity = false)
            {
                if (Count == 0 || item.Item1 < this[0].Item1)
                {
                    if (fixedCapacity && Count == Capacity)
                        RemoveAt(Count - 1);
                    Insert(0, item);
                }
                else if (Count > 0 && item.Item1 > this[Count - 1].Item1)
                {
                    if (fixedCapacity && Count == Capacity)
                        return;
                    Add(item);
                }
                else
                {
                    if (fixedCapacity && Count == Capacity)
                        RemoveAt(Count - 1);
                    Insert(DetermineInsertionIndex(item.Item1, 0, Count - 1), item);
                }
            }

            private int DetermineInsertionIndex(Int64 sortBy, int lowerFence, int upperFence)
            {
                if (upperFence == 0)
                    return 0;
                if (lowerFence == Count - 1)
                    return Count - 1;
                if (upperFence - lowerFence == 1)
                    return upperFence;
                int halfwayIdx = lowerFence + (upperFence - lowerFence) / 2;
                if (sortBy == this[halfwayIdx].Item1)
                    return halfwayIdx;
                if (sortBy < this[halfwayIdx].Item1)
                    return DetermineInsertionIndex(sortBy, lowerFence, halfwayIdx);
                if (sortBy > this[halfwayIdx].Item1)
                    return DetermineInsertionIndex(sortBy, halfwayIdx, upperFence);
                throw new InvalidOperationException("Attempting to insert invalid item into CustomSortedList");
            }
        }

        public string GetSolution1()
        {
            List<Vec3> inputCopy = [];
            CustomSortedList connectionsByDistance = new(_test ? 10 : 1000);
            foreach (List<Int64> v in _input)
            {
                Vec3 box = new(v[0], v[1], v[2]);
                foreach (Vec3 existing in inputCopy)
                    connectionsByDistance.AddSorted(new(box.SquareDist(existing), new(box, existing)), true);
                inputCopy.Add(box);
            }
            inputCopy.Clear();
            var circuits = BuildCircuits(connectionsByDistance);
            // Find the biggest three
            Tuple<int, int, int> biggestThree = new(0, 0, 0);
            foreach (var circuit in circuits)
            {
                if (circuit.Count > biggestThree.Item1)
                    biggestThree = new(circuit.Count, biggestThree.Item1, biggestThree.Item2);
                else if (circuit.Count > biggestThree.Item2)
                    biggestThree = new(biggestThree.Item1, circuit.Count, biggestThree.Item2);
                else if (circuit.Count > biggestThree.Item3)
                    biggestThree = new(biggestThree.Item1, biggestThree.Item2, circuit.Count);
            }
            return $"Solution 1: {biggestThree.Item1 * biggestThree.Item2 * biggestThree.Item3}";
        }

        private List<HashSet<Vec3>> BuildCircuits(List<Tuple<Int64, Tuple<Vec3, Vec3>>> connections)
        {
            List<HashSet<Vec3>> allCircuits = [];
            foreach (var connection in connections)
            {
                List<HashSet<Vec3>> circuitsToExpand = [];
                for (int i = 0; i < allCircuits.Count; ++i)
                {
                    HashSet<Vec3> circuit = allCircuits[i];
                    if (circuit.Contains(connection.Item2.Item1))
                    {
                        circuitsToExpand.Add(circuit);
                        allCircuits.RemoveAt(i--);
                    }
                    else if (circuit.Contains(connection.Item2.Item2))
                    {
                        circuitsToExpand.Add(circuit);
                        allCircuits.RemoveAt(i--);
                    }
                }
                if (circuitsToExpand.Count > 0)
                {
                    circuitsToExpand[0].UnionWith([connection.Item2.Item1, connection.Item2.Item2]);
                    for (int i = 1; i < circuitsToExpand.Count; ++i)
                    {
                        circuitsToExpand[0].UnionWith(circuitsToExpand[i]);
                    }
                    allCircuits.Add(circuitsToExpand[0]);
                }
                else
                {
                    allCircuits.Add([connection.Item2.Item1, connection.Item2.Item2]);
                }
            }
            return allCircuits;
        }

        public string GetSolution2()
        {
            List<Vec3> inputCopy = [];
            CustomSortedList connectionsByDistance = new(_test ? 10 : 1000);
            foreach (List<Int64> v in _input)
            {
                Vec3 box = new(v[0], v[1], v[2]);
                foreach (Vec3 existing in inputCopy)
                    connectionsByDistance.AddSorted(new(box.SquareDist(existing), new(box, existing)), false);
                inputCopy.Add(box);
            }
            var circuits = BuildWholeNetwork(connectionsByDistance, inputCopy, out Tuple<Vec3, Vec3> lastConnection);
            return $"Solution 2: {(Int64)lastConnection.Item1.X * (Int64)lastConnection.Item2.X}";
        }

        private List<HashSet<Vec3>> BuildWholeNetwork(List<Tuple<Int64, Tuple<Vec3, Vec3>>> connections, List<Vec3> separatedNodes, out Tuple<Vec3, Vec3> lastConnection)
        {
            List<HashSet<Vec3>> allCircuits = [];
            foreach (var connection in connections)
            {
                List<HashSet<Vec3>> circuitsToExpand = [];
                for (int i = 0; i < allCircuits.Count; ++i)
                {
                    HashSet<Vec3> circuit = allCircuits[i];
                    if (circuit.Contains(connection.Item2.Item1))
                    {
                        circuitsToExpand.Add(circuit);
                        allCircuits.RemoveAt(i--);
                    }
                    else if (circuit.Contains(connection.Item2.Item2))
                    {
                        circuitsToExpand.Add(circuit);
                        allCircuits.RemoveAt(i--);
                    }
                }
                if (circuitsToExpand.Count > 0)
                {
                    circuitsToExpand[0].UnionWith([connection.Item2.Item1, connection.Item2.Item2]);
                    for (int i = 1; i < circuitsToExpand.Count; ++i)
                    {
                        circuitsToExpand[0].UnionWith(circuitsToExpand[i]);
                    }
                    allCircuits.Add(circuitsToExpand[0]);
                }
                else
                {
                    allCircuits.Add([connection.Item2.Item1, connection.Item2.Item2]);
                }
                separatedNodes.Remove(connection.Item2.Item1);
                separatedNodes.Remove(connection.Item2.Item2);
                if (separatedNodes.Count == 0 && allCircuits.Count == 1)
                {
                    lastConnection = connection.Item2;
                    return allCircuits;
                }
            }
            lastConnection = new(new(-1, -1, -1), new(-1, -1, -1));
            return [];
        }
    }
}
