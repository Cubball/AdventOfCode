namespace AdventOfCode2023.Day13;

public static class SecondTaskSolution
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
            if (CanMirrorBeBetweenRows(pattern, i, i + 1))
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
            if (CanMirrorBeBetweenCols(pattern, i, i + 1))
            {
                return i + 1;
            }
        }

        return 0;
    }

    private static bool CanMirrorBeBetweenRows(Span<string> pattern, int row1, int row2)
    {
        var foundMismatch = false;
        for (int i = 0; i < pattern[0].Length; i++)
        {
            if (GetNumberOfMismatchesBetweenRows(pattern, row1, row2, i) == 1)
            {
                if (foundMismatch)
                {
                    return false;
                }

                foundMismatch = true;
            }

            if (GetNumberOfMismatchesBetweenRows(pattern, row1, row2, i) > 1)
            {
                return false;
            }
        }

        return foundMismatch;
    }

    private static bool CanMirrorBeBetweenCols(Span<string> pattern, int col1, int col2)
    {
        var foundMismatch = false;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (GetNumberOfMismatchesBetweenCols(pattern, col1, col2, i) == 1)
            {
                if (foundMismatch)
                {
                    return false;
                }

                foundMismatch = true;
            }

            if (GetNumberOfMismatchesBetweenCols(pattern, col1, col2, i) > 1)
            {
                return false;
            }
        }

        return foundMismatch;
    }

    private static int GetNumberOfMismatchesBetweenRows(Span<string> pattern, int row1, int row2, int column)
    {
        var count = Math.Min(row1 + 1, pattern.Length - row2);
        var mismatches = 0;
        for (int i = 0; i < count; i++)
        {
            if (pattern[row1--][column] != pattern[row2++][column])
            {
                mismatches++;
            }
        }

        return mismatches;
    }

    private static int GetNumberOfMismatchesBetweenCols(Span<string> pattern, int col1, int col2, int row)
    {
        var count = Math.Min(col1 + 1, pattern[0].Length - col2);
        var mismatches = 0;
        for (int i = 0; i < count; i++)
        {
            if (pattern[row][col1--] != pattern[row][col2++])
            {
                mismatches++;
            }
        }

        return mismatches;
    }
}