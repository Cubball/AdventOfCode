namespace AdventOfCode2023.Day21;

public static class SecondTaskSolution
{
    private static readonly (int, int)[] Offsets = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

    public static long Initial(string[] input)
    {
        int startRow = 0;
        int startCol = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                if (input[i][j] == 'S')
                {
                    startRow = i;
                    startCol = j;
                }
            }
        }

        var queue = new Queue<(int, int)>();
        var evenCountCorners = 0;
        var oddCountCorners = 0;
        var evenCount = 0;
        var oddCount = 0;
        queue.Enqueue((startRow, startCol));
        var spots = new HashSet<(int, int)>();
        var steps = 130;
        for (int i = 0; i < steps; i++)
        {
            var count = queue.Count;
            spots.Clear();
            for (int j = 0; j < count; j++)
            {
                var (row, col) = queue.Dequeue();
                foreach (var (dx, dy) in Offsets)
                {
                    var nextRow = row + dy;
                    var nextCol = col + dx;
                    if (nextRow >= 0 && nextRow < input.Length &&
                        nextCol >= 0 && nextCol < input[0].Length &&
                        input[nextRow][nextCol] != '#' &&
                        spots.Add((nextRow, nextCol)))
                    {
                        queue.Enqueue((nextRow, nextCol));
                    }
                }
            }

            if (i == 128)
            {
                oddCount = spots.Count;
                oddCountCorners = spots
                    .Where(p =>
                    {
                        var dx = Math.Abs(p.Item1 - startCol);
                        var dy = Math.Abs(p.Item2 - startRow);
                        return (dx + dy) > 65;
                    })
                    .Count();
            }
            else if (i == 129)
            {
                evenCount = spots.Count;
                evenCountCorners = spots
                    .Where(p =>
                    {
                        var dx = Math.Abs(p.Item1 - startCol);
                        var dy = Math.Abs(p.Item2 - startRow);
                        return (dx + dy) > 65;
                    })
                    .Count();
            }
        }

        var n = 202300L;
        return (oddCount * ((n * n) + (2 * n) + 1)) + (evenCount * (n * n)) + (n * evenCountCorners) - ((n + 1) * oddCountCorners);
    }
}