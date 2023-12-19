using System.Drawing;
using System.Globalization;

namespace AdventOfCode2023.Day18;

public static class SecondTaskSolution
{
    public static long Initial(string[] input)
    {
        var instructions = input
            .Select(l =>
            {
                var parts = l.Split(' ');
                return (parts[2][^2] - '0', int.Parse(parts[2][2..^2], NumberStyles.HexNumber, CultureInfo.InvariantCulture));
            })
            .ToArray();
        var area = 0L;
        var corners = GetCorners(instructions);
        corners.Add(corners[0]);
        for (int i = 0; i < corners.Count - 1; i++)
        {
            var (x1, y1) = ((long)corners[i].X, corners[i].Y);
            var (x2, y2) = ((long)corners[i + 1].X, corners[i + 1].Y);
            area += (x1 * y2) - (x2 * y1);
        }

        area = Math.Abs(area);
        var perimeter = 0L;
        foreach (var (_, count) in instructions)
        {
            perimeter += count;
        }

        return ((area + perimeter) / 2) + 1;
    }

    private static List<Point> GetCorners((int, int)[] instructions)
    {
        var list = new List<Point>();
        var current = new Point(0, 0);
        foreach (var (direction, count) in instructions)
        {
            list.Add(current);
            var (dx, dy) = direction switch
            {
                0 => (count, 0),
                1 => (0, -count),
                2 => (-count, 0),
                3 => (0, count),
                _ => throw new ArgumentOutOfRangeException(nameof(instructions)),
            };
            current = new Point(current.X + dx, current.Y + dy);
        }

        return list;
    }
}