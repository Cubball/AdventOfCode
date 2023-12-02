using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day2;

[MemoryDiagnoser]
public class SecondStar
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
        Solutions.Day2.SecondStar.Naive(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        Solutions.Day2.SecondStar.UsingSpans(_input);
    }
}