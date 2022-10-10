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

### **State**

The current world state from the agent perspective.

```csharp
public Dictionary<string, object> State;
```

### **Memory**

Memory storage object for the agent.

```csharp
public Dictionary<string, object> Memory;
```

### **Goals**

List of active goals for the agent.

```csharp
public List<Goal> Goals;
```

### **Actions**

Actions available to the agent.

```csharp
public List<Action> Actions;
```

### **Sensors**

Sensors available to the agent.

```csharp
public List<Sensor> Sensors;
```

### **IsBusy**

True if the agent is currently executing one or more actions, otherwise false.

```csharp
public bool IsBusy;
```

### **IsPlanning**

True if the agent is currently planning, otherwise false.

```csharp
public bool IsPlanning;
```

## Constructors

### **Agent(String, Dictionary&lt;String, Object&gt;, Dictionary&lt;String, Object&gt;, List&lt;Goal&gt;, List&lt;Action&gt;, List&lt;Sensor&gt;)**

Initializes a new instance of the [Agent](./mountaingoap.agent.md) class.

```csharp
public Agent(string name, Dictionary<string, object> state, Dictionary<string, object> memory, List<Goal> goals, List<Action> actions, List<Sensor> sensors)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the agent.

`state` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Initial agent state.

`memory` [Dictionary&lt;String, Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>
Initial agent memory.

`goals` [List&lt;Goal&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Initial agent goals.

`actions` [List&lt;Action&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Actions available to the agent.

`sensors` [List&lt;Sensor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Sensors available to the agent.

## Methods

### **Step()**

You should call this every time your game scene updates.

```csharp
public void Step()
```

### **TriggerOnPlanningStarted(Agent)**

Triggers OnPlanningStarted event.

```csharp
internal static void TriggerOnPlanningStarted(Agent agent)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent that started planning.

### **TriggerOnPlanningFinishedForSingleGoal(Agent, Goal, Single)**

Triggers OnPlanningFinishedForSingleGoal event.

```csharp
internal static void TriggerOnPlanningFinishedForSingleGoal(Agent agent, Goal goal, float utility)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent that finished planning.

`goal` [Goal](./mountaingoap.goal.md)<br>
Goal for which planning was completed.

`utility` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Utility of the plan.

### **TriggerOnPlanningFinished(Agent, Goal, Single)**

Triggers OnPlanningFinished event.

```csharp
internal static void TriggerOnPlanningFinished(Agent agent, Goal goal, float utility)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent that finished planning.

`goal` [Goal](./mountaingoap.goal.md)<br>
Goal that was selected.

`utility` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>
Utility of the plan.

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
