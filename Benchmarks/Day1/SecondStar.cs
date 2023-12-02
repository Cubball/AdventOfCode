using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day1;

[MemoryDiagnoser]
public class SecondStar
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
        Solutions.Day1.SecondStar.Initial(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        Solutions.Day1.SecondStar.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingCustomComparison()
    {
        Solutions.Day1.SecondStar.UsingCustomComparison(_input);
    }
}