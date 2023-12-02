using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day2;

public static partial class FirstStar
{
    public static int Naive(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var colonIndex = line.IndexOf(':');
            var gameId = int.Parse(line[5..colonIndex], CultureInfo.InvariantCulture);
            var sets = line[(colonIndex + 1)..].Split(';', StringSplitOptions.TrimEntries);
            var gameIsPossible = true;
            foreach (var set in sets)
            {
                var cubesInfo = set.Split(',', StringSplitOptions.TrimEntries);
                foreach (var cubeInfo in cubesInfo)
                {
                    var spaceIndex = cubeInfo.IndexOf(' ');
                    var cubesCount = int.Parse(cubeInfo[..spaceIndex], CultureInfo.InvariantCulture);
                    var colorFirstChar = cubeInfo[spaceIndex + 1];
                    if ((colorFirstChar == 'r' && cubesCount > 12) ||
                        (colorFirstChar == 'g' && cubesCount > 13) ||
                        (colorFirstChar == 'b' && cubesCount > 14))
                    {
                        gameIsPossible = false;
                        break;
                    }
                }

                if (!gameIsPossible)
                {
                    break;
                }
            }

            if (gameIsPossible)
            {
                sum += gameId;
            }
        }

        return sum;
    }

    public static int UsingSpans(string[] input)
    {
        var sum = 0;
        var gameId = 1;
        foreach (var line in input)
        {
            var lineAsSpan = line.AsSpan();
            var colonIndex = lineAsSpan.IndexOf(':');
            var sets = lineAsSpan[(colonIndex + 2)..];
            var gameIsPossible = true;
            while (true)
            {
                var semicolonIndex = sets.IndexOf(';');
                var set = semicolonIndex == -1 ? sets : sets[..semicolonIndex];
                while (true)
                {
                    var commaIndex = set.IndexOf(',');
                    var slice = commaIndex == -1 ? set : set[..commaIndex];
                    var spaceIndex = slice.IndexOf(' ');
                    var cubesCount = int.Parse(slice[..spaceIndex], CultureInfo.InvariantCulture);
                    var colorFirstChar = slice[spaceIndex + 1];
                    if ((colorFirstChar == 'r' && cubesCount > 12) ||
                        (colorFirstChar == 'g' && cubesCount > 13) ||
                        (colorFirstChar == 'b' && cubesCount > 14))
                    {
                        gameIsPossible = false;
                        break;
                    }

                    if (commaIndex == -1)
                    {
                        break;
                    }

                    set = set[(commaIndex + 2)..];
                }

                if (semicolonIndex == -1 || !gameIsPossible)
                {
                    break;
                }

                sets = sets[(semicolonIndex + 2)..];
            }

            if (gameIsPossible)
            {
                sum += gameId;
            }

            gameId++;
        }

        return sum;
    }

    public static int UsingRegex(string[] input)
    {
        var sum = 0;
        var regex = CubesRegex();
        foreach (var line in input)
        {
            var gameIsPossible = true;
            var match = regex.Match(line);
            var gameId = int.Parse(match.Groups[1].ValueSpan, CultureInfo.InvariantCulture);
            var counts = match.Groups[2];
            var colors = match.Groups[3];
            for (int i = 0; i < counts.Captures.Count; i++)
            {
                var count = int.Parse(counts.Captures[i].ValueSpan, CultureInfo.InvariantCulture);
                var colorFirstChar = colors.Captures[i].Value[0];
                if ((colorFirstChar == 'r' && count > 12) ||
                    (colorFirstChar == 'g' && count > 13) ||
                    (colorFirstChar == 'b' && count > 14))
                {
                    gameIsPossible = false;
                    break;
                }
            }

            if (gameIsPossible)
            {
                sum += gameId;
            }
        }

        return sum;
    }

    [GeneratedRegex(@"Game (\d+): (?:(?:(\d+) (red|green|blue)(?:, )?)+(?:; )?)+")]
    private static partial Regex CubesRegex();
}