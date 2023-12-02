using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day1;

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
    public void Naive()
    {
        Solutions.Day1.FirstStar.Naive(_input);
    }

    [Benchmark]
    public void BetterApproach()
    {
        Solutions.Day1.FirstStar.BetterApproach(_input);
    }

    [Benchmark]
    public void UsingSearchValues()
    {
        Solutions.Day1.FirstStar.UsingSearchValues(_input);
    }
}