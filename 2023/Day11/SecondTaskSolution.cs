namespace AdventOfCode2023.Day11;

public static class SecondTaskSolution
{
    public static long Initial(string[] input)
    {
        var emptyRows = GetEmptyRows(input);
        var emptyCols = GetEmptyColumns(input);
        var galaxies = GetGalaxies(input);
        var multiplier = 1_000_000 - 1;
        var sum = 0L;
        while (galaxies.Count > 1)
        {
            var (row, col) = galaxies[^1];
            for (int i = 0; i < galaxies.Count - 1; i++)
            {
                var (nextRow, nextCol) = galaxies[i];
                var row1 = Math.Min(row, nextRow);
                var row2 = Math.Max(row, nextRow);
                var col1 = Math.Min(col, nextCol);
                var col2 = Math.Max(col, nextCol);
                var numberOfEmptyRows = GetNumberOfEmptyLinesBetween(row1, row2, emptyRows);
                var numberOfEmptyCols = GetNumberOfEmptyLinesBetween(col1, col2, emptyCols);
                sum += row2 - row1 + (numberOfEmptyRows * multiplier) + col2 - col1 + (numberOfEmptyCols * multiplier);
            }

            galaxies.RemoveAt(galaxies.Count - 1);
        }

        return sum;
    }

    private static int GetNumberOfEmptyLinesBetween(int line1, int line2, HashSet<int> emptyLines)
    {
        var count = 0;
        for (int i = line1 + 1; i < line2; i++)
        {
            if (emptyLines.Contains(i))
            {
                count++;
            }
        }

        return count;
    }

    private static HashSet<int> GetEmptyRows(string[] input)
    {
        var set = new HashSet<int>();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].AsSpan().IndexOf('#') == -1)
            {
                set.Add(i);
            }
        }

        return set;
    }

    private static List<(int Row, int Col)> GetGalaxies(string[] image)
    {
        var set = new List<(int, int)>();
        for (int i = 0; i < image.Length; i++)
        {
            for (int j = 0; j < image[0].Length; j++)
            {
                if (image[i][j] == '#')
                {
                    set.Add((i, j));
                }
            }
        }

        return set;
    }

    private static HashSet<int> GetEmptyColumns(string[] input)
    {
        var set = new HashSet<int>();
        for (int i = 0; i < input[0].Length; i++)
        {
            var isEmpty = true;
            for (int j = 0; j < input.Length; j++)
            {
                if (input[j][i] == '#')
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                set.Add(i);
            }
        }

        return set;
    }
}