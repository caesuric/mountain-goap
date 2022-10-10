# Action

Namespace: MountainGoap

Represents an action in a GOAP system.

```csharp
public class Action
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Action](./mountaingoap.action.md)

## Fields

### **Name**

Name of the action.

```csharp
public string Name;
```

### **Cost**

Cost of the action.

```csharp
public float Cost;
```

## Constructors

### **Action(String, Dictionary&lt;String, PermutationSelectorCallback&gt;, ExecutorCallback, Single, Dictionary&lt;String, Object&gt;, Dictionary&lt;String, Object&gt;)**

Initializes a new instance of the [Action](./mountaingoap.action.md) class.

```csharp
public Action(string name, Dictionary<string, PermutationSelectorCallback> permutationSelectors, ExecutorCallback executor, float cost, Dictionary<string, object> preconditions, Dictionary<string, object> postconditions)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name for the action, for eventing and logging purposes.

`permutationSelectors` [Dictionary&lt;String, PermutationSelectorCallback&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
The permutation selector callback for the action's parameters.

`executor` [ExecutorCallback](./mountaingoap.executorcallback.md)<br>
The executor callback for the action.

`cost` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Cost of the action.

`preconditions` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Preconditions required in the world state in order for the action to occur.

`postconditions` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Postconditions applied after the action is successfully executed.

## Methods

### **SetParameter(String, Object)**

Sets a parameter to the action.

```csharp
public void SetParameter(string key, object value)
```

#### Parameters

`key` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Key to be set.

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
Value to be set.

### **GetParameter(String)**

Gets a parameter to the action.

```csharp
public object GetParameter(string key)
```

#### Parameters

`key` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Key for the value to be retrieved.

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The value stored at the key specified.

### **Execute(Agent)**

Executes a step of work for the agent.

```csharp
internal void Execute(Agent agent)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent executing the action.

### **IsPossible(Dictionary&lt;String, Object&gt;)**

Determines whether or not an action is possible.

```csharp
internal bool IsPossible(Dictionary<string, object> state)
```

#### Parameters

`state` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
The current world state.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the action is possible, otherwise false.

### **GetPermutations(Agent)**

Gets all permutations of parameters possible for an action.

```csharp
internal List<Dictionary<string, object>> GetPermutations(Agent agent)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent for which the action would be performed.

#### Returns

[List&lt;Dictionary&lt;String, Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of possible parameter dictionaries that could be used.

### **ApplyEffects(Dictionary&lt;String, Object&gt;)**

Applies the effects of the action.

```csharp
internal void ApplyEffects(Dictionary<string, object> state)
```

#### Parameters

`state` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
World state to which to apply effects.

## Events

### **OnBeginExecuteAction**

Event that triggers when an action begins executing.

```csharp
public static event BeginExecuteActionEvent OnBeginExecuteAction;
```

### **OnFinishExecuteAction**

Event that triggers when an action finishes executing.

```csharp
public static event FinishExecuteActionEvent OnFinishExecuteAction;
```