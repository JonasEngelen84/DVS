using DVS.Stores;

namespace DVS.Commands.AddViewCommands
{
    class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            
        }
    }
}
