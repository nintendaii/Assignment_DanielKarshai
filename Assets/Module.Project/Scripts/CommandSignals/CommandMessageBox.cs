using Module.Project.Controllers;
using Zenject;

namespace Module.Project.CommandSignals {
    public class CommandMessageBox : ICommandWithSignal {
        [Inject] private readonly MessageBoxController messageBoxController;
        
        public void Execute(ISignal signal) {
            switch (signal) {
                case SignalShowMessageBox signalShowMessageBox:
                    messageBoxController.SetMessage(signalShowMessageBox.Message);
                    messageBoxController.ShowComponent();
                    break;
                case SignalHideMessageBox signalHideMessageBox:
                    messageBoxController.HideComponent();
                    messageBoxController.SetMessage(string.Empty);
                    break;
            }
        }
    }
}