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
            if (!_entries[i % Capacity].Used)
            {
                _entries[i % Capacity].Key = key;
                _entries[i % Capacity].Used = true;
                _entries[i % Capacity].Value = 1;
                return;
            }
            else if( _entries[i % Capacity].Key == key)
            {
                _entries[i % Capacity].Value ++;
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
            if (_entries[i % Capacity].Key == key) 
                return _entries[i % Capacity].Value;
        }

        return 0;
    }

    public void Clear()
    {
        ArrayPool<HashTableEntry>.Shared.Return(_entries); 
    }
}