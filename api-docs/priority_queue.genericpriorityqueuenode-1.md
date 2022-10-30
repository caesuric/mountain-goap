# GenericPriorityQueueNode&lt;TPriority&gt;

Namespace: Priority_Queue

```csharp
public class GenericPriorityQueueNode<TPriority>
```

#### Type Parameters

`TPriority`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [GenericPriorityQueueNode&lt;TPriority&gt;](./priority_queue.genericpriorityqueuenode-1.md)

## Properties

### **Priority**

The Priority to insert this node at.
 Cannot be manually edited - see queue.Enqueue() and queue.UpdatePriority() instead

```csharp
public TPriority Priority { get; protected internal set; }
```

#### Property Value

TPriority<br>

### **QueueIndex**

Represents the current position in the queue

```csharp
public int QueueIndex { get; internal set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **InsertionIndex**

Represents the order the node was inserted in

```csharp
public long InsertionIndex { get; internal set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

## Constructors

### **GenericPriorityQueueNode()**

```csharp
public GenericPriorityQueueNode()
```
