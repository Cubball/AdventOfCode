namespace AdventOfCode2023.Day13;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var start = 0;
        var sum = 0;
        for (int i = 0; i < input.Length + 1; i++)
        {
            if (i == input.Length || string.IsNullOrWhiteSpace(input[i]))
            {
                var span = input[start..i];
                sum += GetNumberOfColumns(span);
                sum += GetNumberOfRows(span) * 100;
                start = i + 1;
            }
        }

        return sum;
    }

    private static int GetNumberOfRows(Span<string> pattern)
    {
        for (int i = 0; i < pattern.Length - 1; i++)
        {
            if (IsMirrorBetweenRows(pattern, i, i + 1))
            {
                return i + 1;
            }
        }

        return 0;
    }

    private static int GetNumberOfColumns(Span<string> pattern)
    {
        for (int i = 0; i < pattern[0].Length - 1; i++)
        {
            if (IsMirrorBetweenCols(pattern, i, i + 1))
            {
                return i + 1;
            }
        }

        return 0;
    }

    private static bool IsMirrorBetweenRows(Span<string> pattern, int row1, int row2)
    {
        for (int i = 0; i < pattern[0].Length; i++)
        {
            if (!IsMirrorBetweenRows(pattern, row1, row2, i))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsMirrorBetweenCols(Span<string> pattern, int col1, int col2)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            if (!IsMirrorBetweenCols(pattern, col1, col2, i))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsMirrorBetweenRows(Span<string> pattern, int row1, int row2, int column)
    {
        var count = Math.Min(row1 + 1, pattern.Length - row2);
        for (int i = 0; i < count; i++)
        {
            if (pattern[row1--][column] != pattern[row2++][column])
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsMirrorBetweenCols(Span<string> pattern, int col1, int col2, int row)
    {
        var count = Math.Min(col1 + 1, pattern[0].Length - col2);
        for (int i = 0; i < count; i++)
        {
            if (pattern[row][col1--] != pattern[row][col2++])
            {
                return false;
            }
        }

        return true;
    }
}