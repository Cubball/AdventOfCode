using System.Globalization;

namespace AdventOfCode2023.Solutions.Day2;

public static class SecondStar
{
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

    public static int UsingSpans(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var lineAsSpan = line.AsSpan();
            var colonIndex = lineAsSpan.IndexOf(':');
            var sets = lineAsSpan[(colonIndex + 2)..];
            var minRedCount = 0;
            var minGreenCount = 0;
            var minBlueCount = 0;
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

                    if (commaIndex == -1)
                    {
                        break;
                    }

                    set = set[(commaIndex + 2)..];
                }

                if (semicolonIndex == -1)
                {
                    break;
                }

                sets = sets[(semicolonIndex + 2)..];
            }

            sum += minRedCount * minGreenCount * minBlueCount;
        }

        return sum;
    }
}