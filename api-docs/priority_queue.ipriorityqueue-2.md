# IPriorityQueue&lt;TItem, TPriority&gt;

Namespace: Priority_Queue

The IPriorityQueue interface. This is mainly here for purists, and in case I decide to add more implementations later.
 For speed purposes, it is actually recommended that you *don't* access the priority queue through this interface, since the JIT can
 (theoretically?) optimize method calls from concrete-types slightly better.

```csharp
public interface IPriorityQueue<TItem, TPriority> : , System.Collections.IEnumerable
```

#### Type Parameters

`TItem`<br>

`TPriority`<br>

Implements IEnumerable&lt;TItem&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)

## Properties

### **First**

Returns the head of the queue, without removing it (use Dequeue() for that).

```csharp
public abstract TItem First { get; }
```

#### Property Value

TItem<br>

### **Count**

Returns the number of nodes in the queue.

```csharp
public abstract int Count { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **Enqueue(TItem, TPriority)**

Enqueue a node to the priority queue. Lower values are placed in front. Ties are broken by first-in-first-out.
 See implementation for how duplicates are handled.

```csharp
void Enqueue(TItem node, TPriority priority)
```

#### Parameters

`node` TItem<br>

`priority` TPriority<br>

### **Dequeue()**

Removes the head of the queue (node with minimum priority; ties are broken by order of insertion), and returns it.

```csharp
TItem Dequeue()
```

#### Returns

TItem<br>

### **Clear()**

Removes every node from the queue.

```csharp
void Clear()
```

### **Contains(TItem)**

Returns whether the given node is in the queue.

```csharp
bool Contains(TItem node)
```

#### Parameters

`node` TItem<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Remove(TItem)**

Removes a node from the queue. The node does not need to be the head of the queue.

```csharp
void Remove(TItem node)
```

#### Parameters

`node` TItem<br>

### **UpdatePriority(TItem, TPriority)**

Call this method to change the priority of a node.

```csharp
void UpdatePriority(TItem node, TPriority priority)
```

#### Parameters

`node` TItem<br>

`priority` TPriority<br>
