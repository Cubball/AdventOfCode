using System.Drawing;

namespace AdventOfCode2023.Day17;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var distances = new Dictionary<(Point Point, Direction Direction, int Length), int>
        {
            [(new Point(0, 1), Direction.Down, 1)] = input[1][0] - '0',
            [(new Point(1, 0), Direction.Right, 1)] = input[0][1] - '0',
        };
        var visited = new HashSet<(Point, Direction, int)>();
        var minHeatLoss = int.MaxValue;
        while (distances.Count > 0)
        {
            var ((current, direction, length), distance) = distances.MinBy(p => p.Value);
            if (current.X == input[0].Length - 1 && current.Y == input.Length - 1)
            {
                minHeatLoss = Math.Min(minHeatLoss, distance);
            }

            visited.Add((current, direction, length));
            TryGoInDirection(input, distances, visited, current, distance, direction, length + 1);
            var (dir1, dir2) = direction.GetPerpendicularDirections();
            TryGoInDirection(input, distances, visited, current, distance, dir1, 1);
            TryGoInDirection(input, distances, visited, current, distance, dir2, 1);
            distances.Remove((current, direction, length));
        }

        return minHeatLoss;
    }

    private static void TryGoInDirection(
            string[] input,
            Dictionary<(Point Point, Direction Direction, int Length), int> distances,
            HashSet<(Point, Direction, int)> visited,
            Point current, int distance, Direction direction, int length)
    {
        var (dx, dy) = direction.GetOffsets();
        var next = new Point(current.X + dx, current.Y + dy);
        if (next.X < 0 || next.Y < 0 ||
            next.X >= input[0].Length || next.Y >= input.Length ||
            length > 3 ||
            visited.Contains((next, direction, length)))
        {
            return;
        }

        var newDistance = distance + input[next.Y][next.X] - '0';
        if (!distances.TryGetValue((next, direction, length), out var value) || value > newDistance)
        {
            distances[(next, direction, length)] = newDistance;
        }
    }

    private static (int, int) GetOffsets(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => (0, -1),
            Direction.Down => (0, 1),
            Direction.Left => (-1, 0),
            Direction.Right => (1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private static (Direction, Direction) GetPerpendicularDirections(this Direction direction)
    {
        return direction switch
        {
            Direction.Up or Direction.Down => (Direction.Left, Direction.Right),
            Direction.Left or Direction.Right => (Direction.Down, Direction.Up),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}