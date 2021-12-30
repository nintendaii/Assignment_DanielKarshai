namespace Module.Project.CommandSignals
{
    public class SignalShowMessageBox : ISignal
    {
        public readonly string Message;

        public SignalShowMessageBox(string message)
        {
            Message = message;
        }
    }
}