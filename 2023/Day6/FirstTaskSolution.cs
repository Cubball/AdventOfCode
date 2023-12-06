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