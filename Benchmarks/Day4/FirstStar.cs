using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day4;

[MemoryDiagnoser]
public class FirstStar
{
    private string[] _input = null!;

    [GlobalSetup]
    public void Setup()
    {
        _input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\..\..\..\Inputs\Day4.txt"));
    }

    [Benchmark]
    public void Simple()
    {
        Solutions.Day4.FirstStar.Simple(_input);
    }

    [Benchmark]
    public void LINQ()
    {
        Solutions.Day4.FirstStar.LINQ(_input);
    }
}