using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace AdventOfCode2023.Day19;

public record struct Part(int X, int M, int A, int S)
{
    public int Sum => X + M + A + S;

    public static Part FromString(string str)
    {
        var span = str.AsSpan()[1..^1];
        var part = new Part();
        Span<Range> ranges = stackalloc Range[4];
        span.Split(ranges, ',');
        part.X = int.Parse(span[ranges[0]][2..], CultureInfo.InvariantCulture);
        part.M = int.Parse(span[ranges[1]][2..], CultureInfo.InvariantCulture);
        part.A = int.Parse(span[ranges[2]][2..], CultureInfo.InvariantCulture);
        part.S = int.Parse(span[ranges[3]][2..], CultureInfo.InvariantCulture);
        return part;
    }
}

public class Workflow
{
    private static readonly MethodInfo ExecuteMethodInfo = typeof(Workflow).GetMethod(nameof(Execute))!;
    private static readonly PropertyInfo DictionaryIndexerInfo = typeof(Dictionary<string, Workflow>).GetProperty("Item", new Type[] { typeof(string) })!;
    private static readonly Type PartType = typeof(Part);

    private readonly Dictionary<string, Workflow> _workflows;
    private Func<Part, int> _func = null!;

    private Workflow(string name, Dictionary<string, Workflow> workflows)
    {
        Name = name;
        _workflows = workflows;
    }

    public string Name { get; set; }

    public void ParseRules(ReadOnlySpan<char> rules)
    {
        rules = rules[1..^1];
        var parameter = Expression.Parameter(PartType);
        var expression = ParseRule(rules, parameter);
        _func = Expression.Lambda<Func<Part, int>>(expression, parameter).Compile();
    }

    public int Execute(Part part) => _func(part);

    public static void AddWorkflow(string str, Dictionary<string, Workflow> workflows)
    {
        var braceIdx = str.IndexOf('{');
        var name = str[..braceIdx];
        var workflow = new Workflow(name, workflows);
        workflows[name] = workflow;
        workflow.ParseRules(str.AsSpan()[braceIdx..]);
    }

    private Expression ParseRule(ReadOnlySpan<char> rule, ParameterExpression parameter)
    {
        if (rule.Length == 1 && rule[0] == 'A')
        {
            return Expression.Property(parameter, nameof(Part.Sum));
        }

        if (rule.Length == 1 && rule[0] == 'R')
        {
            return Expression.Constant(0);
        }

        if (rule[1] is not '<' and not '>')
        {
            return Expression.Call(
                    Expression.Property(Expression.Constant(_workflows), DictionaryIndexerInfo, Expression.Constant(rule.ToString())),
                    ExecuteMethodInfo,
                    parameter);
        }

        var propertyName = rule[0] switch
        {
            'x' => nameof(Part.X),
            'm' => nameof(Part.M),
            'a' => nameof(Part.A),
            's' => nameof(Part.S),
            _ => "",
        };
        var colonIdx = rule.IndexOf(':');
        var commaIdx = rule.IndexOf(',');
        var number = int.Parse(rule[2..colonIdx], CultureInfo.InvariantCulture);
        var condition = rule[1] == '<'
            ? Expression.LessThan(Expression.Property(parameter, propertyName), Expression.Constant(number))
            : Expression.GreaterThan(Expression.Property(parameter, propertyName), Expression.Constant(number));
        return Expression.Condition(condition,
                ParseRule(rule[(colonIdx + 1)..commaIdx], parameter),
                ParseRule(rule[(commaIdx + 1)..], parameter));
    }
}

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

    public static int UsingExpressions(string[] input)
    {
        var workflows = new Dictionary<string, Workflow>();
        var parts = new List<Part>();
        var foundEmptyLine = false;
        for (int i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                foundEmptyLine = true;
                continue;
            }

            if (foundEmptyLine)
            {
                parts.Add(Part.FromString(input[i]));
            }
            else
            {
                Workflow.AddWorkflow(input[i], workflows);
            }
        }

        var startWorkflow = workflows["in"];
        var sum = 0;
        foreach (var part in parts)
        {
            sum += startWorkflow.Execute(part);
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