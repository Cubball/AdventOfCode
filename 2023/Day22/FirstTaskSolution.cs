using System.Globalization;

namespace AdventOfCode2023.Day22;

public struct Point
{
    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public static Point Parse(ReadOnlySpan<char> span)
    {
        Span<Range> ranges = stackalloc Range[3];
        span.Split(ranges, ',');
        return new Point
        {
            X = int.Parse(span[ranges[0]], CultureInfo.InvariantCulture),
            Y = int.Parse(span[ranges[1]], CultureInfo.InvariantCulture),
            Z = int.Parse(span[ranges[2]], CultureInfo.InvariantCulture),
        };
    }
}

public struct Brick
{
    private List<Point>? _points;

    public Point First { get; set; }

    public Point Second { get; set; }

    public readonly int BottomZ => Math.Min(First.Z, Second.Z);

    public readonly int TopZ => Math.Max(First.Z, Second.Z);

    public List<Point> GetPoints()
    {
        if (_points is not null)
        {
            return _points;
        }

        var list = new List<Point>();
        var dx = Math.Sign(Second.X - First.X);
        var dy = Math.Sign(Second.Y - First.Y);
        var dz = Math.Sign(Second.Z - First.Z);
        var point = First;
        while (point.X != Second.X || point.Y != Second.Y || point.Z != Second.Z)
        {
            list.Add(point);
            point = new Point
            {
                X = point.X + dx,
                Y = point.Y + dy,
                Z = point.Z + dz
            };
        }

        list.Add(point);
        return _points = list;
    }

    public readonly Brick FallTo(int zValue)
    {
        Point first;
        Point second;
        if (First.Z == Second.Z)
        {
            first = new Point { X = First.X, Y = First.Y, Z = zValue };
            second = new Point { X = Second.X, Y = Second.Y, Z = zValue };
        }
        else if (First.Z < Second.Z)
        {
            var dz = Second.Z - First.Z;
            first = new Point { X = First.X, Y = First.Y, Z = zValue };
            second = new Point { X = Second.X, Y = Second.Y, Z = zValue + dz };
        }
        else
        {
            var dz = First.Z - Second.Z;
            second = new Point { X = Second.X, Y = Second.Y, Z = zValue };
            first = new Point { X = First.X, Y = First.Y, Z = zValue + dz };
        }

        return new Brick { First = first, Second = second };
    }
}

public static class FirstTaskSolution
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

        var supports = new List<int>[bricks.Count];
        for (int i = 0; i < bricks.Count; i++)
        {
            supports[i] = new List<int>();
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
                if (currentBrickPoints.Any(p1 => nextBrickPoints.Any(p2 => p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z + 1)))
                {
                    supports[i].Add(j);
                }
            }
        }

        var cannotBeRemoved = new bool[bricks.Count];
        for (int i = 0; i < supports.Length; i++)
        {
            if (supports[i].Count == 1)
            {
                cannotBeRemoved[supports[i][0]] = true;
            }
        }

        return cannotBeRemoved.Count(x => !x);
    }
}