// <copyright file="ActionGraph.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap.Async {
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a traversable action graph.
    /// </summary>
    internal class ActionGraph {
        /// <summary>
        /// The set of actions for the graph.
        /// </summary>
        internal List<ActionNode> ActionNodes = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionGraph"/> class.
        /// </summary>
        /// <param name="actions">List of actions to include in the graph.</param>
        /// <param name="state">Initial state to be used.</param>
        /// <returns>Async Task.</returns>
        internal async Task InitAsync(List<Action> actions, ConcurrentDictionary<string, object?> state) {
            foreach (var action in actions) {
                var permutations = await action.GetPermutations(state);
                foreach (var permutation in permutations) ActionNodes.Add(new(action, state, permutation));
            }
        }

        /// <summary>
        /// Gets the list of neighbors for a node.
        /// </summary>
        /// <param name="node">Node for which to retrieve neighbors.</param>
        /// <returns>The set of action/state combinations that can be executed after the current action/state combination.</returns>
        internal async IAsyncEnumerable<ActionNode> NeighborsAsync(ActionNode node) {
            foreach (var otherNode in ActionNodes) {
                if (otherNode.Action is not null && await otherNode.Action.IsPossibleAsync(node.State)) {
                    var newNode = new ActionNode(otherNode.Action.Copy(), node.State.Copy(), otherNode.Parameters.Copy());
                    newNode.Action?.ApplyEffects(newNode.State);
                    yield return newNode;
                }
            }
        }
    }
}