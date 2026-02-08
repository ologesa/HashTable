namespace HashTable.HashFunctions;

public static class Djb2Hash
{
    public static uint Hash(ReadOnlySpan<char> key)
    {
        uint hash = 5381;
        foreach (char c in key)
        {
            hash = (hash << 5) + hash + c;
        }
        return hash;
    }
}