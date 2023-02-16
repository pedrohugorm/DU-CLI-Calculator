namespace DUCalculator.LiveTrace;

public class OutputTraceCommand : ICommand
{
    public void Execute(ExecutionContext context)
    {
        if (!context.PositionAggregator.CanDoTrace())
        {
            return;
        }
        
        foreach (var unit in context.TraceOutputDistanceList)
        {
            var result = context.PositionAggregator.TraceTo(unit);
            Console.WriteLine($"{unit} = {result.Vector3ToPosition()}");
        }
    }
}