using System.Buffers;
using HashTable.HashFunctions;

namespace HashTable.OpenAddressingLinearProbing;

public sealed class OpenAddressingLinearProbing: IHashTable
{
    private const int Capacity = 50000;
    private readonly HashTableEntry[] _entries;
    public HashFunction HashFunction { get; set; } = DotNetHash.Hash; // just a default

    public OpenAddressingLinearProbing()
    {
        _entries =  ArrayPool<HashTableEntry>.Shared.Rent(Capacity);
        for(var i = 0; i < Capacity; i++)
        {
            _entries[i].Used = false;
        }
    }
    
    public void Add(string key)
    {
        uint hashIndex = HashFunction(key) % Capacity;
        for (uint i = hashIndex; i < Capacity + hashIndex; i++)
        {
            ref var entry = ref _entries[i % Capacity];
            if (!entry.Used)
            {
                entry.Key = key;
                entry.Used = true;
                entry.Value = 1;
                return;
            }
            if( entry.Key == key)
            {
                entry.Value ++;
                return;
            }
        }
        throw new HashTableException("HashTable is full");
    }

    public int GetValue(string key)
    {
        uint hashIndex = HashFunction(key) % Capacity;
        for (uint i = hashIndex; i < Capacity + hashIndex; i++)
        {
            ref var entry = ref _entries[i % Capacity];
            if (!entry.Used) 
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
}