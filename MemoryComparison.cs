using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[SimpleJob(RuntimeMoniker.Net481, baseline: true)]
[SimpleJob(RuntimeMoniker.Net80)]
public class MemoryComparison
{

    [Params(10, 1_024, 1_048_576)]
    public int Length { get; set; }

    byte[] first;
    byte[] second;

    [GlobalSetup]
    public void Setup()
    {
        var r = new Random(0);

        first = new byte[Length];
        second = new byte[Length];

        r.NextBytes(first);
        Array.Copy(first, second, Length);
    }

    public void MemCmp()
    {
        MemoryComparer.EqualsMemCmp(first, second);
    }

    public void Loop()
    {
        MemoryComparer.EqualsLoop(first, second);
    }

    public void SequenceEqual()
    {
        MemoryComparer.EqualsSequenceEqual(first, second);
    }


}
