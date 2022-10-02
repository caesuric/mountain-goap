// <copyright file="Agent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    /// <summary>
    /// GOAP agent.
    /// </summary>
    public class Agent {
        /// <summary>
        /// The current world state from the agent perspective.
        /// </summary>
        public Dictionary<string, object> State = new();

        /// <summary>
        /// Memory storage object for the agent.
        /// </summary>
        public Dictionary<string, object> Memory = new();

        /// <summary>
        /// List of active goals for the agent.
        /// </summary>
        public List<Goal> Goals = new();

        /// <summary>
        /// Actions available to the agent.
        /// </summary>
        public List<Action> Actions = new();

        /// <summary>
        /// Sensors available to the agent.
        /// </summary>
        public List<Sensor> Sensors = new();

        /// <summary>
        /// True if the agent is currently executing one or more actions, otherwise false.
        /// </summary>
        public bool IsBusy = false;

        /// <summary>
        /// True if the agent is currently planning, otherwise false.
        /// </summary>
        public bool IsPlanning = false;

        /// <summary>
        /// Chains of actions currently being performed by the agent.
        /// </summary>
        internal List<List<Action>> CurrentActionSequences = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        /// <param name="state">Initial agent state.</param>
        /// <param name="memory">Initial agent memory.</param>
        /// <param name="goals">Initial agent goals.</param>
        /// <param name="actions">Actions available to the agent.</param>
        /// <param name="sensors">Sensors available to the agent.</param>
        public Agent(Dictionary<string, object>? state = null, Dictionary<string, object>? memory = null, List<Goal>? goals = null, List<Action>? actions = null, List<Sensor>? sensors = null) {
            if (state != null) State = state;
            if (memory != null) Memory = memory;
            if (goals != null) Goals = goals;
            if (actions != null) Actions = actions;
            if (sensors != null) Sensors = sensors;
        }

        /// <summary>
        /// You should call this every time your game scene updates.
        /// </summary>
        public void Step() {
            foreach (var sensor in Sensors) sensor.Run(this);
            if (!IsBusy && !IsPlanning) {
                IsPlanning = true;
                new Task(() => Planner.Plan(this)).Start();
            }
            else if (!IsPlanning) Execute();
        }

        /// <summary>
        /// Executes the current action sequences.
        /// </summary>
        private void Execute() {
            if (CurrentActionSequences.Count > 0) {
                List<List<Action>> cullableSequences = new();
                foreach (var sequence in CurrentActionSequences) {
                    if (sequence.Count > 0) {
                        sequence[0].Execute(this);
                        sequence.RemoveAt(0);
                    }
                    else cullableSequences.Add(sequence);
                }
                foreach (var sequence in cullableSequences) CurrentActionSequences.Remove(sequence);
            }
            else IsBusy = false;
        }
    }
}