using DVS.Stores;

namespace DVS.Commands.AddViewCommands
{
    public class CancelAddEmployeeCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
