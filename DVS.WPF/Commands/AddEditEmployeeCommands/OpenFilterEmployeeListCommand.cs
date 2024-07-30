using DVS.WPF.Stores;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
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
