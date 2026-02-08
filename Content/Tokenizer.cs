namespace HashTable.Content;

public static class Tokenizer
{
    public static void ProcessTokens(string text, Action<ReadOnlySpan<char>> action)
    {
        var span = text.AsSpan();

        int i = 0;

        while (i < span.Length)
        {
            while (i < span.Length && !char.IsAsciiLetterOrDigit(span[i]))
                i++;

            if (i >= span.Length)
                break;

            int start = i;
            
            while (i < span.Length && char.IsAsciiLetterOrDigit(span[i]))
                i++;

            var token = span.Slice(start, i - start);
            action(token);
        }
    }
}