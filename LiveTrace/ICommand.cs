namespace DUCalculator.LiveTrace;

public interface ICommand
{
    void Execute(ExecutionContext context);
}