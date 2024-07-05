namespace MountainGoap;

/// <summary>
/// Delegate type for a listener to the event that fires when an action finishes executing.
/// </summary>
/// <param name="agent">Agent executing the action.</param>
/// <param name="action">Action being executed.</param>
/// <param name="status">Execution status of the action.</param>
/// <param name="parameters">Parameters to the action being executed.</param>
public delegate Task FinishExecuteActionAsyncEvent(AgentAsync agent, ActionAsync action, ExecutionStatus status, Dictionary<string, object?> parameters);