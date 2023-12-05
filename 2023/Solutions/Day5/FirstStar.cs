using System.Globalization;

namespace AdventOfCode2023.Solutions.Day5;

public struct MappingInfo
{
    public long SourceRangeStart { get; set; }

    public long DestinationRangeStart { get; set; }

    public long RangeLength { get; set; }

    public readonly long? MapFromSource(long source)
    {
        if (source < SourceRangeStart || source > SourceRangeStart + RangeLength - 1)
        {
            return null;
        }

        var offset = DestinationRangeStart - SourceRangeStart;
        return source + offset;
    }

    public static MappingInfo FromString(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var mappingInfo = new MappingInfo
        {
            SourceRangeStart = long.Parse(parts[1], CultureInfo.InvariantCulture),
            DestinationRangeStart = long.Parse(parts[0], CultureInfo.InvariantCulture),
            RangeLength = long.Parse(parts[2], CultureInfo.InvariantCulture),
        };

        return mappingInfo;
    }
}

public static class FirstStar
{
    public static long Initial(string[] input)
    {
        var seeds = input[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(long.Parse)
            .ToList();
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

        var minLocation = long.MaxValue;
        foreach (var seed in seeds)
        {
            var location = GetValueThroughChain(seed, maps);
            minLocation = Math.Min(location, minLocation);
        }

        return minLocation;
    }

    private static long GetValueThroughChain(long value, List<MappingInfo>[] maps)
    {
        foreach (var map in maps)
        {
            foreach (var info in map)
            {
                var mapped = info.MapFromSource(value);
                if (mapped is not null)
                {
                    value = mapped.Value;
                    break;
                }
            }
        }

        return value;
    }
}