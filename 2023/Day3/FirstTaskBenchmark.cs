using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day3;

[MemoryDiagnoser]
public class FirstTaskBenchmark
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
        FirstTaskSolution.UsingSpans(_input);
    }

    [Benchmark]
    public void UsingRegex()
    {
        FirstTaskSolution.UsingRegex(_input);
    }
}