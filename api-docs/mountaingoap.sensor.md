# Sensor

Namespace: MountainGoap

Sensor for getting information about world state.

```csharp
public class Sensor
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Sensor](./mountaingoap.sensor.md)

## Fields

### **Name**

Name of the sensor.

```csharp
public string Name;
```

## Constructors

### **Sensor(SensorRunCallback, String)**

Initializes a new instance of the [Sensor](./mountaingoap.sensor.md) class.

```csharp
public Sensor(SensorRunCallback runCallback, string name)
```

#### Parameters

`runCallback` [SensorRunCallback](./mountaingoap.sensorruncallback.md)<br>
Callback to be executed when the sensor runs.

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the sensor.

## Methods

### **Run(Agent)**

Runs the sensor during a game loop.

```csharp
public void Run(Agent agent)
```

#### Parameters

`agent` [Agent](./mountaingoap.agent.md)<br>
Agent for which the sensor is being run.

## Events

### **OnSensorRun**

Event that triggers when a sensor runs.

```csharp
public static event SensorRunEvent OnSensorRun;
```
