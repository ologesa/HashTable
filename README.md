# HashTable
A project to study different strategies to implement an efficient HasTable

# Toy problem
Our target is to insert the entire works of Shakespeare into a hash table and then search for a specific phrase,
such as "To be or not to be that is the question."  

# Options to handle collisions
1. Separate Chaining (Closed Addressing). This strategy uses a list to handle collisions. Each bucket in the hash table 
contains a list of key-value pairs whose keys hash to the same index.
2. Linear Probing.

# Hash Functions
1. DotNet Hash Code (https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-readonlyspan((system-char))))
2. FNV-1a (https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function)
3. DJB2 (https://theartincode.stanis.me/008-djb2/)

# Results


| Method             | HashType | HashTableAlgorithmType | Mean     | Error    | StdDev   | Allocated |
|------------------- |--------- |----------------------- |---------:|---------:|---------:|----------:|
| HashTableBenchmark | DotNet   | ClosedAddressing       | 79.81 ms | 0.718 ms | 0.600 ms |         - |
| HashTableBenchmark | DotNet   | OpenAddressingLP       | 33.38 ms | 0.665 ms | 1.249 ms |         - |
| HashTableBenchmark | Djb2     | ClosedAddressing       | 74.95 ms | 1.091 ms | 1.021 ms |         - |
| HashTableBenchmark | Djb2     | OpenAddressingLP       | 30.44 ms | 0.597 ms | 0.529 ms |         - |
| HashTableBenchmark | Fnv      | ClosedAddressing       | 77.90 ms | 1.115 ms | 0.988 ms |         - |
| HashTableBenchmark | Fnv      | OpenAddressingLP       | 30.66 ms | 0.536 ms | 0.502 ms |         - |

So far OpenAddressingLP + Djb2 function is the best. 

# References
https://en.wikipedia.org/wiki/Category:Hashing