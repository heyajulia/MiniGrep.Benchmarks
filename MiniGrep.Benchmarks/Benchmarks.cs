using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;

namespace MiniGrep.Benchmarks;

[MemoryDiagnoser]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class Benchmarks
{
    [Params("^a.*e$", "^a*.b*.c*.d*.e*.f*.g*.h*.i*.j*.$")]
    public string Regexp { get; set; } = null!;

    [Params("argyle", "aaa1bbb2ccc3ddd4eee5fff6ggg7hhh8iii9")]
    public string Text { get; set; } = null!;

    [Benchmark]
    public unsafe bool Unsafe()
    {
        fixed (char* regexp = Regexp, text = Text)
        {
            return Implementations.Unsafe.Match(regexp, text);
        }
    }

    [Benchmark]
    public bool Substring()
    {
        return Implementations.Substring.Match(Regexp, Text);
    }

    [Benchmark(Baseline = true)]
    public bool Stdlib()
    {
        return Implementations.Stdlib.Match(Regexp, Text);
    }

    [Benchmark]
    public bool Span()
    {
        return Implementations.Span.Match(Regexp, Text);
    }
}