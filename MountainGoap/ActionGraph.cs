// <copyright file="ActionGraph.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Represents a traversable action graph.
    /// </summary>
    internal class ActionGraph {
        /// <summary>
        /// The set of actions for the graph.
        /// </summary>
        internal List<ActionNode> ActionNodes = new();

        /// <summary>
        /// Gets the list of neighbors for a node.
        /// </summary>
        /// <param name="node">Node for which to retrieve neighbors.</param>
        /// <returns>The set of action/state combinations that can be executed after the current action/state combination.</returns>
        internal IEnumerable<ActionNode> Neighbors(ActionNode node) {
            foreach (var otherNode in ActionNodes) {
                if (otherNode.Action.IsPossible(node.State)) {
                    var instancedNode = new ActionNode(otherNode.Action, node.State.Copy());
                    otherNode.Action.ApplyEffects(instancedNode.State);
                    yield return instancedNode;
                }
            }
        }
    }
}