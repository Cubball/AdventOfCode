using System.Globalization;

namespace AdventOfCode2023.Day24;

public struct Point
{
    public long X { get; set; }

    public long Y { get; set; }

    public long Z { get; set; }
}

public static class FirstTaskSolution
{
    private static readonly char[] Separators = new char[] { ' ', ',' };

    public static int Initial(string[] input)
    {
        var points = new List<(Point Position, Point Velocity)>();
        foreach (var line in input)
        {
            var parts = line.Split('@');
            var positionParts = parts[0].Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            var position = new Point
            {
                X = long.Parse(positionParts[0], CultureInfo.InvariantCulture),
                Y = long.Parse(positionParts[1], CultureInfo.InvariantCulture),
                Z = long.Parse(positionParts[2], CultureInfo.InvariantCulture),
            };
            var velocityParts = parts[1].Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            var velocity = new Point
            {
                X = long.Parse(velocityParts[0], CultureInfo.InvariantCulture),
                Y = long.Parse(velocityParts[1], CultureInfo.InvariantCulture),
                Z = long.Parse(velocityParts[2], CultureInfo.InvariantCulture),
            };
            points.Add((position, velocity));
        }

        const long lowerBound = 200000000000000;
        const long upperBound = 400000000000000;
        var count = 0;
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                var (position1, velocity1) = points[i];
                var (position2, velocity2) = points[j];
                var k1 = (decimal)velocity1.Y / velocity1.X;
                var b1 = position1.Y - (k1 * position1.X);
                var k2 = (decimal)velocity2.Y / velocity2.X;
                var b2 = position2.Y - (k2 * position2.X);
                if (k1 == k2)
                {
                    continue;
                }

                var x = (b2 - b1) / (k1 - k2);
                var y = (k1 * x) + b1;
                var dx1 = x - position1.X;
                var dy1 = y - position1.Y;
                var dx2 = x - position2.X;
                var dy2 = y - position2.Y;
                if (x >= lowerBound && x <= upperBound && y >= lowerBound && y <= upperBound &&
                    dx1 * velocity1.X >= 0 && dy1 * velocity1.Y >= 0 &&
                    dx2 * velocity2.X >= 0 && dy2 * velocity2.Y >= 0)
                {
                    count++;
                }
            }
        }

        return count;
    }
}