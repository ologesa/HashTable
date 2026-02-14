namespace HashTable.OpenAddressingRobinHood;

public record struct HashTableEntry
{
    public required string Key { get; set; }
    public uint Distance { get; set; }
    public int Value { get; set; }
    public bool Used { get; set; }
}