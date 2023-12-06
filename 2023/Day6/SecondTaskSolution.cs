using System.Globalization;

namespace AdventOfCode2023.Day6;

public static class SecondTaskSolution
{
    public static long Initial(string[] input)
    {
        var time = long.Parse(string.Concat(input[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)), CultureInfo.InvariantCulture);
        var distance = long.Parse(string.Concat(input[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)), CultureInfo.InvariantCulture);
        return GetNumberOfWaysToWin(time, distance);
    }

    public static long ReplaceInsteadOfSplit(string[] input)
    {
        var time = long.Parse(input[0][(input[0].IndexOf(':') + 1)..].Replace(" ", ""), CultureInfo.InvariantCulture);
        var distance = long.Parse(input[1][(input[1].IndexOf(':') + 1)..].Replace(" ", ""), CultureInfo.InvariantCulture);
        return GetNumberOfWaysToWin(time, distance);
    }

    public static long UsingSpans(string[] input)
    {
        var time = GetNumberFromSpan(input[0]);
        var distance = GetNumberFromSpan(input[1]);
        return GetNumberOfWaysToWin(time, distance);
    }

    private static long GetNumberFromSpan(ReadOnlySpan<char> line)
    {
        var number = 0L;
        foreach (var character in line)
        {
            if (!char.IsDigit(character))
            {
                continue;
            }

            var digit = character - '0';
            number *= 10;
            number += digit;
        }

        return number;
    }

    private static long GetNumberOfWaysToWin(long time, long distance)
    {
        var discriminantRoot = Math.Sqrt((time * time) - (4 * distance));
        var x1 = (time - discriminantRoot) / 2;
        var x2 = (time + discriminantRoot) / 2;
        var x1Rounded = Math.Ceiling(x1);
        var x2Rounded = Math.Floor(x2);
        if (x1 == x1Rounded)
        {
            x1Rounded++;
        }

        if (x2 == x2Rounded)
        {
            x2Rounded--;
        }

        var rangeStart = (long)x1Rounded;
        var rangeEnd = (long)x2Rounded;
        return rangeEnd - rangeStart + 1;
    }
}