namespace MiniGrep.Benchmarks.Implementations;

internal static unsafe class Unsafe
{
    internal static bool Match(char* regexp, char* text)
    {
        if (regexp[0] == '^')
            return MatchHere(regexp + 1, text);

        do
        {
            if (MatchHere(regexp, text))
                return true;
        } while (*text++ != '\0');

        return false;
    }

    private static bool MatchHere(char* regexp, char* text)
    {
        if (regexp[0] == '\0')
            return true;

        if (regexp[1] == '*')
            return MatchStar(regexp[0], regexp + 2, text);

        if (regexp[0] == '$' && regexp[1] == '\0')
            return *text == '\0';

        if (*text != '\0' && (regexp[0] == '.' || regexp[0] == *text))
            return MatchHere(regexp + 1, text + 1);

        return false;
    }

    private static bool MatchStar(char c, char* regexp, char* text)
    {
        do
        {
            if (MatchHere(regexp, text))
                return true;
        } while (*text != '\0' && (*text++ == c || c == '.'));

        return false;
    }
}