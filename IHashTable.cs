namespace HashTable;

public interface IHashTable
{
    public HashFunction HashFunction { get; set; }
    public void Add(string key );
    public int GetValue(string key);
    public void Dispose();
}