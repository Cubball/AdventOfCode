using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day3;

[MemoryDiagnoser]
public class SecondTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day3\Input.txt"));
    }

    [Benchmark]
    public void UsingSpans()
    {
        SecondTaskSolution.UsingSpans(_input);
    }
}