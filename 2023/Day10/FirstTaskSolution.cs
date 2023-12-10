namespace AdventOfCode2023.Day10;

public static class FirstTaskSolution
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
        var matrix = input.Select(l => l.ToCharArray()).ToArray();
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
                return length;
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

        return 0;
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