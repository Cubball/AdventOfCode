using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day3;

[MemoryDiagnoser]
public class FirstStar
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
        Solutions.Day3.FirstStar.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingRegex()
    {
        Solutions.Day3.FirstStar.UsingRegex(_input);
    }
}