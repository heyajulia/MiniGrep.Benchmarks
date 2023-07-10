namespace MiniGrep.Benchmarks.Implementations;

internal static class Substring
{
    internal static bool Match(string regexp, string text)
    {
        if (regexp[0] == '^')
            return MatchHere(regexp[1..], text);

        var t = text;

        do
        {
            if (MatchHere(regexp, text))
                return true;
        } while ((t = t[1..]).Length != 0);

        return false;
    }

    private static bool MatchHere(string regexp, string text)
    {
        if (regexp.Length == 0)
            return true;

        if (regexp.Length >= 2 && regexp[1] == '*')
            return MatchStar(regexp[0], regexp[2..], text);

        if (regexp is ['$'])
            return text.Length == 0;

        if (text.Length != 0 && (regexp[0] == '.' || regexp[0] == text[0]))
            return MatchHere(regexp[1..], text[1..]);

        return false;
    }

    private static bool MatchStar(char c, string regexp, string text)
    {
        var t = text;

        if (MatchHere(regexp, t))
            return true;

        while (t.Length != 0 && ((t = t[1..])[0] == c || c == '.'))
            if (MatchHere(regexp, t))
                return true;

        return false;
    }
}