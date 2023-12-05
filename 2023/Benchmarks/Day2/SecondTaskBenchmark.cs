using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day2;

[MemoryDiagnoser]
public class SecondTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Inputs\Day2.txt"));
    }

    [Benchmark]
    public void Naive()
    {
        Solutions.Day2.SecondTaskSolution.Naive(_input);
    }

    [Benchmark]
    public void BetterApproach()
    {
        Solutions.Day2.SecondTaskSolution.BetterApproach(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        Solutions.Day2.SecondTaskSolution.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingRegex()
    {
        Solutions.Day2.SecondTaskSolution.UsingRegex(_input);
    }
}