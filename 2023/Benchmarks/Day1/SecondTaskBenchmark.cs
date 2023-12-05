using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day1;

[MemoryDiagnoser]
public class SecondTaskBenchmark
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
        Solutions.Day1.SecondTaskSolution.Naive(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        Solutions.Day1.SecondTaskSolution.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingCustomComparison()
    {
        Solutions.Day1.SecondTaskSolution.UsingCustomComparison(_input);
    }
}