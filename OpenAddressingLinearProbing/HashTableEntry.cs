namespace HashTable.OpenAddressingLinearProbing;

public struct HashTableEntry
{
    public required string Key { get; set; }
    public int Value { get; set; }
    public bool Used { get; set; }
}