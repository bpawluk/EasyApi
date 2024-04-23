using BenchmarkDotNet.Running;

namespace BlazorUtils.EasyApi.Benchmarks;

public class Program
{
    static void Main(string[] _)
    {
        BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}
