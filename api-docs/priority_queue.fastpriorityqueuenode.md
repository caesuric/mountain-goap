# FastPriorityQueueNode

Namespace: Priority_Queue

```csharp
public class FastPriorityQueueNode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FastPriorityQueueNode](./priority_queue.fastpriorityqueuenode.md)

## Properties

### **Priority**

The Priority to insert this node at.
 Cannot be manually edited - see queue.Enqueue() and queue.UpdatePriority() instead

```csharp
public float Priority { get; protected internal set; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **QueueIndex**

Represents the current position in the queue

```csharp
public int QueueIndex { get; internal set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **FastPriorityQueueNode()**

```csharp
public FastPriorityQueueNode()
```
