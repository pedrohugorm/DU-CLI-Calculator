namespace DUCalculator.LiveTrace;

public class LiveTraceSubProgram
{
    public void Run()
    {
        var positionAggregator = new PositionAggregator();
        var context = new ExecutionContext(positionAggregator);
        
        ReadCommands(command =>
        {
            try
            {
                command.Execute(context);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Failed to Parse Line");
                Console.WriteLine(e.ToString());
                Console.WriteLine();
            }
        });
    }

    public void ReadCommands(Action<ICommand> onCommand)
    {
        while (true)
        {
            var line = Console.ReadLine()?.ToLower().Trim();

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            
            if (line.StartsWith("::pos{") && line.EndsWith("}"))
            {
                onCommand.Invoke(
                    new PositionStringCommand(
                        line,
                        new OutputTraceCommand()
                    )
                );
            }
            else if (line is "exit" or "x")
            {
                Console.WriteLine("Exited!");
                break;
            }
            else if (line is "clear" or "c")
            {
                onCommand.Invoke(new ClearScreenCommand());
            }
            else if (line is "ct")
            {
                onCommand.Invoke(new ClearBufferCommand());
            }
            else if (line is "trace" or "t")
            {
                onCommand.Invoke(new OutputTraceCommand());
            }
            else if (line.StartsWith("su "))
            {
                onCommand.Invoke(new SetTraceOutputListCommand(line));
            }
            else if (line is "show")
            {
                onCommand.Invoke(new ShowContextDataCommand());
            }
            else if (line is "help")
            {
                onCommand.Invoke(new ShowHelpCommand());
            }
        }
    }
}