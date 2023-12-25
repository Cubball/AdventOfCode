namespace AdventOfCode2023.Day23;

public static class SecondTaskSolution
{
    private static int s_maxHikeLength;

    public static int Initial(string[] input)
    {
        var visited = new bool[input.Length, input[0].Length];
        FindMaxHikeLength(input, 0, 1, visited);
        return s_maxHikeLength;
    }

    private static void FindMaxHikeLength(string[] map, int row, int col, bool[,] visited)
    {
        if (row == map.Length - 1 && col == map[0].Length - 2)
        {
            s_maxHikeLength = Math.Max(s_maxHikeLength, GetVisitedCount(visited));
            return;
        }

        visited[row, col] = true;
        if (col + 1 < map[0].Length && map[row][col + 1] != '#' && !visited[row, col + 1])
        {
            FindMaxHikeLength(map, row, col + 1, visited);
        }

        if (col - 1 >= 0 && map[row][col - 1] != '#' && !visited[row, col - 1])
        {
            FindMaxHikeLength(map, row, col - 1, visited);
        }

        if (row + 1 < map.Length && map[row + 1][col] != '#' && !visited[row + 1, col])
        {
            FindMaxHikeLength(map, row + 1, col, visited);
        }

        if (row - 1 >= 0 && map[row - 1][col] != '#' && !visited[row - 1, col])
        {
            FindMaxHikeLength(map, row - 1, col, visited);
        }

        visited[row, col] = false;
    }

    private static int GetVisitedCount(bool[,] visited)
    {
        var count = 0;
        var rows = visited.GetLength(0);
        var cols = visited.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (visited[i, j])
                {
                    count++;
                }
            }
        }

        return count;
    }
}