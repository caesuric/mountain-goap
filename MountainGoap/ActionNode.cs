// <copyright file="ActionNode.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// Represents an action node in an action graph.
    /// </summary>
    internal class ActionNode {
        /// <summary>
        /// The state of the world for this action node.
        /// </summary>
        internal Dictionary<string, object> State = new ();

        /// <summary>
        /// The action to be executed when the world is in the defined <see cref="State"/>.
        /// </summary>
        internal Action Action;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionNode"/> class.
        /// </summary>
        /// <param name="action">Action to be assigned to the node.</param>
        /// <param name="state">State to be assigned to the node.</param>
        internal ActionNode(Action action, Dictionary<string, object> state) {
            Action = action;
            State = state;
        }

        /// <summary>
        /// Cost to traverse this node.
        /// </summary>
        /// <returns>THe cost of the action to be executed.</returns>
        internal float Cost() {
            return Action.Cost;
        }
    }
}