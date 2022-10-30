# GenericPriorityQueue&lt;TItem, TPriority&gt;

Namespace: Priority_Queue

A copy of StablePriorityQueue which also has generic priority-type

```csharp
public sealed class GenericPriorityQueue<TItem, TPriority> : IFixedSizePriorityQueue`2, IPriorityQueue`2, , System.Collections.IEnumerable
```

#### Type Parameters

`TItem`<br>
The values in the queue.  Must extend the GenericPriorityQueueNode class

`TPriority`<br>
The priority-type.  Must extend IComparable<TPriority>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [GenericPriorityQueue&lt;TItem, TPriority&gt;](./priority_queue.genericpriorityqueue-2.md)<br>
Implements IFixedSizePriorityQueue&lt;TItem, TPriority&gt;, IPriorityQueue&lt;TItem, TPriority&gt;, IEnumerable&lt;TItem&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **Count**

Returns the number of nodes in the queue.
 O(1)

```csharp
public int Count { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MaxSize**

Returns the maximum number of items that can be enqueued at once in this queue. Once you hit this number (ie. once Count == MaxSize),
 attempting to enqueue another item will cause undefined behavior. O(1)

```csharp
public int MaxSize { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **First**

Returns the head of the queue, without removing it (use Dequeue() for that).
 If the queue is empty, behavior is undefined.
 O(1)

```csharp
public TItem First { get; }
```

#### Property Value

TItem<br>

## Constructors

### **GenericPriorityQueue(Int32)**

Instantiate a new Priority Queue

```csharp
public GenericPriorityQueue(int maxNodes)
```

#### Parameters

`maxNodes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)

### **GenericPriorityQueue(Int32, IComparer&lt;TPriority&gt;)**

Instantiate a new Priority Queue

```csharp
public GenericPriorityQueue(int maxNodes, IComparer<TPriority> comparer)
```

#### Parameters

`maxNodes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)

`comparer` IComparer&lt;TPriority&gt;<br>
The comparer used to compare TPriority values.

### **GenericPriorityQueue(Int32, Comparison&lt;TPriority&gt;)**

Instantiate a new Priority Queue

```csharp
public GenericPriorityQueue(int maxNodes, Comparison<TPriority> comparer)
```

#### Parameters

`maxNodes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)

`comparer` Comparison&lt;TPriority&gt;<br>
The comparison function to use to compare TPriority values

## Methods

### **Clear()**

Removes every node from the queue.
 O(n) (So, don't do this often!)

```csharp
public void Clear()
```

### **Contains(TItem)**

Returns (in O(1)!) whether the given node is in the queue.
 If node is or has been previously added to another queue, the result is undefined unless oldQueue.ResetNode(node) has been called
 O(1)

```csharp
public bool Contains(TItem node)
```

#### Parameters

`node` TItem<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Enqueue(TItem, TPriority)**

Enqueue a node to the priority queue. Lower values are placed in front. Ties are broken by first-in-first-out.
 If the queue is full, the result is undefined.
 If the node is already enqueued, the result is undefined.
 If node is or has been previously added to another queue, the result is undefined unless oldQueue.ResetNode(node) has been called
 O(log n)

```csharp
public void Enqueue(TItem node, TPriority priority)
```

#### Parameters

`node` TItem<br>

`priority` TPriority<br>

### **Dequeue()**

Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.
 If queue is empty, result is undefined
 O(log n)

```csharp
public TItem Dequeue()
```

#### Returns

TItem<br>

### **Resize(Int32)**

Resize the queue so it can accept more nodes. All currently enqueued nodes are remain.
 Attempting to decrease the queue size to a size too small to hold the existing nodes results in undefined behavior
 O(n)

```csharp
public void Resize(int maxNodes)
```

#### Parameters

`maxNodes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UpdatePriority(TItem, TPriority)**

This method must be called on a node every time its priority changes while it is in the queue. 
 Forgetting to call this method will result in a corrupted queue!
 Calling this method on a node not in the queue results in undefined behavior
 O(log n)

```csharp
public void UpdatePriority(TItem node, TPriority priority)
```

#### Parameters

`node` TItem<br>

`priority` TPriority<br>

### **Remove(TItem)**

Removes a node from the queue. The node does not need to be the head of the queue. 
 If the node is not in the queue, the result is undefined. If unsure, check Contains() first
 O(log n)

```csharp
public void Remove(TItem node)
```

#### Parameters

`node` TItem<br>

### **ResetNode(TItem)**

By default, nodes that have been previously added to one queue cannot be added to another queue.
 If you need to do this, please call originalQueue.ResetNode(node) before attempting to add it in the new queue

```csharp
public void ResetNode(TItem node)
```

#### Parameters

`node` TItem<br>

### **GetEnumerator()**

```csharp
public IEnumerator<TItem> GetEnumerator()
```

#### Returns

IEnumerator&lt;TItem&gt;<br>

### **IsValidQueue()**

Should not be called in production code.
 Checks to make sure the queue is still in a valid state. Used for testing/debugging the queue.

```csharp
public bool IsValidQueue()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
