using DVS.Commands;
using DVS.Commands.EmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.AddViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        public EditEmployeeFormViewModel EditEmployeeFormViewModel { get; }

        public EditEmployeeViewModel(ModalNavigationStore modalNavigationStore)
        {
            ICommand editEmployeeCommand = new EditEmployeeCommand(this, modalNavigationStore);
            ICommand cancelEmployeeCommand = new CloseModalCommand(modalNavigationStore);
            ICommand clearEmployeeClothesListCommand = new ClearEmployeeClothesListCommand(modalNavigationStore);
            ICommand deleteEmployeeCommand = new DeleteEmployeeCommand();

            EditEmployeeFormViewModel = new EditEmployeeFormViewModel(editEmployeeCommand,
                                                                      deleteEmployeeCommand,
                                                                      clearEmployeeClothesListCommand,
                                                                      cancelEmployeeCommand);
        }
    }
}
