using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[SimpleJob(RuntimeMoniker.Net481, baseline: true)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class MemoryComparison
{

    [Params(10, 1_024, 1_048_576, 1073741824)]
    public int Length { get; set; }

#if NET481
        byte[] first;
        byte[] second;
#else
    byte[] first = null!;
    byte[] second = null!;
#endif
    [GlobalSetup]
    public void Setup()
    {
        var r = new Random(0);

        first = new byte[Length];
        second = new byte[Length];

        r.NextBytes(first);
        Array.Copy(first, second, Length);
    }

    [Benchmark]
    public void MemCmp()
    {
        MemoryComparer.EqualsMemCmp(first, second);
    }

    [Benchmark]
    public void Loop()
    {
        MemoryComparer.EqualsLoop(first, second);
    }

    [Benchmark]
    public void SequenceEqual()
    {
        MemoryComparer.EqualsSequenceEqual(first, second);
    }

    [Benchmark]
    public void Span()
    {
        MemoryComparer.EqualsSpan(first, second);
    }
}
