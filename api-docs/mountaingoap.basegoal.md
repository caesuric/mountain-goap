# BaseGoal

Namespace: MountainGoap

Represents an abstract class for a goal to be achieved for an agent.

```csharp
public abstract class BaseGoal
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BaseGoal](./mountaingoap.basegoal.md)

## Fields

### **Name**

Name of the goal.

```csharp
public string Name;
```

## Constructors

### **BaseGoal(String, Single)**

Initializes a new instance of the [BaseGoal](./mountaingoap.basegoal.md) class.

```csharp
public BaseGoal(string name, float weight)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the goal.

`weight` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Weight to give the goal.
