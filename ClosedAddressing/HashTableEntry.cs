namespace HashTable.ClosedAddressing;

public struct HashTableEntry
{
    public static HashTableEntry Empty = new() { Key = string.Empty, Value = 0 };
    public required string Key { get; set; }
    public int Value { get; set; }
}