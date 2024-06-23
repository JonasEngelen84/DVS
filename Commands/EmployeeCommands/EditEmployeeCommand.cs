using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.EmployeeCommands
{
    public class EditEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditEmployeeCommand(AddEditEmployeeViewModel addEmployeeViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
