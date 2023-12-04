using BenchmarkDotNet.Attributes;

namespace AdventOfCode2023.Benchmarks.Day4;

[MemoryDiagnoser]
public class SecondStar
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
        Solutions.Day4.SecondStar.Simple(_input);
    }
}