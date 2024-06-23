using DVS.Stores;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class SaveCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public SaveCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {

        }
    }
}
