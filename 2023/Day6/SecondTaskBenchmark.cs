using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day6;

[MemoryDiagnoser]
public class SecondTaskBenchmark
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
        SecondTaskSolution.Initial(_input);
    }

    [Benchmark]
    public void ReplaceInsteadOfSplit()
    {
        SecondTaskSolution.ReplaceInsteadOfSplit(_input);
    }

    [Benchmark]
    public void UsingSpans()
    {
        SecondTaskSolution.UsingSpans(_input);
    }
}