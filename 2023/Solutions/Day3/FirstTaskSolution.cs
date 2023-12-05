using System.Buffers;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions.Day3;

public static partial class FirstTaskSolution
{
    private static readonly SearchValues<char> DigitSearchValues = SearchValues.Create(".0123456789");

    public static int UsingRegex(string[] input)
    {
        var sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            var matches = NumbersRegex().Matches(line);
            for (int j = 0; j < matches.Count; j++)
            {
                var match = matches[j];
                var start = match.Index;
                var end = start + match.Length - 1;
                var isPartNumber = (start > 0 && line[start - 1] != '.') ||
                                   (end < line.Length - 1 && line[end + 1] != '.');
                var left = Math.Max(0, start - 1);
                var right = Math.Min(line.Length, end + 2);
                if (!isPartNumber && i > 0)
                {
                    var span = input[i - 1].AsSpan()[left..right];
                    isPartNumber = span.ContainsAnyExcept(DigitSearchValues);
                }

                if (!isPartNumber && i < line.Length - 1)
                {
                    var span = input[i + 1].AsSpan()[left..right];
                    isPartNumber = span.ContainsAnyExcept(DigitSearchValues);
                }

                if (isPartNumber)
                {
                    sum += int.Parse(match.ValueSpan, CultureInfo.InvariantCulture);
                }
            }
        }

        return sum;
    }

    public static int UsingSpans(string[] input)
    {
        var sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i].AsSpan();
            var span = line;
            var offset = 0;
            while (true)
            {
                var start = span.IndexOfAnyInRange('1', '9');
                if (start == -1)
                {
                    break;
                }

                var end = span[start..].IndexOfAnyExceptInRange('0', '9') - 1;
                if (end < 0)
                {
                    end = span.Length - 1;
                }
                else
                {
                    end += start;
                }

                var isPartNumber = AdjacentToSymbol(line, start + offset, end + offset);
                if (!isPartNumber && i > 0)
                {
                    isPartNumber = ContainsSymbol(input[i - 1], start + offset, end + offset);
                }

                if (!isPartNumber && i < input.Length - 1)
                {
                    isPartNumber = ContainsSymbol(input[i + 1], start + offset, end + offset);
                }

                if (isPartNumber)
                {
                    sum += int.Parse(span[start..(end + 1)], CultureInfo.InvariantCulture);
                }

                if (end == span.Length - 1)
                {
                    break;
                }

                span = span[(end + 1)..];
                offset += end + 1;
            }
        }

        return sum;
    }

    private static bool ContainsSymbol(ReadOnlySpan<char> line, int numberStart, int numberEnd)
    {
        var startIndex = Math.Max(0, numberStart - 1);
        var endIndex = Math.Min(line.Length, numberEnd + 2);
        return line[startIndex..endIndex].ContainsAnyExcept(DigitSearchValues);
    }

    private static bool AdjacentToSymbol(ReadOnlySpan<char> line, int numberStart, int numberEnd)
    {
        return (numberStart > 0 && line[numberStart - 1] != '.') ||
               (numberEnd < line.Length - 1 && line[numberEnd + 1] != '.');
    }

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex NumbersRegex();
}