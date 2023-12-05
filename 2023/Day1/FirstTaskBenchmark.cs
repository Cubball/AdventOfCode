using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day1;

[MemoryDiagnoser]
public class FirstTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day1\Input.txt"));
    }

    [Benchmark]
    public void Initial()
    {
        FirstTaskSolution.Initial(_input);
    }

    [Benchmark]
    public void BetterApproach()
    {
        FirstTaskSolution.BetterApproach(_input);
    }

    [Benchmark]
    public void UsingSearchValues()
    {
        FirstTaskSolution.UsingSearchValues(_input);
    }
}