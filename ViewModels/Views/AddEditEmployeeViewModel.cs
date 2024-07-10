using DVS.Commands;
using DVS.Commands.AddEditEmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEditEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }

        public ICommand CloseModalCommand { get; }


        public AddEditEmployeeViewModel(DVSListingViewModel dVSListingViewModel,
                                        ClothesStore clothesStore,
                                        EmployeeStore employeeStore,
                                        ModalNavigationStore modalNavigationStore)
        {
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);
            ICommand editEmployeeCommand = new EditEmployeeCommand(this, modalNavigationStore);
            ICommand clearEmployeeClothesListCommand = new ClearEmployeeClothesListCommand(modalNavigationStore);
            ICommand deleteEmployeeCommand = new DeleteEmployeeCommand();
            ICommand addEmployeeCommand = new AddEmployeeCommand(this, employeeStore, modalNavigationStore);

            AddEditEmployeeFormViewModel = new AddEditEmployeeFormViewModel(
                dVSListingViewModel, clothesStore, employeeStore, addEmployeeCommand,
                editEmployeeCommand, clearEmployeeClothesListCommand, deleteEmployeeCommand);
        }
    }
}
