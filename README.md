# HashTable
A project to study different strategies to implement an efficient HasTable

# Toy problem
Our target is to insert the entire works of Shakespeare into a hash table and then search for a specific phrase,
such as "To be or not to be that is the question."  

# Options to handle collisions
1. Separate Chaining (Closed Addressing). This strategy uses a list to handle collisions. Each bucket in the hash table 
contains a list of key-value pairs whose keys hash to the same index.
2. OpenAddressing Linear Probing (https://en.wikipedia.org/wiki/Open_addressing)
3. OpenAddressing Round Robin (https://cs.uwaterloo.ca/research/tr/1986/CS-86-14.pdf)


# Hash Functions
1. DotNet Hash Code (https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-readonlyspan((system-char))))
2. FNV-1a (https://en.wikipedia.org/wiki/Fowler%E2%80%93Noll%E2%80%93Vo_hash_function)
3. DJB2 (https://theartincode.stanis.me/008-djb2/)

# Results

| Method             | HashType | HashTableAlgorithmType | Mean     | Error    | StdDev   | Allocated |
|------------------- |--------- |----------------------- |---------:|---------:|---------:|----------:|
| HashTableBenchmark | Djb2     | ClosedAddressing       | 96.63 ms | 1.507 ms | 1.410 ms |         - |
| HashTableBenchmark | Djb2     | OpenAddressingLP       | 33.66 ms | 0.619 ms | 0.608 ms |         - |
| HashTableBenchmark | Djb2     | OpenAddressingRR       | 56.74 ms | 1.106 ms | 1.035 ms |         - |
| HashTableBenchmark | Fnv      | ClosedAddressing       | 98.55 ms | 1.330 ms | 1.179 ms |         - |
| HashTableBenchmark | Fnv      | OpenAddressingLP       | 33.99 ms | 0.648 ms | 0.636 ms |         - |
| HashTableBenchmark | Fnv      | OpenAddressingRR       | 44.32 ms | 0.730 ms | 0.683 ms |         - |

So far OpenAddressingLP + Djb2 function is the best. 

# References
https://en.wikipedia.org/wiki/Category:Hashing