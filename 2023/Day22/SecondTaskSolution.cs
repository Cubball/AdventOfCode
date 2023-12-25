namespace AdventOfCode2023.Day22;

public static class SecondTaskSolution
{
    public static int Initial(string[] input)
    {
        var bricks = new List<Brick>();
        foreach (var line in input)
        {
            var span = line.AsSpan();
            var tildeIndex = span.IndexOf('~');
            var firstPoint = Point.Parse(span[..tildeIndex]);
            var secondPoint = Point.Parse(span[(tildeIndex + 1)..]);
            bricks.Add(new Brick { First = firstPoint, Second = secondPoint });
        }

        bricks.Sort((a, b) => a.BottomZ.CompareTo(b.BottomZ));
        var heights = new Dictionary<(int, int), int>();
        for (int i = 0; i < bricks.Count; i++)
        {
            var points = bricks[i].GetPoints();
            var lowestZ = 0;
            foreach (var point in points)
            {
                lowestZ = Math.Max(lowestZ, heights.GetValueOrDefault((point.X, point.Y)));
            }

            bricks[i] = bricks[i].FallTo(lowestZ + 1);
            points = bricks[i].GetPoints();
            foreach (var point in points)
            {
                if (!heights.TryGetValue((point.X, point.Y), out var value) || value < point.Z)
                {
                    heights[(point.X, point.Y)] = point.Z;
                }
            }
        }

        var supporting = new List<int>[bricks.Count];
        var supportsCounts = new int[bricks.Count];
        for (int i = 0; i < bricks.Count; i++)
        {
            supporting[i] = new List<int>();
            var currentBrick = bricks[i];
            var currentBrickPoints = currentBrick.GetPoints();
            for (int j = 0; j < bricks.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                var nextBrick = bricks[j];
                var nextBrickPoints = nextBrick.GetPoints();
                if (currentBrickPoints.Any(p1 => nextBrickPoints.Any(p2 => p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z - 1)))
                {
                    supporting[i].Add(j);
                    supportsCounts[j]++;
                }
            }
        }

        var totalCount = 0;
        for (int i = 0; i < bricks.Count; i++)
        {
            var supportsCountsCopy = supportsCounts.ToArray();
            var queue = new Queue<int>();
            queue.Enqueue(i);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var brick in supporting[current])
                {
                    if (--supportsCountsCopy[brick] == 0 && bricks[brick].BottomZ > 1)
                    {
                        queue.Enqueue(brick);
                        totalCount++;
                    }
                }
            }
        }

        return totalCount;
    }
}