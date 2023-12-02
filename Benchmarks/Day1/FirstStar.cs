using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Benchmarks.Day1;

[MemoryDiagnoser]
public class FirstStar
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Inputs\Day1.txt"));
    }

    [Benchmark]
    public void Initial()
    {
        Solutions.Day1.FirstStar.Initial(_input);
    }

    [Benchmark]
    public void BetterApproach()
    {
        Solutions.Day1.FirstStar.BetterApproach(_input);
    }

    [Benchmark]
    public void InitialUsingSpans()
    {
        Solutions.Day1.FirstStar.InitialUsingSpans(_input);
    }

    [Benchmark]
    public void BetterApproachUsingSpans()
    {
        Solutions.Day1.FirstStar.BetterApproachUsingSpans(_input);
    }
}