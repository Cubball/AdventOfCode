using System.Globalization;

namespace AdventOfCode2023.Day5;

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

    public readonly long? MapFromDestination(long destination)
    {
        if (destination < DestinationRangeStart || destination > DestinationRangeStart + RangeLength - 1)
        {
            return null;
        }

        var offset = SourceRangeStart - DestinationRangeStart;
        return destination + offset;
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

    public static MappingInfo FromSpan(ReadOnlySpan<char> line)
    {
        Span<Range> ranges = stackalloc Range[3];
        line.Split(ranges, ' ', StringSplitOptions.RemoveEmptyEntries);
        var mappingInfo = new MappingInfo
        {
            SourceRangeStart = long.Parse(line[ranges[1]], CultureInfo.InvariantCulture),
            DestinationRangeStart = long.Parse(line[ranges[0]], CultureInfo.InvariantCulture),
            RangeLength = long.Parse(line[ranges[2]], CultureInfo.InvariantCulture),
        };

        return mappingInfo;
    }
}

public static class FirstTaskSolution
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

    public static long UsingSpans(string[] input)
    {
        var seeds = ParseSeeds(input[0]);
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

            maps[mapIndex].Add(MappingInfo.FromSpan(line));
        }

        var minLocation = long.MaxValue;
        foreach (var seed in seeds)
        {
            var location = GetValueThroughChain(seed, maps);
            minLocation = Math.Min(location, minLocation);
        }

        return minLocation;
    }

    private static List<long> ParseSeeds(ReadOnlySpan<char> line)
    {
        var spaceIndex = line.IndexOf(' ');
        var list = new List<long>();
        while (spaceIndex < line.Length)
        {
            line = line[(spaceIndex + 1)..];
            if (line.IsEmpty)
            {
                break;
            }

            var nextSpaceIndex = line.IndexOf(' ');
            if (nextSpaceIndex == -1)
            {
                nextSpaceIndex = line.Length;
            }

            list.Add(long.Parse(line[..nextSpaceIndex], CultureInfo.InvariantCulture));
            spaceIndex = nextSpaceIndex;
        }

        return list;
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