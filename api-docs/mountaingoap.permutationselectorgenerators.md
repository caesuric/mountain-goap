# PermutationSelectorGenerators

Namespace: MountainGoap

Generators for default permutation selectors for convenience.

```csharp
public static class PermutationSelectorGenerators
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PermutationSelectorGenerators](./mountaingoap.permutationselectorgenerators.md)

## Methods

### **SelectFromCollection&lt;T&gt;(IEnumerable&lt;T&gt;)**

Generates a permutation selector that returns all elements of an enumerable.

```csharp
public static PermutationSelectorCallback SelectFromCollection<T>(IEnumerable<T> values)
```

#### Type Parameters

`T`<br>
Type of the .

#### Parameters

`values` IEnumerable&lt;T&gt;<br>
Set of values to be included in permutations.

#### Returns

[PermutationSelectorCallback](./mountaingoap.permutationselectorcallback.md)<br>
A lambda function that returns all elements from the collection passed in.

### **SelectFromCollectionInState&lt;T&gt;(String)**

Generates a permutation selector that returns all elements of an enumerable within the agent state.

```csharp
public static PermutationSelectorCallback SelectFromCollectionInState<T>(string key)
```

#### Type Parameters

`T`<br>
Type of the .

#### Parameters

`key` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Key of the state to check for the collection.

#### Returns

[PermutationSelectorCallback](./mountaingoap.permutationselectorcallback.md)<br>
A lambda function that returns all elements from the collection in the state.

### **SelectFromIntegerRange(Int32, Int32)**

Generates a permutation selector that returns all integer elements in a range.

```csharp
public static PermutationSelectorCallback SelectFromIntegerRange(int lowerBound, int upperBound)
```

#### Parameters

`lowerBound` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Lower bound from which to start.

`upperBound` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Upper bound, non-inclusive.

#### Returns

[PermutationSelectorCallback](./mountaingoap.permutationselectorcallback.md)<br>
A lambda function that returns all elements in the range given.
