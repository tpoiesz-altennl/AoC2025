using AoC2025.Utilities;

namespace AoC2025.Solutions
{
    public class Day11
    {
        private List<string> _input;
        public Day11(bool testInput)
        {
            _input = InputReader.ReadAsStringList(11, testInput);
        }

        private class DeviceNode
        {
            public DeviceNode(string name)
            {
                ResultPaths = [];
                Name = name;
            }

            public string Name;
            public Dictionary<string, Int64> ResultPaths;
        }

        private Dictionary<string, List<string>> ParseInput(out List<DeviceNode> devices)
        {
            Dictionary<string, List<string>> ret = [];
            devices = [];
            foreach (string s in _input)
            {
                string[] subs = s.Split(" ");
                // Strip ':' from device name
                string key = subs[0][0..^1];
                ret.Add(key, [.. subs[1..]]);
                devices.Add(new(key));
            }
            return ret;
        }

        public string GetSolution1()
        {
            Dictionary<string, List<string>> deviceChains = ParseInput(out List<DeviceNode> devices);
            if (deviceChains.ContainsKey("you"))
                return $"Solution 1: {ExploreNode(deviceChains, devices, "you")}";
            else
                return $"Solution 1: No solution";
        }

        private Int64 ExploreNode(Dictionary<string, List<string>> links, List<DeviceNode> devices, string current, string target = "out")
        {
            if (current.Equals(target))
                return 1;
            else if (current.Equals("out"))
                return 0;

            DeviceNode currentNode = devices.Find(d => d.Name.Equals(current))!;
            if (currentNode.ResultPaths.ContainsKey(target))
                return currentNode.ResultPaths[target];
            else
            {
                currentNode.ResultPaths.Add(target, 0);
                foreach (string link in links[current])
                {
                    currentNode.ResultPaths[target] += ExploreNode(links, devices, link, target);
                }
                return currentNode.ResultPaths[target];
            }
        }

        public string GetSolution2()
        {
            Dictionary<string, List<string>> deviceChains = ParseInput(out List<DeviceNode> devices);
            if (deviceChains.ContainsKey("svr"))
            {
                Int64 fftToDac = ExploreNode(deviceChains, devices, "fft", "dac");
                Int64 dacToFft = ExploreNode(deviceChains, devices, "dac", "fft");

                // There are no loops, so at least one of fftToDac or dacToFft will be zero
                if (fftToDac != 0)
                {
                    Int64 dacToOut = ExploreNode(deviceChains, devices, "dac");
                    Int64 svrToOut = ExploreNode(deviceChains, devices, "svr", "fft") * fftToDac * dacToOut;
                    return $"Solution 2: fftToDac: {fftToDac}, dacToOut: {dacToOut}, serverToOut: {svrToOut}";
                }
                else if (dacToFft != 0)
                {
                    Int64 fftToOut = ExploreNode(deviceChains, devices, "fft");
                    Int64 svrToOut = ExploreNode(deviceChains, devices, "svr", "dac") * dacToFft * fftToOut;
                    return $"Solution 2: dacToFft: {dacToFft}, fftToOut: {fftToOut}, serverToOut: {svrToOut}";
                }
            }
            return $"Solution 2: No solution";
        }
    }
}
