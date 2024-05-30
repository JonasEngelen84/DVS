using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.EmployeeCommands
{
    public class EditEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditEmployeeCommand(EditEmployeeViewModel editEmployeeViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
