# Goal

Namespace: MountainGoap

Represents a goal to be achieved for an agent.

```csharp
public class Goal
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Goal](./mountaingoap.goal.md)

## Fields

### **Name**

Name of the goal.

```csharp
public string Name;
```

## Constructors

### **Goal(String, Single, Dictionary&lt;String, Object&gt;)**

Initializes a new instance of the [Goal](./mountaingoap.goal.md) class.

```csharp
public Goal(string name, float weight, Dictionary<string, object> desiredState)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the goal.

`weight` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Weight to give the goal.

`desiredState` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Desired end state of the goal.
