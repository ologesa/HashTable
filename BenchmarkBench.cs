using BenchmarkDotNet.Attributes;
using HashTable.ClosedAddressing;
using HashTable.Content;
using HashTable.HashFunctions;
using JetBrains.Annotations;

namespace HashTable;
[MemoryDiagnoser]
public class BenchmarkBench
{
    private IHashTable _hashTable = null!;
    private readonly List<string> _words = [];

    [Params(HashKind.DotNet, HashKind.Djb2, HashKind.Fnv)]
    public HashKind HashType { get; [UsedImplicitly] set; }

    [Params(HashTableAlgorithmKind.ClosedAddressing)]
    public HashTableAlgorithmKind HashTableAlgorithmType { get; [UsedImplicitly] set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var content = File.ReadAllText("Content/Shakespeare.txt");
        Tokenizer.ProcessTokens(content, token => _words.Add(token.ToString()));
    }

    [IterationSetup]
    public void Setup()
    {
        _hashTable = CreateTable(HashTableAlgorithmType, HashType);
    }

    [Benchmark]
    public void HashTableBenchmark()
        => DoTheRoutine(_hashTable, TextWriter.Null);

    private static IHashTable CreateTable(HashTableAlgorithmKind hashTableAlgorithmKind, HashKind hashKind)
    {
        var hashFunction = GetHashFunction(hashKind);

        return GetHashTableAlgorithm(hashTableAlgorithmKind, hashFunction);
    }

    private static IHashTable GetHashTableAlgorithm(HashTableAlgorithmKind hashTableAlgorithmKind, HashFunction hash)
    {
        return hashTableAlgorithmKind switch
        {
            HashTableAlgorithmKind.ClosedAddressing => new CloseAddressingHashTable { HashFunction = hash },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static HashFunction GetHashFunction(HashKind kind)
        => kind switch
        {
            HashKind.DotNet => DotNetHash.Hash,
            HashKind.Djb2   => Djb2Hash.Hash,
            HashKind.Fnv    => FnvHash.Hash,
            _ => throw new ArgumentOutOfRangeException()
        };

    private void DoTheRoutine(IHashTable hashTable, TextWriter tw)
    {
        foreach (string word in _words)
            hashTable.Add(word);

        for (var i = 0; i < 50_000; i++)
        {
            tw.WriteLine(hashTable.GetValue("To"));
            tw.WriteLine(hashTable.GetValue("be"));
            tw.WriteLine(hashTable.GetValue("or"));
            tw.WriteLine(hashTable.GetValue("not"));
            tw.WriteLine(hashTable.GetValue("to"));
            tw.WriteLine(hashTable.GetValue("be"));
            tw.WriteLine(hashTable.GetValue("that"));
            tw.WriteLine(hashTable.GetValue("is"));
            tw.WriteLine(hashTable.GetValue("the"));
            tw.WriteLine(hashTable.GetValue("question"));
        }
    }
}
