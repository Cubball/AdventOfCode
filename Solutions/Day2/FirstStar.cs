using System.Globalization;

namespace AdventOfCode2023.Solutions.Day2;

public static class FirstStar
{
    public static int Naive(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var semicolonIndex = line.IndexOf(':');
            var gameId = int.Parse(line[5..semicolonIndex], CultureInfo.InvariantCulture);
            var sets = line[(semicolonIndex + 1)..].Split(';', StringSplitOptions.TrimEntries);
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
                    }
                }
            }

            if (gameIsPossible)
            {
                sum += gameId;
            }
        }

        return sum;
    }
}