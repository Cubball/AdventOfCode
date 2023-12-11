using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day11;

[MemoryDiagnoser]
public class FirstTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day11\Input.txt"));
    }

    [Benchmark]
    public void Initial()
    {
        FirstTaskSolution.Initial(_input);
    }

    [Benchmark]
    public void WithoutBFS()
    {
        FirstTaskSolution.WithoutBFS(_input);
    }

    [Benchmark]
    public void WithoutExpanding()
    {
        FirstTaskSolution.WithoutExpanding(_input);
    }
}