using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day2;

public static partial class SecondStar
{
    private static readonly char[] Separators = new[] { ' ', ':', ';', ',' };

    public static int Naive(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var colonIndex = line.IndexOf(':');
            var sets = line[(colonIndex + 1)..].Split(';', StringSplitOptions.TrimEntries);
            var minRedCount = 0;
            var minGreenCount = 0;
            var minBlueCount = 0;
            foreach (var set in sets)
            {
                var cubesInfo = set.Split(',', StringSplitOptions.TrimEntries);
                foreach (var cubeInfo in cubesInfo)
                {
                    var spaceIndex = cubeInfo.IndexOf(' ');
                    var cubesCount = int.Parse(cubeInfo[..spaceIndex], CultureInfo.InvariantCulture);
                    var colorFirstChar = cubeInfo[spaceIndex + 1];
                    if (colorFirstChar == 'r')
                    {
                        if (minRedCount < cubesCount)
                        {
                            minRedCount = cubesCount;
                        }
                    }
                    else if (colorFirstChar == 'g')
                    {
                        if (minGreenCount < cubesCount)
                        {
                            minGreenCount = cubesCount;
                        }
                    }
                    else
                    {
                        if (minBlueCount < cubesCount)
                        {
                            minBlueCount = cubesCount;
                        }
                    }
                }
            }

            sum += minRedCount * minGreenCount * minBlueCount;
        }

        return sum;
    }

    public static int BetterApproach(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var parts = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            var minRedCount = 0;
            var minGreenCount = 0;
            var minBlueCount = 0;
            for (int i = 2; i < parts.Length - 1; i += 2)
            {
                var count = int.Parse(parts[i], CultureInfo.InvariantCulture);
                var colorFirstChar = parts[i + 1][0];
                if (colorFirstChar == 'r')
                {
                    if (minRedCount < count)
                    {
                        minRedCount = count;
                    }
                }
                else if (colorFirstChar == 'g')
                {
                    if (minGreenCount < count)
                    {
                        minGreenCount = count;
                    }
                }
                else
                {
                    if (minBlueCount < count)
                    {
                        minBlueCount = count;
                    }
                }
            }

            sum += minRedCount * minGreenCount * minBlueCount;
        }

        return sum;
    }

    public static int UsingSpans(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var minRedCount = 0;
            var minGreenCount = 0;
            var minBlueCount = 0;
            var lineAsSpan = line.AsSpan();
            var colonIndex = lineAsSpan.IndexOf(':');
            lineAsSpan = lineAsSpan[(colonIndex + 2)..];
            while (true)
            {
                var separatorIndex = lineAsSpan.IndexOfAny(",;");
                var spaceIndex = lineAsSpan.IndexOf(' ');
                var count = int.Parse(lineAsSpan[..spaceIndex], CultureInfo.InvariantCulture);
                var colorFirstChar = lineAsSpan[spaceIndex + 1];
                if (colorFirstChar == 'r')
                {
                    if (minRedCount < count)
                    {
                        minRedCount = count;
                    }
                }
                else if (colorFirstChar == 'g')
                {
                    if (minGreenCount < count)
                    {
                        minGreenCount = count;
                    }
                }
                else
                {
                    if (minBlueCount < count)
                    {
                        minBlueCount = count;
                    }
                }

                if (separatorIndex == -1)
                {
                    break;
                }

                lineAsSpan = lineAsSpan[(separatorIndex + 2)..];
            }

            sum += minRedCount * minGreenCount * minBlueCount;
        }

        return sum;
    }

    public static int UsingRegex(string[] input)
    {
        var sum = 0;
        var regex = CubesRegex();
        foreach (var line in input)
        {
            var match = regex.Match(line);
            var gameId = int.Parse(match.Groups[1].ValueSpan, CultureInfo.InvariantCulture);
            var counts = match.Groups[2];
            var colors = match.Groups[3];
            var minRedCount = 0;
            var minGreenCount = 0;
            var minBlueCount = 0;
            for (int i = 0; i < counts.Captures.Count; i++)
            {
                var count = int.Parse(counts.Captures[i].ValueSpan, CultureInfo.InvariantCulture);
                var colorFirstChar = colors.Captures[i].Value[0];
                if (colorFirstChar == 'r')
                {
                    if (minRedCount < count)
                    {
                        minRedCount = count;
                    }
                }
                else if (colorFirstChar == 'g')
                {
                    if (minGreenCount < count)
                    {
                        minGreenCount = count;
                    }
                }
                else
                {
                    if (minBlueCount < count)
                    {
                        minBlueCount = count;
                    }
                }
            }

            sum += minRedCount * minGreenCount * minBlueCount;
        }

        return sum;
    }

    [GeneratedRegex(@"Game (\d+): (?:(\d+) (red|green|blue)(?:[,;] )?)+")]
    private static partial Regex CubesRegex();
}