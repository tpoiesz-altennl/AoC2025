using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day8(bool testInput)
    {
        private List<List<Int64>> _input => InputReader.ReadAsNumberTuples(8, "\n", ",", testInput);

        private class Vec3 : Tuple<int, int, int>
        {
            public Vec3(int x, int y, int z) : base(x, y, z) { }
            public int X => Item1;
            public int Y => Item2;
            public int Z => Item3;

            public Int64 SquareDist(Vec3 other)
            {
                return (Int64)(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
            }
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
            _input.ForEach(v =>
                {
                    if (v[0] > int.MaxValue || v[1] > int.MaxValue || v[2] > int.MaxValue)
                        throw new ArgumentException("Provided input values are too big!");
                });
            List<Vec3> inputCopy = [];
            CustomSortedList connectionsByDistance = new(testInput ? 10 : 1000);
            foreach (List<Int64> v in _input)
            {
                Vec3 box = new((int)v[0], (int)v[1], (int)v[2]);
                foreach (Vec3 existing in inputCopy)
                    connectionsByDistance.AddSorted(new(box.SquareDist(existing), new(box, existing)), true);
                inputCopy.Add(box);
            }
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
            return "404";
        }
    }
}
