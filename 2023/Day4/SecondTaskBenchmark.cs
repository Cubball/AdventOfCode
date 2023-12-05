using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Day4;

[MemoryDiagnoser]
public class SecondTaskBenchmark
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Day4\Input.txt"));
    }

    [Benchmark]
    public void Simple()
    {
        SecondTaskSolution.Simple(_input);
    }
}