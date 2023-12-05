using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day2;

[MemoryDiagnoser]
public class FirstTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day2\Input.txt"));
    }

    [Benchmark]
    public void Naive()
    {
        FirstTaskSolution.Naive(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        FirstTaskSolution.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingRegex()
    {
        FirstTaskSolution.UsingRegex(_input);
    }
}