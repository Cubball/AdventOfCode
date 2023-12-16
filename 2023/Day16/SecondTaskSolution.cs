namespace AdventOfCode2023.Day16;

public static class SecondTaskSolution
{
    public static int Initial(string[] input)
    {
        var visited = input.Select(l => new List<(int, int)>[l.Length]).ToArray();
        var count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            Recurse(input, visited, i, -1, i, 0);
            count = Math.Max(count, GetNumberOfEnergizedAndClear(visited));
            Recurse(input, visited, i, input[0].Length, i, input[0].Length - 1);
            count = Math.Max(count, GetNumberOfEnergizedAndClear(visited));
        }

        for (int i = 0; i < input[0].Length; i++)
        {
            Recurse(input, visited, -1, i, 0, i);
            count = Math.Max(count, GetNumberOfEnergizedAndClear(visited));
            Recurse(input, visited, input.Length, i, input.Length - 1, i);
            count = Math.Max(count, GetNumberOfEnergizedAndClear(visited));
        }

        return count;
    }

    private static int GetNumberOfEnergizedAndClear(List<(int, int)>[][] visited)
    {
        var count = 0;
        for (int i = 0; i < visited.Length; i++)
        {
            for (int j = 0; j < visited[0].Length; j++)
            {
                if (visited[i][j] is not null)
                {
                    count++;
                }

                visited[i][j] = null!;
            }
        }

        return count;
    }

    private static void Recurse(string[] input, List<(int, int)>[][] visited, int fromRow, int fromCol, int toRow, int toCol)
    {
        if (toRow < 0 || toCol < 0 || toRow >= input.Length || toCol >= input[0].Length)
        {
            return;
        }

        var dx = toCol - fromCol;
        var dy = toRow - fromRow;
        if (visited[toRow][toCol] is null)
        {
            visited[toRow][toCol] = new List<(int, int)>();
        }
        else if (visited[toRow][toCol].Contains((dx, dy)))
        {
            return;
        }

        visited[toRow][toCol].Add((dx, dy));
        if (input[toRow][toCol] == '.')
        {
            Recurse(input, visited, toRow, toCol, toRow + dy, toCol + dx);
            return;
        }

        if (dx == 0)
        {
            if (input[toRow][toCol] == '|')
            {
                Recurse(input, visited, toRow, toCol, toRow + dy, toCol);
            }
            else if (input[toRow][toCol] == '-')
            {
                Recurse(input, visited, toRow, toCol, toRow, toCol - 1);
                Recurse(input, visited, toRow, toCol, toRow, toCol + 1);
            }
            else if (input[toRow][toCol] == '/')
            {
                if (dy == 1)
                {
                    Recurse(input, visited, toRow, toCol, toRow, toCol - 1);
                }
                else
                {
                    Recurse(input, visited, toRow, toCol, toRow, toCol + 1);
                }
            }
            else
            {
                if (dy == 1)
                {
                    Recurse(input, visited, toRow, toCol, toRow, toCol + 1);
                }
                else
                {
                    Recurse(input, visited, toRow, toCol, toRow, toCol - 1);
                }
            }
        }
        else
        {
            if (input[toRow][toCol] == '-')
            {
                Recurse(input, visited, toRow, toCol, toRow, toCol + dx);
            }
            else if (input[toRow][toCol] == '|')
            {
                Recurse(input, visited, toRow, toCol, toRow - 1, toCol);
                Recurse(input, visited, toRow, toCol, toRow + 1, toCol);
            }
            else if (input[toRow][toCol] == '/')
            {
                if (dx == 1)
                {
                    Recurse(input, visited, toRow, toCol, toRow - 1, toCol);
                }
                else
                {
                    Recurse(input, visited, toRow, toCol, toRow + 1, toCol);
                }
            }
            else
            {
                if (dx == 1)
                {
                    Recurse(input, visited, toRow, toCol, toRow + 1, toCol);
                }
                else
                {
                    Recurse(input, visited, toRow, toCol, toRow - 1, toCol);
                }
            }
        }
    }
}