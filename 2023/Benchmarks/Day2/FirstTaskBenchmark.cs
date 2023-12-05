using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day2;

[MemoryDiagnoser]
public class FirstTaskBenchmark
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
        Solutions.Day2.FirstTaskSolution.Naive(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        Solutions.Day2.FirstTaskSolution.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingRegex()
    {
        Solutions.Day2.FirstTaskSolution.UsingRegex(_input);
    }
}