using System.Globalization;

namespace AdventOfCode2023.Day19;

public static class FirstTaskSolution
{
    public static int Initial(string[] input)
    {
        var emptyLineIndex = Array.FindIndex(input, string.IsNullOrWhiteSpace);
        var workflows = new Dictionary<string, string[]>();
        for (int i = 0; i < emptyLineIndex; i++)
        {
            var line = input[i];
            var idx = line.IndexOf('{');
            workflows[line[..idx]] = line[(idx + 1)..^1].Split(',').ToArray();
        }

        var parts = input[(emptyLineIndex + 1)..]
            .Select(l => l[1..^1]
                        .Split(',')
                        .Select(p => int.Parse(p[2..], CultureInfo.InvariantCulture))
                        .ToArray());
        var sum = 0;
        foreach (var part in parts)
        {
            sum += ProcessPart(workflows, part);
        }

        return sum;
    }

    private static int ProcessPart(Dictionary<string, string[]> workflows, int[] part)
    {
        var workflowName = "in";
        var ruleIdx = 0;
        while (true)
        {
            if (workflowName == "A")
            {
                return part.Sum();
            }

            if (workflowName == "R")
            {
                return 0;
            }

            var rule = workflows[workflowName][ruleIdx];
            var characteristic = rule[0] switch
            {
                'x' => 0,
                'm' => 1,
                'a' => 2,
                's' => 3,
                _ => -1,
            };

            var less = rule[1] == '<';
            var colonIdx = rule.IndexOf(':');
            var number = int.Parse(rule[2..colonIdx], CultureInfo.InvariantCulture);
            if (less)
            {
                if (part[characteristic] < number)
                {
                    workflowName = rule[(colonIdx + 1)..];
                    ruleIdx = 0;
                }
                else if (ruleIdx == workflows[workflowName].Length - 2)
                {
                    workflowName = workflows[workflowName][^1];
                    ruleIdx = 0;
                }
                else
                {
                    ruleIdx++;
                }
            }
            else
            {
                if (part[characteristic] > number)
                {
                    workflowName = rule[(colonIdx + 1)..];
                    ruleIdx = 0;
                }
                else if (ruleIdx == workflows[workflowName].Length - 2)
                {
                    workflowName = workflows[workflowName][^1];
                    ruleIdx = 0;
                }
                else
                {
                    ruleIdx++;
                }

            }
        }
    }
}