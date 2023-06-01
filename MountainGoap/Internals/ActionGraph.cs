// <copyright file="ActionGraph.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
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
        internal ActionGraph(List<Action> actions, Dictionary<string, object?> state) {
            foreach (var action in actions) {
                var permutations = action.GetPermutations(state);
                foreach (var permutation in permutations) ActionNodes.Add(new(action, state, permutation));
            }
        }

        /// <summary>
        /// Gets the list of neighbors for a node.
        /// </summary>
        /// <param name="node">Node for which to retrieve neighbors.</param>
        /// <returns>The set of action/state combinations that can be executed after the current action/state combination.</returns>
        internal IEnumerable<ActionNode> Neighbors(ActionNode node) {
#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
            foreach (var otherNode in ActionNodes) {
                if (otherNode.Action is not null && otherNode.Action.IsPossible(node.State)) {
                    foreach (var permutation in otherNode.Action.GetPermutations(node.State.Copy())) {
                        var instancedNode = new ActionNode(otherNode.Action, node.State.Copy(), permutation.Copy());
                        otherNode.Action.ApplyEffects(instancedNode.State);
                        yield return instancedNode;
                    }
                }
            }
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
        }
    }
}