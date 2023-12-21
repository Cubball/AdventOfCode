using System.Globalization;

namespace AdventOfCode2023.Day19;

public static class SecondTaskSolution
{
    public static long Initial(string[] input)
    {
        var workflows = new Dictionary<string, string[]>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }

            var braceIdx = line.IndexOf('{');
            workflows[line[..braceIdx]] = line[(braceIdx + 1)..^1].Split(',');
        }

        return GetAcceptedCount(workflows);
    }

    private static long GetAcceptedCount(
            Dictionary<string, string[]> workflows,
            string workflowName = "in",
            int ruleIdx = 0,
            int xStart = 1,
            int xEnd = 4000,
            int mStart = 1,
            int mEnd = 4000,
            int aStart = 1,
            int aEnd = 4000,
            int sStart = 1,
            int sEnd = 4000)
    {
        if (xStart > xEnd || mStart > mEnd || aStart > aEnd || sStart > sEnd)
        {
            return 0;
        }

        if (workflowName == "R")
        {
            return 0;
        }

        if (workflowName == "A")
        {
            return (xEnd - xStart + 1L) * (mEnd - mStart + 1) * (aEnd - aStart + 1) * (sEnd - sStart + 1);
        }


        var rule = workflows[workflowName][ruleIdx];
        if (rule.Length < 2 || rule[1] is not '>' and not '<')
        {
            return GetAcceptedCount(workflows, workflows[workflowName][ruleIdx], 0, xStart, xEnd, mStart, mEnd, aStart, aEnd, sStart, sEnd);
        }

        var less = rule[1] == '<';
        var colonIdx = rule.IndexOf(':');
        var number = int.Parse(rule[2..colonIdx], CultureInfo.InvariantCulture);
        var nextWorkflowName = rule[(colonIdx + 1)..];
        return less
            ? rule[0] switch
            {
                'x' => GetAcceptedCount(workflows, nextWorkflowName, 0, xStart, number - 1, mStart, mEnd, aStart, aEnd, sStart, sEnd) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, number, xEnd, mStart, mEnd, aStart, aEnd, sStart, sEnd),
                'm' => GetAcceptedCount(workflows, nextWorkflowName, 0, xStart, xEnd, mStart, number - 1, aStart, aEnd, sStart, sEnd) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, xStart, xEnd, number, mEnd, aStart, aEnd, sStart, sEnd),
                'a' => GetAcceptedCount(workflows, nextWorkflowName, 0, xStart, xEnd, mStart, mEnd, aStart, number - 1, sStart, sEnd) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, xStart, xEnd, mStart, mEnd, number, aEnd, sStart, sEnd),
                's' => GetAcceptedCount(workflows, nextWorkflowName, 0, xStart, xEnd, mStart, mEnd, aStart, aEnd, sStart, number - 1) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, xStart, xEnd, mStart, mEnd, aStart, aEnd, number, sEnd),
                _ => -1,
            }
            : rule[0] switch
            {
                'x' => GetAcceptedCount(workflows, nextWorkflowName, 0, number + 1, xEnd, mStart, mEnd, aStart, aEnd, sStart, sEnd) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, xStart, number, mStart, mEnd, aStart, aEnd, sStart, sEnd),
                'm' => GetAcceptedCount(workflows, nextWorkflowName, 0, xStart, xEnd, number + 1, mEnd, aStart, aEnd, sStart, sEnd) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, xStart, xEnd, mStart, number, aStart, aEnd, sStart, sEnd),
                'a' => GetAcceptedCount(workflows, nextWorkflowName, 0, xStart, xEnd, mStart, mEnd, number + 1, aEnd, sStart, sEnd) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, xStart, xEnd, mStart, mEnd, aStart, number, sStart, sEnd),
                's' => GetAcceptedCount(workflows, nextWorkflowName, 0, xStart, xEnd, mStart, mEnd, aStart, aEnd, number + 1, sEnd) +
                       GetAcceptedCount(workflows, workflowName, ruleIdx + 1, xStart, xEnd, mStart, mEnd, aStart, aEnd, sStart, number),
                _ => -1,
            };
    }
}