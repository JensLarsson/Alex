using System;

public abstract class Command
{
    public abstract void Execute();

}

public class ActionCommand : Command
{
    private Action action;
    public ActionCommand(Action OnExecute)
    {
        this.action = OnExecute;
    }

    public override void Execute()
    {
        action();
    }
}