namespace HashTable.HashFunctions;

public static class DotNetHash
{
    public static uint Hash(ReadOnlySpan<char> key)
    {
        return unchecked((uint)string.GetHashCode(key));
    }
}