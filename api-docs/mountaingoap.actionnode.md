# ActionNode

Namespace: MountainGoap

Represents an action node in an action graph.

```csharp
public class ActionNode : Priority_Queue.FastPriorityQueueNode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FastPriorityQueueNode](./priority_queue.fastpriorityqueuenode.md) → [ActionNode](./mountaingoap.actionnode.md)

## Properties

### **State**

Gets or sets the state of the world for this action node.

```csharp
public Dictionary<string, object> State { get; set; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

### **Parameters**

Gets or sets parameters to be passed to the action.

```csharp
public Dictionary<string, object> Parameters { get; set; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

### **Action**

Gets or sets the action to be executed when the world is in the defined [ActionNode.State](./mountaingoap.actionnode.md#state).

```csharp
public Action Action { get; set; }
```

#### Property Value

[Action](./mountaingoap.action.md)<br>

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

## Methods

### **Equals(Object)**

```csharp
public bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
public int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Cost(Dictionary&lt;String, Object&gt;)**

Cost to traverse this node.

```csharp
internal float Cost(Dictionary<string, object> currentState)
```

#### Parameters

`currentState` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Current state after previous node is executed.

#### Returns

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
The cost of the action to be executed.
