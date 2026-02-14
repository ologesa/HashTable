namespace HashTable;

public interface IHashTable
{
    HashFunction HashFunction { get; set; }
    void Add(string key );
    int GetValue(string key);
    void Dispose();
    double GetLoadFactor(); 
}