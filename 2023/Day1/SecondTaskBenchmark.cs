using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day1;

[MemoryDiagnoser]
public class SecondTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day1\Input.txt"));
    }

    [Benchmark]
    public void Naive()
    {
        SecondTaskSolution.Naive(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        SecondTaskSolution.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingCustomComparison()
    {
        SecondTaskSolution.UsingCustomComparison(_input);
    }
}