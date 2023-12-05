using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day2;

[MemoryDiagnoser]
public class SecondTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day2\Input.txt"));
    }

    [Benchmark]
    public void Initial()
    {
        SecondTaskSolution.Initial(_input);
    }

    [Benchmark]
    public void BetterApproach()
    {
        SecondTaskSolution.BetterApproach(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        SecondTaskSolution.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingRegex()
    {
        SecondTaskSolution.UsingRegex(_input);
    }
}