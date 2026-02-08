using System.Buffers;
using HashTable.HashFunctions;

namespace HashTable.ClosedAddressing;

public sealed class CloseAddressingHashTable : IHashTable
{
    private const int Buckets = 500;
    private readonly HashBucket[] _buckets;
    public HashFunction HashFunction { get; set; } = DotNetHash.Hash; // just a default

    public CloseAddressingHashTable()
    {
        _buckets = ArrayPool<HashBucket>.Shared.Rent(Buckets);
        for (int i = 0; i < Buckets; i++)
            _buckets[i] = new HashBucket();
    }

    public void Add(string key)
    {
        uint hashIndex = HashFunction(key) % Buckets;
        var bucket = _buckets[hashIndex];
        ref var entry = ref bucket.TryFind(key, out bool found);
        if (found)
        {
            entry.Value++;
            return;
        }
        bucket.Add(key, 1);
    }

    public int GetValue(string key)
    {
        uint hashIndex = HashFunction(key) % Buckets;
        var bucket = _buckets[hashIndex];
        ref var entry = ref bucket.TryFind(key, out bool found);
        return found ? entry.Value : 0;
    }

    public void Clear()
    {
        for (int i = 0; i < Buckets; i++)
            _buckets[i].Clear();
        ArrayPool<HashBucket>.Shared.Return(_buckets);
    }
}