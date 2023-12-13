namespace AdventOfCode2023.Day12;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var sum = 0;
        foreach (var (line, groups) in Parse(input))
        {
            sum += Recurse(line, 0, groups);
        }

        return sum;
    }

    private static List<(char[], List<int>)> Parse(string[] input)
    {
        var list = new List<(char[], List<int>)>();
        foreach (var line in input)
        {
            var parts = line.Split(' ');
            var numbers = parts[1].Split(',').Select(int.Parse).ToList();
            list.Add((parts[0].ToCharArray(), numbers));
        }

        return list;
    }

    private static int Recurse(char[] line, int index, List<int> groups)
    {
        if (index == line.Length)
        {
            return IsValid(line, groups) ? 1 : 0;
        }

        if (line[index] != '?')
        {
            return Recurse(line, index + 1, groups);
        }

        var sum = 0;
        line[index] = '#';
        sum += Recurse(line, index + 1, groups);
        line[index] = '.';
        sum += Recurse(line, index + 1, groups);
        line[index] = '?';
        return sum;
    }

    private static bool IsValid(char[] line, List<int> groups)
    {
        var groupIndex = -1;
        var index = 0;
        while (index < line.Length)
        {
            if (line[index] != '#')
            {
                index++;
                continue;
            }

            if (++groupIndex >= groups.Count)
            {
                return false;
            }

            var groupCount = 1;
            while (++index < line.Length && line[index] == '#')
            {
                groupCount++;
            }

            if (groupCount != groups[groupIndex])
            {
                return false;
            }
        }

        return groupIndex == groups.Count - 1;
    }
}