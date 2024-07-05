using System.Collections.Concurrent;

namespace MountainGoap;

public interface IAgent
{
    /// <summary>
    /// Name of the agent.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets or sets the current world state from the agent perspective.
    /// </summary>
    ConcurrentDictionary<string, object?> State { get; set; }

    /// <summary>
    /// Gets or sets the list of active goals for the agent.
    /// </summary>
    List<BaseGoal> Goals { get; set; }

    /// <summary>
    /// Gets or sets the actions available to the agent.
    /// </summary>
    List<IAction> ActionList { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the agent is currently executing one or more actions.
    /// </summary>
    public bool IsBusy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the agent is currently planning.
    /// </summary>
    public bool IsPlanning { get; set; }
    
    public void AddCurrentActionSequences(List<IAction> actionList);
}