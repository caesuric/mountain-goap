using System.Collections.Concurrent;

namespace MountainGoap;

/// <summary>
/// Delegate type for a callback that defines the cost of an action.
/// </summary>
/// <param name="action">Action being executed.</param>
/// <param name="currentState">State as it will be when cost is relevant.</param>
/// <returns>Cost of the action.</returns>
public delegate float CostAsyncCallback(ActionAsync action, ConcurrentDictionary<string, object?> currentState);