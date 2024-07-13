using DVS.Stores;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditEmployeeCommands
{
    public class EditEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditEmployeeCommand(EditEmployeeViewModel editEmployeeViewModel,ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
