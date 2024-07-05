namespace MountainGoap
{
    /// <summary>
    /// Delegate type for an async callback that runs a sensor during a game loop.
    /// </summary>
    /// <param name="agent">Agent using the sensor.</param>
    /// <returns>Async Task.</returns>
    public delegate Task SensorRunAsyncCallback(AgentAsync agent);
}