using DVS.Stores;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenFilterEmployeeListCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenFilterEmployeeListCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
