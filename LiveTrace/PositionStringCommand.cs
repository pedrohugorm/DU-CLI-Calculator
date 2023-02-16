namespace DUCalculator.LiveTrace;

public class PositionStringCommand : ICommand
{
    private readonly OutputTraceCommand _outputTraceCommand;
    public string Value { get; }

    public PositionStringCommand(string value, OutputTraceCommand outputTraceCommand)
    {
        _outputTraceCommand = outputTraceCommand;
        Value = value;
    }

    public void Execute(ExecutionContext context)
    {
        context.PositionAggregator.PushPosition(Value.Trim().PositionToVector3());
        _outputTraceCommand.Execute(context);
    }
}