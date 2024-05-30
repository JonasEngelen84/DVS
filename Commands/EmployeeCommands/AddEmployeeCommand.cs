using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.EmployeeCommands
{
    public class AddEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
