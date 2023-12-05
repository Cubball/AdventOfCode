using System.Globalization;

namespace AdventOfCode2023.Day5;

public static class MappingInfoExtension
{
    public static long? MapFromDestination(this MappingInfo mappingInfo, long destination)
    {
        if (destination < mappingInfo.DestinationRangeStart || destination > mappingInfo.DestinationRangeStart + mappingInfo.RangeLength - 1)
        {
            return null;
        }

        var offset = mappingInfo.SourceRangeStart - mappingInfo.DestinationRangeStart;
        return destination + offset;
    }
}

public static class SecondTaskSolution
{
    public static long Initial(string[] input)
    {
        var values = input[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .ToArray();
        var seeds = new List<(long Start, long Length)>();
        for (int i = 0; i < values.Length - 1; i += 2)
        {
            seeds.Add((long.Parse(values[i], CultureInfo.InvariantCulture), long.Parse(values[i + 1], CultureInfo.InvariantCulture)));
        }

        var maps = new List<MappingInfo>[7] { new(), new(), new(), new(), new(), new(), new() };
        var mapIndex = -1;
        for (int i = 1; i < input.Length; i++)
        {
            var line = input[i];
            if (line.Length == 0)
            {
                i++;
                mapIndex++;
                continue;
            }

            maps[mapIndex].Add(MappingInfo.FromString(line));
        }

        for (long i = 1; i <= long.MaxValue; i++)
        {
            if (SeedForLocationExsits(i, maps, seeds))
            {
                return i;
            }
        }

        return -1;
    }

    private static bool SeedForLocationExsits(long location, List<MappingInfo>[] maps, List<(long Start, long Length)> seeds)
    {
        for (int i = maps.Length - 1; i >= 0; i--)
        {
            foreach (var info in maps[i])
            {
                var mapped = info.MapFromDestination(location);
                if (mapped is not null)
                {
                    location = mapped.Value;
                    break;
                }
            }
        }

        foreach (var (start, length) in seeds)
        {
            if (location >= start && location <= start + length - 1)
            {
                return true;
            }
        }

        return false;
    }
}