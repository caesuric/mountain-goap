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

## Properties

### **StateCostDeltaMultiplier**

Gets or sets multiplier for delta value to provide delta cost.

```csharp
public StateCostDeltaMultiplierCallback StateCostDeltaMultiplier { get; set; }
```

#### Property Value

[StateCostDeltaMultiplierCallback](./mountaingoap.statecostdeltamultipliercallback.md)<br>

## Constructors

### **Action(String, Dictionary&lt;String, PermutationSelectorCallback&gt;, ExecutorCallback, Single, CostCallback, Dictionary&lt;String, Object&gt;, Dictionary&lt;String, ComparisonValuePair&gt;, Dictionary&lt;String, Object&gt;, Dictionary&lt;String, Object&gt;, Dictionary&lt;String, String&gt;, StateMutatorCallback, StateCheckerCallback, StateCostDeltaMultiplierCallback)**

Initializes a new instance of the [Action](./mountaingoap.action.md) class.

```csharp
public Action(string name, Dictionary<string, PermutationSelectorCallback> permutationSelectors, ExecutorCallback executor, float cost, CostCallback costCallback, Dictionary<string, object> preconditions, Dictionary<string, ComparisonValuePair> comparativePreconditions, Dictionary<string, object> postconditions, Dictionary<string, object> arithmeticPostconditions, Dictionary<string, string> parameterPostconditions, StateMutatorCallback stateMutator, StateCheckerCallback stateChecker, StateCostDeltaMultiplierCallback stateCostDeltaMultiplier)
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

`costCallback` [CostCallback](./mountaingoap.costcallback.md)<br>
Callback for determining the cost of the action.

`preconditions` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Preconditions required in the world state in order for the action to occur.

`comparativePreconditions` [Dictionary&lt;String, ComparisonValuePair&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Preconditions indicating relative value requirements needed for the action to occur.

`postconditions` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Postconditions applied after the action is successfully executed.

`arithmeticPostconditions` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Arithmetic postconditions added to state after the action is successfully executed.

`parameterPostconditions` [Dictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Parameter postconditions copied to state after the action is successfully executed.

`stateMutator` [StateMutatorCallback](./mountaingoap.statemutatorcallback.md)<br>
Callback for modifying state after action execution or evaluation.

`stateChecker` [StateCheckerCallback](./mountaingoap.statecheckercallback.md)<br>
Callback for checking state before action execution or evaluation.

`stateCostDeltaMultiplier` [StateCostDeltaMultiplierCallback](./mountaingoap.statecostdeltamultipliercallback.md)<br>
Callback for multiplier for delta value to provide delta cost.

## Methods

### **DefaultStateCostDeltaMultiplier(Action, String)**

```csharp
public static float DefaultStateCostDeltaMultiplier(Action action, string stateKey)
```

#### Parameters

`action` [Action](./mountaingoap.action.md)<br>

`stateKey` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Copy()**

Makes a copy of the action.

```csharp
public Action Copy()
```

#### Returns

[Action](./mountaingoap.action.md)<br>
A copy of the action.

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

### **GetCost(ConcurrentDictionary&lt;String, Object&gt;)**

Gets the cost of the action.

```csharp
public float GetCost(ConcurrentDictionary<string, object> currentState)
```

#### Parameters

`currentState` ConcurrentDictionary&lt;String, Object&gt;<br>
State as it will be when cost is relevant.

#### Returns

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
The cost of the action.

### **Execute(Agent)**

Executes a step of work for the agent.

```csharp
internal ExecutionStatus Execute(Agent agent)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent executing the action.

#### Returns

[ExecutionStatus](./mountaingoap.executionstatus.md)<br>
The execution status of the action.

### **IsPossible(ConcurrentDictionary&lt;String, Object&gt;)**

Determines whether or not an action is possible.

```csharp
internal bool IsPossible(ConcurrentDictionary<string, object> state)
```

#### Parameters

`state` ConcurrentDictionary&lt;String, Object&gt;<br>
The current world state.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the action is possible, otherwise false.

### **GetPermutations(ConcurrentDictionary&lt;String, Object&gt;)**

Gets all permutations of parameters possible for an action.

```csharp
internal List<Dictionary<string, object>> GetPermutations(ConcurrentDictionary<string, object> state)
```

#### Parameters

`state` ConcurrentDictionary&lt;String, Object&gt;<br>
World state when the action would be performed.

#### Returns

[List&lt;Dictionary&lt;String, Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of possible parameter dictionaries that could be used.

### **ApplyEffects(ConcurrentDictionary&lt;String, Object&gt;)**

Applies the effects of the action.

```csharp
internal void ApplyEffects(ConcurrentDictionary<string, object> state)
```

#### Parameters

`state` ConcurrentDictionary&lt;String, Object&gt;<br>
World state to which to apply effects.

### **SetParameters(Dictionary&lt;String, Object&gt;)**

Sets all parameters to the action.

```csharp
internal void SetParameters(Dictionary<string, object> parameters)
```

#### Parameters

`parameters` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Dictionary of parameters to be passed to the action.

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
