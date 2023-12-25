namespace AdventOfCode2023.Day23;

public static class FirstTaskSolution
{
    private static int s_maxHikeLength;

    public static int Initial(string[] input)
    {
        var visited = new HashSet<(int, int)>();
        FindMaxHikeLength(input, 0, 1, visited);
        return s_maxHikeLength;
    }

    private static void FindMaxHikeLength(string[] map, int row, int col, HashSet<(int, int)> visited)
    {
        if (row < 0 || col < 0 || row >= map.Length || col >= map[0].Length || map[row][col] == '#' || visited.Contains((row, col)))
        {
            return;
        }

        if (row == map.Length - 1 && col == map[0].Length - 2)
        {
            s_maxHikeLength = Math.Max(s_maxHikeLength, visited.Count);
            return;
        }

        visited.Add((row, col));
        if (map[row][col] is '.' or '>')
        {
            FindMaxHikeLength(map, row, col + 1, visited);
        }

        if (map[row][col] is '.' or '<')
        {
            FindMaxHikeLength(map, row, col - 1, visited);
        }

        if (map[row][col] is '.' or 'v')
        {
            FindMaxHikeLength(map, row + 1, col, visited);
        }

        if (map[row][col] is '.' or '^')
        {
            FindMaxHikeLength(map, row - 1, col, visited);
        }

        visited.Remove((row, col));
    }
}