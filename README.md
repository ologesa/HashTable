# HashTable
A project to study different strategies to implement and efficient HasTable

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

(so far only linear probing)

| Method                     | Mean     | Error    | StdDev   | Allocated |
|--------------------------- |---------:|---------:|---------:|----------:|
| ClosedAddressingDotNetHash | 82.39 ms | 0.736 ms | 0.652 ms |         - |
| ClosedAddressingDjb2Hash   | 76.43 ms | 1.459 ms | 3.524 ms |         - |
| ClosedAddressingFnvHash    | 78.91 ms | 1.127 ms | 0.999 ms |         - |



# References
https://en.wikipedia.org/wiki/Category:Hashing