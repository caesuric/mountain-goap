# SimplePriorityQueue&lt;TItem, TPriority&gt;

Namespace: Priority_Queue

A simplified priority queue implementation. Is stable, auto-resizes, and thread-safe, at the cost of being slightly slower than
 FastPriorityQueue
 Methods tagged as O(1) or O(log n) are assuming there are no duplicates. Duplicates may increase the algorithmic complexity.

```csharp
public class SimplePriorityQueue<TItem, TPriority> : IPriorityQueue`2, , System.Collections.IEnumerable
```

#### Type Parameters

`TItem`<br>
The type to enqueue

`TPriority`<br>
The priority-type to use for nodes.  Must extend IComparable<TPriority>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SimplePriorityQueue&lt;TItem, TPriority&gt;](./priority_queue.simplepriorityqueue-2.md)<br>
Implements IPriorityQueue&lt;TItem, TPriority&gt;, IEnumerable&lt;TItem&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **Count**

Returns the number of nodes in the queue.
 O(1)

```csharp
public int Count { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **First**

Returns the head of the queue, without removing it (use Dequeue() for that).
 Throws an exception when the queue is empty.
 O(1)

```csharp
public TItem First { get; }
```

#### Property Value

TItem<br>

## Constructors

### **SimplePriorityQueue()**

Instantiate a new Priority Queue

```csharp
public SimplePriorityQueue()
```

### **SimplePriorityQueue(IComparer&lt;TPriority&gt;)**

Instantiate a new Priority Queue

```csharp
public SimplePriorityQueue(IComparer<TPriority> priorityComparer)
```

#### Parameters

`priorityComparer` IComparer&lt;TPriority&gt;<br>
The comparer used to compare TPriority values.  Defaults to Comparer<TPriority>.default

### **SimplePriorityQueue(Comparison&lt;TPriority&gt;)**

Instantiate a new Priority Queue

```csharp
public SimplePriorityQueue(Comparison<TPriority> priorityComparer)
```

#### Parameters

`priorityComparer` Comparison&lt;TPriority&gt;<br>
The comparison function to use to compare TPriority values

### **SimplePriorityQueue(IEqualityComparer&lt;TItem&gt;)**

Instantiate a new Priority Queue

```csharp
public SimplePriorityQueue(IEqualityComparer<TItem> itemEquality)
```

#### Parameters

`itemEquality` IEqualityComparer&lt;TItem&gt;<br>
The equality comparison function to use to compare TItem values

### **SimplePriorityQueue(IComparer&lt;TPriority&gt;, IEqualityComparer&lt;TItem&gt;)**

Instantiate a new Priority Queue

```csharp
public SimplePriorityQueue(IComparer<TPriority> priorityComparer, IEqualityComparer<TItem> itemEquality)
```

#### Parameters

`priorityComparer` IComparer&lt;TPriority&gt;<br>
The comparer used to compare TPriority values.  Defaults to Comparer<TPriority>.default

`itemEquality` IEqualityComparer&lt;TItem&gt;<br>
The equality comparison function to use to compare TItem values

### **SimplePriorityQueue(Comparison&lt;TPriority&gt;, IEqualityComparer&lt;TItem&gt;)**

Instantiate a new Priority Queue

```csharp
public SimplePriorityQueue(Comparison<TPriority> priorityComparer, IEqualityComparer<TItem> itemEquality)
```

#### Parameters

`priorityComparer` Comparison&lt;TPriority&gt;<br>
The comparison function to use to compare TPriority values

`itemEquality` IEqualityComparer&lt;TItem&gt;<br>
The equality comparison function to use to compare TItem values

## Methods

### **Clear()**

Removes every node from the queue.
 O(n)

```csharp
public void Clear()
```

### **Contains(TItem)**

Returns whether the given item is in the queue.
 O(1)

```csharp
public bool Contains(TItem item)
```

#### Parameters

`item` TItem<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Dequeue()**

Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.
 If queue is empty, throws an exception
 O(log n)

```csharp
public TItem Dequeue()
```

#### Returns

TItem<br>

### **Enqueue(TItem, TPriority)**

Enqueue a node to the priority queue. Lower values are placed in front. Ties are broken by first-in-first-out.
 This queue automatically resizes itself, so there's no concern of the queue becoming 'full'.
 Duplicates and null-values are allowed.
 O(log n)

```csharp
public void Enqueue(TItem item, TPriority priority)
```

#### Parameters

`item` TItem<br>

`priority` TPriority<br>

### **EnqueueWithoutDuplicates(TItem, TPriority)**

Enqueue a node to the priority queue if it doesn't already exist. Lower values are placed in front. Ties are broken by first-in-first-out.
 This queue automatically resizes itself, so there's no concern of the queue becoming 'full'. Null values are allowed.
 Returns true if the node was successfully enqueued; false if it already exists.
 O(log n)

```csharp
public bool EnqueueWithoutDuplicates(TItem item, TPriority priority)
```

#### Parameters

`item` TItem<br>

`priority` TPriority<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Remove(TItem)**

Removes an item from the queue. The item does not need to be the head of the queue. 
 If the item is not in the queue, an exception is thrown. If unsure, check Contains() first.
 If multiple copies of the item are enqueued, only the first one is removed. 
 O(log n)

```csharp
public void Remove(TItem item)
```

#### Parameters

`item` TItem<br>

### **UpdatePriority(TItem, TPriority)**

Call this method to change the priority of an item.
 Calling this method on a item not in the queue will throw an exception.
 If the item is enqueued multiple times, only the first one will be updated.
 (If your requirements are complex enough that you need to enqueue the same item multiple times and be able
 to update all of them, please wrap your items in a wrapper class so they can be distinguished).
 O(log n)

```csharp
public void UpdatePriority(TItem item, TPriority priority)
```

#### Parameters

`item` TItem<br>

`priority` TPriority<br>

### **GetPriority(TItem)**

Returns the priority of the given item.
 Calling this method on a item not in the queue will throw an exception.
 If the item is enqueued multiple times, only the priority of the first will be returned.
 (If your requirements are complex enough that you need to enqueue the same item multiple times and be able
 to query all their priorities, please wrap your items in a wrapper class so they can be distinguished).
 O(1)

```csharp
public TPriority GetPriority(TItem item)
```

#### Parameters

`item` TItem<br>

#### Returns

TPriority<br>

### **TryFirst(TItem&)**

```csharp
public bool TryFirst(TItem& first)
```

#### Parameters

`first` TItem&<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TryDequeue(TItem&)**

Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and sets it to first.
 Useful for multi-threading, where the queue may become empty between calls to Contains() and Dequeue()
 Returns true if successful; false if queue was empty
 O(log n)

```csharp
public bool TryDequeue(TItem& first)
```

#### Parameters

`first` TItem&<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TryRemove(TItem)**

Attempts to remove an item from the queue. The item does not need to be the head of the queue. 
 Useful for multi-threading, where the queue may become empty between calls to Contains() and Remove()
 Returns true if the item was successfully removed, false if it wasn't in the queue.
 If multiple copies of the item are enqueued, only the first one is removed. 
 O(log n)

```csharp
public bool TryRemove(TItem item)
```

#### Parameters

`item` TItem<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TryUpdatePriority(TItem, TPriority)**

Call this method to change the priority of an item.
 Useful for multi-threading, where the queue may become empty between calls to Contains() and UpdatePriority()
 If the item is enqueued multiple times, only the first one will be updated.
 (If your requirements are complex enough that you need to enqueue the same item multiple times and be able
 to update all of them, please wrap your items in a wrapper class so they can be distinguished).
 Returns true if the item priority was updated, false otherwise.
 O(log n)

```csharp
public bool TryUpdatePriority(TItem item, TPriority priority)
```

#### Parameters

`item` TItem<br>

`priority` TPriority<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TryGetPriority(TItem, TPriority&)**

Attempt to get the priority of the given item.
 Useful for multi-threading, where the queue may become empty between calls to Contains() and GetPriority()
 If the item is enqueued multiple times, only the priority of the first will be returned.
 (If your requirements are complex enough that you need to enqueue the same item multiple times and be able
 to query all their priorities, please wrap your items in a wrapper class so they can be distinguished).
 Returns true if the item was found in the queue, false otherwise
 O(1)

```csharp
public bool TryGetPriority(TItem item, TPriority& priority)
```

#### Parameters

`item` TItem<br>

`priority` TPriority&<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetEnumerator()**

```csharp
public IEnumerator<TItem> GetEnumerator()
```

#### Returns

IEnumerator&lt;TItem&gt;<br>

### **IsValidQueue()**

```csharp
public bool IsValidQueue()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
