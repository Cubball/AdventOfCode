using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day3;

[MemoryDiagnoser]
public class SecondStar
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Inputs\Day3.txt"));
    }

    [Benchmark]
    public void UsingSpans()
    {
        Solutions.Day3.SecondStar.UsingSpans(_input);
    }
}