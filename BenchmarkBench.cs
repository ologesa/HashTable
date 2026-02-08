using BenchmarkDotNet.Attributes;
using HashTable.ClosedAddressing;
using HashTable.Content;
using HashTable.HashFunctions;

namespace HashTable;

[MemoryDiagnoser]
public class BenchmarkBench
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private CloseAddressingHashTable _closedAddressingDotNetHash;
    private CloseAddressingHashTable _closedAddressingDJb2Hash;
    private CloseAddressingHashTable _closedAddressingFnvHash;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private readonly List<string> _words = [];

    [GlobalSetup]
    public void GlobalSetup()
    {
        var content = File.ReadAllText("Content/Shakespeare.txt");

        Tokenizer.ProcessTokens(content, token => { _words.Add(token.ToString()); });
    }

    [IterationSetup]
    public void Setup()
    {
        _closedAddressingDotNetHash = new CloseAddressingHashTable { HashFunction = DotNetHash.Hash };
        _closedAddressingDJb2Hash = new CloseAddressingHashTable { HashFunction = Djb2Hash.Hash };
        _closedAddressingFnvHash = new CloseAddressingHashTable { HashFunction = FnvHash.Hash };
    }

    [IterationCleanup]
    public void Cleanup()
    {
        _closedAddressingDotNetHash.Clear();
        _closedAddressingDJb2Hash.Clear();
        _closedAddressingFnvHash.Clear();
    }

    [Benchmark]
    public void ClosedAddressingDotNetHash() => DoTheRoutine(_closedAddressingDotNetHash, TextWriter.Null);

    [Benchmark]
    public void ClosedAddressingDjb2Hash() => DoTheRoutine(_closedAddressingDJb2Hash, TextWriter.Null);

    [Benchmark]
    public void ClosedAddressingFnvHash() => DoTheRoutine(_closedAddressingFnvHash, TextWriter.Null);


    void DoTheRoutine(IHashTable hashTable, TextWriter tw)
    {
        foreach (var word in _words)
        {
            hashTable.Add(word);
        }

        for (int i = 0; i < 50_000; i++)
        {
            tw.WriteLine( hashTable.GetValue("To"));
            tw.WriteLine( hashTable.GetValue("be"));
            tw.WriteLine( hashTable.GetValue("or"));
            tw.WriteLine( hashTable.GetValue("not"));
            tw.WriteLine( hashTable.GetValue("to"));
            tw.WriteLine( hashTable.GetValue("be"));
            tw.WriteLine( hashTable.GetValue("that"));
            tw.WriteLine( hashTable.GetValue("is"));
            tw.WriteLine( hashTable.GetValue("the"));
            tw.WriteLine( hashTable.GetValue("question"));
        }
    }
}