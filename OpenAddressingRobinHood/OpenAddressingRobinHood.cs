using System.Buffers;
using HashTable.HashFunctions;

namespace HashTable.OpenAddressingRobinHood;

public sealed class OpenAddressingRobinHood : IHashTable
{
    private const int Capacity = 40000;
    private int _count = 0;
    private readonly HashTableEntry[] _entries;
    public HashFunction HashFunction { get; set; } = DotNetHash.Hash; // just a default

    public OpenAddressingRobinHood()
    {
        _entries = ArrayPool<HashTableEntry>.Shared.Rent(Capacity);
        for (var i = 0; i < Capacity; i++)
        {
            _entries[i].Used = false;
        }
    }

    public void Add(string key)
    {
        uint hashIndex = HashFunction(key) % Capacity;
        uint distance = 0;
        int value = 1;
        for (uint i = hashIndex; i < Capacity + hashIndex; i++, distance++)
        {
            ref var entry = ref _entries[i % Capacity];
            if (!entry.Used)
            {
                entry.Key = key;
                entry.Used = true;
                entry.Value = value;
                entry.Distance = distance;
                _count++;
                return;
            }

            if (entry.Key == key)
            {
                entry.Value++;
                return;
            }

            if (distance > entry.Distance)
            {
                (entry.Key, key) = (key, entry.Key);
                (entry.Value, value) = (value, entry.Value);
                (entry.Distance, distance) = (distance, entry.Distance);
            }
        }

        throw new HashTableException("HashTable is full");
    }

    public int GetValue(string key)
    {
        uint hashIndex = HashFunction(key) % Capacity;
        for (uint i = hashIndex, distance = 0 ; i < Capacity + hashIndex; i++, distance++)
        {
            ref var entry = ref _entries[i % Capacity];
            if (!entry.Used)
                return 0;
            if(distance > entry.Distance)
                return 0;
            if (entry.Key == key)
                return entry.Value;
        }

        return 0;
    }

    public void Dispose()
    {
        ArrayPool<HashTableEntry>.Shared.Return(_entries);
    }

    public double GetLoadFactor()
    {
        return _count / (double)Capacity;
    }
}