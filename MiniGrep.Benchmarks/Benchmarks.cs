using BenchmarkDotNet.Attributes;

namespace MiniGrep.Benchmarks;

[MemoryDiagnoser]
public class Benchmarks
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    [ParamsSource(nameof(ParameterSource))]
    public (string Regexp, string Text) Parameters { get; set; }

    public static IEnumerable<(string Regexp, string Text)> ParameterSource()
    {
        return new[]
        {
            ("^a.*e$", "argyle"),
            ("^a*.b*.c*.d*.e*.f*.g*.h*.i*.j*.$", "aaa1bbb2ccc3ddd4eee5fff6ggg7hhh8iii9")
        };
    }

    [Benchmark]
    public unsafe bool Unsafe()
    {
        fixed (char* regexp = Parameters.Regexp, text = Parameters.Text)
        {
            return Implementations.Unsafe.Match(regexp, text);
        }
    }

    [Benchmark]
    public bool Substring()
    {
        return Implementations.Substring.Match(Parameters.Regexp, Parameters.Text);
    }

    [Benchmark(Baseline = true)]
    public bool Stdlib()
    {
        return Implementations.Stdlib.Match(Parameters.Regexp, Parameters.Text);
    }

    [Benchmark]
    public bool Span()
    {
        return Implementations.Span.Match(Parameters.Regexp, Parameters.Text);
    }
}