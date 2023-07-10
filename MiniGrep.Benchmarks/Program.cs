#if RELEASE
using BenchmarkDotNet.Running;
#endif

namespace MiniGrep.Benchmarks;

internal static class Program
{
    private static void Main()
    {
#if RELEASE
        BenchmarkRunner.Run<Benchmarks>();
#else
#error Please switch to Release configuration to run the benchmarks.
#endif
    }
}