internal class Program
{
    private static void Main(string[] args)
    {
        BenchmarkDotNet.Running.BenchmarkRunner.Run<MemoryComparison>();
    }
}