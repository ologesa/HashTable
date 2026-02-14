
using BenchmarkDotNet.Running;
using HashTable;
using HashTable.ClosedAddressing;
using HashTable.Content;
using HashTable.HashFunctions;
using HashTable.OpenAddressingLinearProbing;

//DoTheRoutine<CloseAddressingHashTable>( DotNetHash.Hash);
//DoTheRoutine<OpenAddressingLinearProbing>( DotNetHash.Hash);
BenchmarkRunner.Run<BenchmarkBench>();

return;


static void DoTheRoutine<T> (HashFunction hash )
    where T : IHashTable, new()
{
    IHashTable hashTable = new T() { HashFunction = hash };

    var content = File.ReadAllText("Content/Shakespeare.txt");
    
    
    
    Tokenizer.ProcessTokens(content, token =>
    {
        hashTable.Add(token.ToString());
    });

    Console.Out.WriteLine(hashTable.GetValue("To"));
    Console.Out.WriteLine(hashTable.GetValue("be"));
    Console.Out.WriteLine(hashTable.GetValue("or"));
    Console.Out.WriteLine(hashTable.GetValue("not"));
    Console.Out.WriteLine(hashTable.GetValue("to"));
    Console.Out.WriteLine(hashTable.GetValue("be"));
    hashTable.Clear();
}