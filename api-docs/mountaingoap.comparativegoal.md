# ComparativeGoal

Namespace: MountainGoap

Represents a goal to be achieved for an agent.

```csharp
public class ComparativeGoal : BaseGoal
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BaseGoal](./mountaingoap.basegoal.md) → [ComparativeGoal](./mountaingoap.comparativegoal.md)

## Fields

### **Name**

Name of the goal.

```csharp
public string Name;
```

## Constructors

### **ComparativeGoal(String, Single, Dictionary&lt;String, ComparisonValuePair&gt;)**

Initializes a new instance of the [ComparativeGoal](./mountaingoap.comparativegoal.md) class.

```csharp
public ComparativeGoal(string name, float weight, Dictionary<string, ComparisonValuePair> desiredState)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the goal.

`weight` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Weight to give the goal.

`desiredState` [Dictionary&lt;String, ComparisonValuePair&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Desired state for the comparative goal.
