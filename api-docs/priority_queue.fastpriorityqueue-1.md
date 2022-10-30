# FastPriorityQueue&lt;T&gt;

Namespace: Priority_Queue

An implementation of a min-Priority Queue using a heap. Has O(1) .Contains()!
 See https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Getting-Started for more information

```csharp
public sealed class FastPriorityQueue<T> : IFixedSizePriorityQueue`2, IPriorityQueue`2, , System.Collections.IEnumerable
```

#### Type Parameters

`T`<br>
The values in the queue.  Must extend the FastPriorityQueueNode class

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FastPriorityQueue&lt;T&gt;](./priority_queue.fastpriorityqueue-1.md)<br>
Implements IFixedSizePriorityQueue&lt;T, Single&gt;, IPriorityQueue&lt;T, Single&gt;, IEnumerable&lt;T&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

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
public T First { get; }
```

#### Property Value

T<br>

## Constructors

### **FastPriorityQueue(Int32)**

Instantiate a new Priority Queue

```csharp
public FastPriorityQueue(int maxNodes)
```

#### Parameters

`maxNodes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)

## Methods

### **Clear()**

Removes every node from the queue.
 O(n) (So, don't do this often!)

```csharp
public void Clear()
```

### **Contains(T)**

Returns (in O(1)!) whether the given node is in the queue.
 If node is or has been previously added to another queue, the result is undefined unless oldQueue.ResetNode(node) has been called
 O(1)

```csharp
public bool Contains(T node)
```

#### Parameters

`node` T<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Enqueue(T, Single)**

Enqueue a node to the priority queue. Lower values are placed in front. Ties are broken arbitrarily.
 If the queue is full, the result is undefined.
 If the node is already enqueued, the result is undefined.
 If node is or has been previously added to another queue, the result is undefined unless oldQueue.ResetNode(node) has been called
 O(log n)

```csharp
public void Enqueue(T node, float priority)
```

#### Parameters

`node` T<br>

`priority` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Dequeue()**

Removes the head of the queue and returns it.
 If queue is empty, result is undefined
 O(log n)

```csharp
public T Dequeue()
```

#### Returns

T<br>

### **Resize(Int32)**

Resize the queue so it can accept more nodes. All currently enqueued nodes are remain.
 Attempting to decrease the queue size to a size too small to hold the existing nodes results in undefined behavior
 O(n)

```csharp
public void Resize(int maxNodes)
```

#### Parameters

`maxNodes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UpdatePriority(T, Single)**

This method must be called on a node every time its priority changes while it is in the queue. 
 Forgetting to call this method will result in a corrupted queue!
 Calling this method on a node not in the queue results in undefined behavior
 O(log n)

```csharp
public void UpdatePriority(T node, float priority)
```

#### Parameters

`node` T<br>

`priority` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Remove(T)**

Removes a node from the queue. The node does not need to be the head of the queue. 
 If the node is not in the queue, the result is undefined. If unsure, check Contains() first
 O(log n)

```csharp
public void Remove(T node)
```

#### Parameters

`node` T<br>

### **ResetNode(T)**

By default, nodes that have been previously added to one queue cannot be added to another queue.
 If you need to do this, please call originalQueue.ResetNode(node) before attempting to add it in the new queue
 If the node is currently in the queue or belongs to another queue, the result is undefined

```csharp
public void ResetNode(T node)
```

#### Parameters

`node` T<br>

### **GetEnumerator()**

```csharp
public IEnumerator<T> GetEnumerator()
```

#### Returns

IEnumerator&lt;T&gt;<br>

### **IsValidQueue()**

Should not be called in production code.
 Checks to make sure the queue is still in a valid state. Used for testing/debugging the queue.

```csharp
public bool IsValidQueue()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
