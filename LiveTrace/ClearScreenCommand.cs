namespace DUCalculator.LiveTrace;

public class ClearScreenCommand : ICommand
{
    public void Execute(ExecutionContext context)
    {
        Console.Clear();
        Console.WriteLine("Ready to receive new positions.");
    }
}