using System.Buffers;

namespace HashTable.ClosedAddressing;

public sealed class HashBucket
{
    private int _count = 0;
    private const int Capacity = 100;
    private readonly HashTableEntry[] _entries = ArrayPool<HashTableEntry>.Shared.Rent(Capacity);
    
    public ref HashTableEntry TryFind(string key, out bool found )
    {
        for (var i = 0; i < _count; i ++)
        {
            if (_entries[i].Key == key)
            {
                found = true;   
                return ref _entries[i];
            }
        }
        found = false;
        return ref HashTableEntry.Empty;
    }

    public void Add(string key, int value)
    {
        if(_count == Capacity)
            throw new Exception("Bucket is full");
        _entries[_count].Key = key;
        _entries[_count].Value = value;
        _count++;
    }

    public void Clear() => ArrayPool<HashTableEntry>.Shared.Return(_entries);
}