namespace HashTable.HashFunctions;

public static class FnvHash
{
    private const uint FnvPrime = 16777619;
    private const uint FnvOffsetBasis = 2166136261;

    public static uint Hash(ReadOnlySpan<char> data)
    {
        uint hash = FnvOffsetBasis;

        foreach (char c in data)
        {
            hash ^= c;
            hash *= FnvPrime;
        }

        return hash;
    }
}