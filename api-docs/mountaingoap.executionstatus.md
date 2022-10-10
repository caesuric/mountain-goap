# ExecutionStatus

Namespace: MountainGoap

Possible execution status for an action.

```csharp
public enum ExecutionStatus
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [ExecutionStatus](./mountaingoap.executionstatus.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| NotYetExecuted | 1 | Indicates that the action is not currently executing. |
| Executing | 2 | Indicates that the action is currently executing. |
| Succeeded | 3 | Indicates that the action has succeeded. |
| Failed | 4 | Indicates that the action has failed. |
| NotPossible | 5 | Indicates that the action is not possible due to preconditions. |
