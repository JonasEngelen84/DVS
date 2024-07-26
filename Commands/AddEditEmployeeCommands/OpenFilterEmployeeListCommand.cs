using DVS.Stores;

namespace DVS.Commands.AddEditEmployeeCommands
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
