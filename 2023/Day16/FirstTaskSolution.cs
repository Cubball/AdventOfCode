namespace AdventOfCode2023.Day16;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var visited = input.Select(l => new List<(int, int)>[l.Length]).ToArray();
        Recurse(input, visited, 0, -1, 0, 0);
        var count = 0;
        foreach (var array in visited)
        {
            foreach (var item in array)
            {
                if (item is not null)
                {
                    count++;
                }
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