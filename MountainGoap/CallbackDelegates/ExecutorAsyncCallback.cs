namespace MountainGoap;

/// <summary>
/// Delegate type for a callback that defines a list of all possible parameter states for the given state.
/// </summary>
/// <param name="agent">Agent executing the action.</param>
/// <param name="action">Action being executed.</param>
/// <returns>New execution status of the action.</returns>
public delegate Task<ExecutionStatus> ExecutorAsyncCallback(AgentAsync agent, ActionAsync action);