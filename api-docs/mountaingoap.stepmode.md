# StepMode

Namespace: MountainGoap

Different modes with which MountainGoap can execute an agent step.

```csharp
public enum StepMode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [StepMode](./mountaingoap.stepmode.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Default | 1 | Default step mode. Runs async, doesn't necessitate taking action. |
| OneAction | 2 | Turn-based step mode, plans synchronously, executes at least one action if possible. |
| AllActions | 3 | Turn-based step mode, plans synchronously, executes all actions in planned action sequence. |
