using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.EmployeeCommands
{
    class DeleteEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public DeleteEmployeeCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
