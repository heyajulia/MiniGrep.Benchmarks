using System.Text.RegularExpressions;

namespace MiniGrep.Benchmarks.Implementations;

internal static class Stdlib
{
    internal static bool Match(string regexp, string text)
    {
        return Regex.Match(text, regexp).Success;
    }
}