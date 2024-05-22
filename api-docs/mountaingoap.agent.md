# Agent

Namespace: MountainGoap

GOAP agent.

```csharp
public class Agent
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Agent](./mountaingoap.agent.md)

## Fields

### **Name**

Name of the agent.

```csharp
public string Name;
```

## Properties

### **CurrentActionSequences**

Gets the chains of actions currently being performed by the agent.

```csharp
public List<List<Action>> CurrentActionSequences { get; }
```

#### Property Value

[List&lt;List&lt;Action&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **State**

Gets or sets the current world state from the agent perspective.

```csharp
public ConcurrentDictionary<string, object> State { get; set; }
```

#### Property Value

ConcurrentDictionary&lt;String, Object&gt;<br>

### **Memory**

Gets or sets the memory storage object for the agent.

```csharp
public Dictionary<string, object> Memory { get; set; }
```

#### Property Value

[Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

### **Goals**

Gets or sets the list of active goals for the agent.

```csharp
public List<BaseGoal> Goals { get; set; }
```

#### Property Value

[List&lt;BaseGoal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **Actions**

Gets or sets the actions available to the agent.

```csharp
public List<Action> Actions { get; set; }
```

#### Property Value

[List&lt;Action&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **Sensors**

Gets or sets the sensors available to the agent.

```csharp
public List<Sensor> Sensors { get; set; }
```

#### Property Value

[List&lt;Sensor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **CostMaximum**

Gets or sets the plan cost maximum for the agent.

```csharp
public float CostMaximum { get; set; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **IsBusy**

Gets or sets a value indicating whether the agent is currently executing one or more actions.

```csharp
public bool IsBusy { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsPlanning**

Gets or sets a value indicating whether the agent is currently planning.

```csharp
public bool IsPlanning { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **Agent(String, ConcurrentDictionary&lt;String, Object&gt;, Dictionary&lt;String, Object&gt;, List&lt;BaseGoal&gt;, List&lt;Action&gt;, List&lt;Sensor&gt;, Single)**

Initializes a new instance of the [Agent](./mountaingoap.agent.md) class.

```csharp
public Agent(string name, ConcurrentDictionary<string, object> state, Dictionary<string, object> memory, List<BaseGoal> goals, List<Action> actions, List<Sensor> sensors, float costMaximum)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the agent.

`state` ConcurrentDictionary&lt;String, Object&gt;<br>
Initial agent state.

`memory` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Initial agent memory.

`goals` [List&lt;BaseGoal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Initial agent goals.

`actions` [List&lt;Action&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Actions available to the agent.

`sensors` [List&lt;Sensor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Sensors available to the agent.

`costMaximum` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Maximum cost of an allowable plan.

## Methods

### **Step(StepMode)**

You should call this every time your game state updates.

```csharp
public void Step(StepMode mode)
```

#### Parameters

`mode` [StepMode](./mountaingoap.stepmode.md)<br>
Mode to be used for executing the step of work.

### **ClearPlan()**

Clears the current action sequences (also known as plans).

```csharp
public void ClearPlan()
```

### **Plan()**

Makes a plan.

```csharp
public void Plan()
```

### **PlanAsync()**

Makes a plan asynchronously.

```csharp
public void PlanAsync()
```

### **ExecutePlan()**

Executes the current plan.

```csharp
public void ExecutePlan()
```

### **TriggerOnPlanningStarted(Agent)**

Triggers OnPlanningStarted event.

```csharp
internal static void TriggerOnPlanningStarted(Agent agent)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent that started planning.

### **TriggerOnPlanningStartedForSingleGoal(Agent, BaseGoal)**

Triggers OnPlanningStartedForSingleGoal event.

```csharp
internal static void TriggerOnPlanningStartedForSingleGoal(Agent agent, BaseGoal goal)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent that started planning.

`goal` [BaseGoal](./mountaingoap.basegoal.md)<br>
Goal for which planning was started.

### **TriggerOnPlanningFinishedForSingleGoal(Agent, BaseGoal, Single)**

Triggers OnPlanningFinishedForSingleGoal event.

```csharp
internal static void TriggerOnPlanningFinishedForSingleGoal(Agent agent, BaseGoal goal, float utility)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent that finished planning.

`goal` [BaseGoal](./mountaingoap.basegoal.md)<br>
Goal for which planning was completed.

`utility` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Utility of the plan.

### **TriggerOnPlanningFinished(Agent, BaseGoal, Single)**

Triggers OnPlanningFinished event.

```csharp
internal static void TriggerOnPlanningFinished(Agent agent, BaseGoal goal, float utility)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent that finished planning.

`goal` [BaseGoal](./mountaingoap.basegoal.md)<br>
Goal that was selected.

`utility` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Utility of the plan.

### **TriggerOnPlanUpdated(Agent, List&lt;Action&gt;)**

Triggers OnPlanUpdated event.

```csharp
internal static void TriggerOnPlanUpdated(Agent agent, List<Action> actionList)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent for which the plan was updated.

`actionList` [List&lt;Action&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
New action list for the agent.

### **TriggerOnEvaluatedActionNode(ActionNode, Dictionary&lt;ActionNode, ActionNode&gt;)**

Triggers OnEvaluatedActionNode event.

```csharp
internal static void TriggerOnEvaluatedActionNode(ActionNode node, Dictionary<ActionNode, ActionNode> nodes)
```

#### Parameters

`node` [ActionNode](./mountaingoap.actionnode.md)<br>
Action node being evaluated.

`nodes` [Dictionary&lt;ActionNode, ActionNode&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
List of nodes in the path that led to this point.

## Events

### **OnAgentStep**

Event that fires when the agent executes a step of work.

```csharp
public static event AgentStepEvent OnAgentStep;
```

### **OnAgentActionSequenceCompleted**

Event that fires when an action sequence completes.

```csharp
public static event AgentActionSequenceCompletedEvent OnAgentActionSequenceCompleted;
```

### **OnPlanningStarted**

Event that fires when planning begins.

```csharp
public static event PlanningStartedEvent OnPlanningStarted;
```

### **OnPlanningStartedForSingleGoal**

Event that fires when planning for a single goal starts.

```csharp
public static event PlanningStartedForSingleGoalEvent OnPlanningStartedForSingleGoal;
```

### **OnPlanningFinishedForSingleGoal**

Event that fires when planning for a single goal finishes.

```csharp
public static event PlanningFinishedForSingleGoalEvent OnPlanningFinishedForSingleGoal;
```

### **OnPlanningFinished**

Event that fires when planning finishes.

```csharp
public static event PlanningFinishedEvent OnPlanningFinished;
```

### **OnPlanUpdated**

Event that fires when a new plan is finalized for the agent.

```csharp
public static event PlanUpdatedEvent OnPlanUpdated;
```

### **OnEvaluatedActionNode**

Event that fires when the pathfinder evaluates a single node in the action graph.

```csharp
public static event EvaluatedActionNodeEvent OnEvaluatedActionNode;
```
