namespace MountainGoap;

/// <summary>
/// Delegate type for a listener to the event that fires when an agent completes an action sequence.
/// </summary>
/// <param name="agent">Agent executing the action sequence.</param>
public delegate Task AgentActionSequenceCompletedAsyncEvent(AgentAsync agent);