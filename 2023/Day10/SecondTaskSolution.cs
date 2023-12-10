namespace AdventOfCode2023.Day10;

public static class SecondTaskSolution
{
    private static readonly (int, int)[] Offsets = new[]
    {
        (-1, 0),
        (1, 0),
        (0, -1),
        (0, 1),
    };

    public static int Initial(string[] input)
    {
        var matrix = GetMatrix(input);
        var visited = matrix.Select(r => new bool[r.Length]).ToArray();
        var rows = matrix.Length;
        var cols = matrix[0].Length;
        var startRow = 0;
        var startCol = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (matrix[i][j] == 'S')
                {
                    startRow = i;
                    startCol = j;
                }
            }
        }

        var queue = new Queue<(int Row, int Col, int Length)>();
        queue.Enqueue((startRow, startCol, 0));
        while (queue.Count > 0)
        {
            var (row, col, length) = queue.Dequeue();
            if (visited[row][col])
            {
                break;
            }

            visited[row][col] = true;
            foreach (var (dx, dy) in Offsets)
            {
                if (CanGo(matrix, visited, row, col, dx, dy))
                {
                    queue.Enqueue((row + dy, col + dx, length + 1));
                }
            }
        }

        queue.Clear();
        queue.Enqueue((0, 0, 0));
        visited[0][0] = true;
        while (queue.Count > 0)
        {
            var (row, col, _) = queue.Dequeue();
            foreach (var (dx, dy) in Offsets)
            {
                var toRow = row + dy;
                var toCol = col + dx;
                if (toRow >= 0 && toCol >= 0 && toRow < rows && toCol < cols && !visited[toRow][toCol])
                {
                    queue.Enqueue((toRow, toCol, 0));
                    visited[toRow][toCol] = true;
                }
            }
        }

        var enclosedCount = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i % 2 == 1 && j % 2 == 1 && !visited[i][j])
                {
                    enclosedCount++;
                }
            }
        }

        return enclosedCount;
    }

    private static char[][] GetMatrix(string[] input)
    {
        var matrix = new char[(input.Length * 2) + 1][];
        matrix[0] = new char[(input[0].Length * 2) + 1];
        Array.Fill(matrix[0], '.');
        matrix[^1] = matrix[0].ToArray();
        for (int i = 1; i < matrix.Length - 1; i += 2)
        {
            matrix[i] = new char[matrix[0].Length];
            matrix[i][0] = '.';
            matrix[i][^1] = '.';
            for (int j = 1; j < matrix[0].Length - 1; j++)
            {
                if (j % 2 == 0)
                {
                    matrix[i][j] = GetHorizontalConnector(input[i / 2][(j / 2) - 1], input[i / 2][j / 2]);
                }
                else
                {
                    matrix[i][j] = input[i / 2][j / 2];
                }
            }
        }

        for (int i = 2; i < matrix.Length - 2; i += 2)
        {
            matrix[i] = new char[matrix[0].Length];
            matrix[i][0] = '.';
            matrix[i][^1] = '.';
            for (int j = 0; j < matrix[0].Length; j++)
            {
                matrix[i][j] = GetVerticalConnector(matrix[i - 1][j], matrix[i + 1][j]);
            }
        }

        return matrix;
    }

    private static char GetVerticalConnector(char top, char bottom)
    {
        return top is '|' or 'S' or '7' or 'F' && bottom is '|' or 'S' or 'J' or 'L' ? '|' : '.';
    }

    private static char GetHorizontalConnector(char left, char right)
    {
        return left is 'F' or 'L' or '-' or 'S' && right is 'J' or '7' or '-' or 'S' ? '-' : '.';
    }

    private static bool CanGo(char[][] matrix, bool[][] visited,
                              int fromRow, int fromCol, int dx, int dy)
    {
        var toRow = fromRow + dy;
        var toCol = fromCol + dx;
        if (toRow < 0 || toCol < 0 ||
            toRow >= matrix.Length ||
            toCol >= matrix[0].Length ||
            visited[toRow][toCol])
        {
            return false;
        }

        return CanGoFrom(matrix[fromRow][fromCol], dx, dy) &&
               CanGoTo(matrix[toRow][toCol], dx, dy);
    }

    private static bool CanGoFrom(char pipe, int dx, int dy)
    {
        return ((dx, dy), pipe) switch
        {
            (_, 'S') => true,
            ((-1 or 1, 0), '-') => true,
            ((0, -1 or 1), '|') => true,
            ((0, 1) or (1, 0), 'F') => true,
            ((0, 1) or (-1, 0), '7') => true,
            ((0, -1) or (1, 0), 'L') => true,
            ((0, -1) or (-1, 0), 'J') => true,
            _ => false,
        };
    }

    private static bool CanGoTo(char pipe, int dx, int dy)
    {
        return ((dx, dy), pipe) switch
        {
            ((-1 or 1, 0), '-') => true,
            ((0, -1 or 1), '|') => true,
            ((0, 1) or (1, 0), 'J') => true,
            ((0, 1) or (-1, 0), 'L') => true,
            ((0, -1) or (1, 0), '7') => true,
            ((0, -1) or (-1, 0), 'F') => true,
            _ => false,
        };
    }
}