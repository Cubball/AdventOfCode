using System.Globalization;

namespace AdventOfCode2023.Day6;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var entries = input[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(int.Parse)
            .Zip(input[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(int.Parse));
        var product = 1;
        foreach (var (time, distance) in entries)
        {
            product *= GetNumberOfWaysToWin(time, distance);
        }

        return product;
    }

    public static int LINQ(string[] input) =>
        input[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(int.Parse)
            .Zip(input[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .Select(int.Parse))
            .Aggregate(1, (p, t) => p *= GetNumberOfWaysToWin(t.First, t.Second));

    public static int UsingSpans(string[] input)
    {
        var races = new List<(int Time, int Distance)>();
        var times = input[0].AsSpan();
        var distances = input[1].AsSpan();
        while (true)
        {
            var timeStartIndex = times.IndexOfAnyInRange('0', '9');
            if (timeStartIndex == -1)
            {
                break;
            }

            times = times[timeStartIndex..];
            var distanceStartIndex = distances.IndexOfAnyInRange('0', '9');
            distances = distances[distanceStartIndex..];
            var timeEndIndex = times.IndexOfAnyExceptInRange('0', '9');
            if (timeEndIndex == -1)
            {
                timeEndIndex = times.Length;
            }

            var distanceEndIndex = distances.IndexOfAnyExceptInRange('0', '9');
            if (distanceEndIndex == -1)
            {
                distanceEndIndex = distances.Length;
            }

            races.Add((int.Parse(times[..timeEndIndex], CultureInfo.InvariantCulture), int.Parse(distances[..distanceEndIndex], CultureInfo.InvariantCulture)));
            times = times[timeEndIndex..];
            distances = distances[distanceEndIndex..];
        }

        var product = 1;
        foreach (var (time, distance) in races)
        {
            product *= GetNumberOfWaysToWin(time, distance);
        }

        return product;
    }

    private static int GetNumberOfWaysToWin(int time, int distance)
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

        var rangeStart = (int)x1Rounded;
        var rangeEnd = (int)x2Rounded;
        return rangeEnd - rangeStart + 1;
    }
}