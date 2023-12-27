namespace AdventOfCode2023.Day25;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var graph = new Dictionary<string, List<string>>();
        foreach (var line in input)
        {
            var parts = line.Split(':');
            var node = parts[0];
            var children = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            if (graph.TryGetValue(node, out var existingChildren))
            {
                existingChildren.AddRange(children);
            }
            else
            {
                graph[node] = children;
            }

            foreach (var child in children)
            {
                graph.TryAdd(child, new List<string>());
                if (!graph[child].Contains(node))
                {
                    graph[child].Add(node);
                }
            }
        }

        var graphCopy = graph.ToDictionary(x => x.Key, x => x.Value.ToList());
        var random = new Random();
        while (true)
        {
            while (graph.Count > 2)
            {
                var node1 = graph.Keys.ElementAt(random.Next(graph.Count));
                var node2 = graph[node1][random.Next(graph[node1].Count)];
                var newNode = $"{node1},{node2}";
                while (graph[node1].Remove(node2)) { }
                while (graph[node2].Remove(node1)) { }
                graph[newNode] = new List<string>();
                foreach (var node in graph[node1])
                {
                    while (graph[node].Remove(node1)) { }
                    graph[node].Add(newNode);
                    graph[newNode].Add(node);
                }

                foreach (var node in graph[node2])
                {
                    while (graph[node].Remove(node2)) { }
                    graph[node].Add(newNode);
                    graph[newNode].Add(node);
                }

                graph.Remove(node1);
                graph.Remove(node2);
            }

            var remainingNode = graph.First();
            if (remainingNode.Value.Count == 3)
            {
                var firstPartCount = remainingNode.Key.Count(c => c == ',') + 1;
                return firstPartCount * (graphCopy.Count - firstPartCount);
            }

            graph = graphCopy.ToDictionary(x => x.Key, x => x.Value.ToList());
        }
    }
}