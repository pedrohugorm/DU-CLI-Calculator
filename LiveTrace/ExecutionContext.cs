namespace DUCalculator.LiveTrace;

public class ExecutionContext
{
    public List<SpaceUnit> TraceOutputDistanceList = new()
    {
        new SpaceUnit(5),
        new SpaceUnit(10),
        new SpaceUnit(15),
        new SpaceUnit(20),
    };
    
    public PositionAggregator PositionAggregator { get; }

    public ExecutionContext(PositionAggregator positionAggregator)
    {
        PositionAggregator = positionAggregator;
    }
}