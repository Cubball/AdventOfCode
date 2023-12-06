using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day6;

[MemoryDiagnoser]
public class FirstTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day6\Input.txt"));
    }

    [Benchmark]
    public void Initial()
    {
        FirstTaskSolution.Initial(_input);
    }

    [Benchmark]
    public void LINQ()
    {
        FirstTaskSolution.LINQ(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        FirstTaskSolution.UsingSpans(_input);
    }
}