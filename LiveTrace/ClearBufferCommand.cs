namespace DUCalculator.LiveTrace;

public class ClearBufferCommand : ICommand
{
    public void Execute(ExecutionContext context)
    {
        context.PositionAggregator.Clear();
        
        Console.Clear();
        Console.WriteLine("Cleared Buffer. Ready to receive new positions.");
    }
}