using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day4;

[MemoryDiagnoser]
public class FirstTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day4\Input.txt"));
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
}