using System.Drawing;
using System.Globalization;

namespace AdventOfCode2023.Day18;

public static class FirstTaskSolution
{
    private static readonly (int, int)[] Offsets = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

    public static int Initial(string[] input)
    {
        var instructions = input
            .Select(l =>
            {
                var parts = l.Split(' ');
                return (parts[0][0], int.Parse(parts[1], CultureInfo.InvariantCulture));
            })
            .ToArray();
        var (topLeft, bottomRight) = GetBounds(instructions);
        var width = bottomRight.X - topLeft.X + 1;
        var height = bottomRight.Y - topLeft.Y + 1;
        var map = new byte[height + 2, width + 2];
        DigOutExterior(map, -topLeft.X + 1, -topLeft.Y + 1, instructions);
        DigOutInterior(map);
        var rows = map.GetLength(0);
        var cols = map.GetLength(1);
        var count = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] < 2)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static void DigOutInterior(byte[,] map)
    {
        var rows = map.GetLength(0);
        var cols = map.GetLength(1);
        var queue = new Queue<Point>();
        map[0, 0] = 2;
        queue.Enqueue(new Point(0, 0));
        while (queue.Count > 0)
        {
            var point = queue.Dequeue();
            var x = point.X;
            var y = point.Y;
            foreach (var (dx, dy) in Offsets)
            {
                var newX = x + dx;
                var newY = y + dy;
                if (newX >= 0 && newX < cols && newY >= 0 && newY < rows && map[newY, newX] == 0)
                {
                    map[newY, newX] = 2;
                    queue.Enqueue(new Point(newX, newY));
                }
            }
        }
    }

    private static void DigOutExterior(byte[,] map, int startX, int startY, (char, int)[] instructions)
    {
        foreach (var (direction, count) in instructions)
        {
            if (direction == 'U')
            {
                for (int i = 0; i < count; i++)
                {
                    map[startY--, startX] = 1;
                }
            }
            else if (direction == 'D')
            {
                for (int i = 0; i < count; i++)
                {
                    map[startY++, startX] = 1;
                }
            }
            else if (direction == 'R')
            {
                for (int i = 0; i < count; i++)
                {
                    map[startY, startX++] = 1;
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    map[startY, startX--] = 1;
                }
            }
        }
    }

    private static (Point TopLeft, Point BottomRight) GetBounds((char, int)[] instructions)
    {
        var minX = 0;
        var minY = 0;
        var maxX = 0;
        var maxY = 0;
        var x = 0;
        var y = 0;
        foreach (var (direction, count) in instructions)
        {
            if (direction == 'U')
            {
                y -= count;
                minY = Math.Min(minY, y);
            }
            else if (direction == 'D')
            {
                y += count;
                maxY = Math.Max(maxY, y);
            }
            else if (direction == 'L')
            {
                x -= count;
                minX = Math.Min(minX, x);
            }
            else if (direction == 'R')
            {
                x += count;
                maxX = Math.Max(maxX, x);
            }
        }

        return (new Point(minX, minY), new Point(maxX, maxY));
    }
}