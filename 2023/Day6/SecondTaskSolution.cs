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