namespace MountainGoap;

/// <summary>
/// Delegate type for a listener to the event that fires when an agent executes a step of work.
/// </summary>
/// <param name="agent">Agent executing the step of work.</param>
public delegate Task AgentStepAsyncEvent(AgentAsync agent);