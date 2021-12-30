namespace Module.Project.CommandSignals
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommandWithSignal
    {
        void Execute(ISignal signal);
    }
}