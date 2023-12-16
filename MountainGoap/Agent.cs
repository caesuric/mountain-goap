// <copyright file="Agent.cs" company="Chris Muller">
// Copyright (c) Chris Muller. All rights reserved.
// </copyright>

namespace MountainGoap {
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// GOAP agent.
    /// </summary>
    public class Agent {
        /// <summary>
        /// Name of the agent.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Chains of actions currently being performed by the agent.
        /// </summary>
        internal List<List<Action>> CurrentActionSequences = new();

        /// <summary>
        /// Gets the current action sequences.
        /// </summary>
        public List<List<Action>> GetCurrentActionSequences()
        {
            return CurrentActionSequences;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        /// <param name="name">Name of the agent.</param>
        /// <param name="state">Initial agent state.</param>
        /// <param name="memory">Initial agent memory.</param>
        /// <param name="goals">Initial agent goals.</param>
        /// <param name="actions">Actions available to the agent.</param>
        /// <param name="sensors">Sensors available to the agent.</param>
        /// <param name="costMaximum">Maximum cost of an allowable plan.</param>
        public Agent(string? name = null, Dictionary<string, object?>? state = null, Dictionary<string, object?>? memory = null, List<BaseGoal>? goals = null, List<Action>? actions = null, List<Sensor>? sensors = null, float costMaximum = float.MaxValue) {
            Name = name ?? $"Agent {Guid.NewGuid()}";
            if (state != null) State = state;
            if (memory != null) Memory = memory;
            if (goals != null) Goals = goals;
            if (actions != null) Actions = actions;
            if (sensors != null) Sensors = sensors;
            CostMaximum = costMaximum;
        }

        /// <summary>
        /// Event that fires when the agent executes a step of work.
        /// </summary>
        public static event AgentStepEvent OnAgentStep = (agent) => { };

        /// <summary>
        /// Event that fires when an action sequence completes.
        /// </summary>
        public static event AgentActionSequenceCompletedEvent OnAgentActionSequenceCompleted = (agent) => { };

        /// <summary>
        /// Event that fires when planning begins.
        /// </summary>
        public static event PlanningStartedEvent OnPlanningStarted = (agent) => { };

        /// <summary>
        /// Event that fires when planning for a single goal finishes.
        /// </summary>
        public static event PlanningFinishedForSingleGoalEvent OnPlanningFinishedForSingleGoal = (agent, goal, utility) => { };

        /// <summary>
        /// Event that fires when planning finishes.
        /// </summary>
        public static event PlanningFinishedEvent OnPlanningFinished = (agent, goal, utility) => { };

        /// <summary>
        /// Event that fires when a new plan is finalized for the agent.
        /// </summary>
        public static event PlanUpdatedEvent OnPlanUpdated = (agent, actionList) => { };

        /// <summary>
        /// Event that fires when the pathfinder evaluates a single node in the action graph.
        /// </summary>
        public static event EvaluatedActionNodeEvent OnEvaluatedActionNode = (node, nodes) => { };

        /// <summary>
        /// Gets or sets the current world state from the agent perspective.
        /// </summary>
        public Dictionary<string, object?> State { get; set; } = new();

        /// <summary>
        /// Gets or sets the memory storage object for the agent.
        /// </summary>
        public Dictionary<string, object?> Memory { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of active goals for the agent.
        /// </summary>
        public List<BaseGoal> Goals { get; set; } = new();

        /// <summary>
        /// Gets or sets the actions available to the agent.
        /// </summary>
        public List<Action> Actions { get; set; } = new();

        /// <summary>
        /// Gets or sets the sensors available to the agent.
        /// </summary>
        public List<Sensor> Sensors { get; set; } = new();

        /// <summary>
        /// Gets or sets the plan cost maximum for the agent.
        /// </summary>
        public float CostMaximum { get; set; } = new();

        /// <summary>
        /// Gets or sets a value indicating whether the agent is currently executing one or more actions.
        /// </summary>
        public bool IsBusy { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the agent is currently planning.
        /// </summary>
        public bool IsPlanning { get; set; } = false;

        /// <summary>
        /// You should call this every time your game state updates.
        /// </summary>
        /// <param name="mode">Mode to be used for executing the step of work.</param>
        public void Step(StepMode mode = StepMode.Default) {
            OnAgentStep(this);
            foreach (var sensor in Sensors) sensor.Run(this);
            if (mode == StepMode.Default) {
                StepAsync();
                return;
            }
            if (!IsBusy) Planner.Plan(this, CostMaximum);
            if (mode == StepMode.OneAction) Execute();
            else if (mode == StepMode.AllActions) while (IsBusy) Execute();
        }

        /// <summary>
        /// Triggers OnPlanningStarted event.
        /// </summary>
        /// <param name="agent">Agent that started planning.</param>
        internal static void TriggerOnPlanningStarted(Agent agent) {
            OnPlanningStarted(agent);
        }

        /// <summary>
        /// Triggers OnPlanningFinishedForSingleGoal event.
        /// </summary>
        /// <param name="agent">Agent that finished planning.</param>
        /// <param name="goal">Goal for which planning was completed.</param>
        /// <param name="utility">Utility of the plan.</param>
        internal static void TriggerOnPlanningFinishedForSingleGoal(Agent agent, BaseGoal goal, float utility) {
            OnPlanningFinishedForSingleGoal(agent, goal, utility);
        }

        /// <summary>
        /// Triggers OnPlanningFinished event.
        /// </summary>
        /// <param name="agent">Agent that finished planning.</param>
        /// <param name="goal">Goal that was selected.</param>
        /// <param name="utility">Utility of the plan.</param>
        internal static void TriggerOnPlanningFinished(Agent agent, BaseGoal? goal, float utility) {
            OnPlanningFinished(agent, goal, utility);
        }

        /// <summary>
        /// Triggers OnPlanUpdated event.
        /// </summary>
        /// <param name="agent">Agent for which the plan was updated.</param>
        /// <param name="actionList">New action list for the agent.</param>
        internal static void TriggerOnPlanUpdated(Agent agent, List<Action> actionList) {
            OnPlanUpdated(agent, actionList);
        }

        /// <summary>
        /// Triggers OnEvaluatedActionNode event.
        /// </summary>
        /// <param name="node">Action node being evaluated.</param>
        /// <param name="nodes">List of nodes in the path that led to this point.</param>
        internal static void TriggerOnEvaluatedActionNode(ActionNode node, Dictionary<ActionNode, ActionNode> nodes) {
            OnEvaluatedActionNode(node, nodes);
        }

        /// <summary>
        /// Executes an asynchronous step of agent work.
        /// </summary>
        private void StepAsync() {
            if (!IsBusy && !IsPlanning) {
                IsPlanning = true;
                var t = new Thread(new ThreadStart(() => { Planner.Plan(this, CostMaximum); }));
                t.Start();
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
                        var executionStatus = sequence[0].Execute(this);
                        if (executionStatus != ExecutionStatus.Executing) sequence.RemoveAt(0);
                    }
                    else cullableSequences.Add(sequence);
                }
                foreach (var sequence in cullableSequences) {
                    CurrentActionSequences.Remove(sequence);
                    OnAgentActionSequenceCompleted(this);
                }
            }
            else IsBusy = false;
        }
    }
}