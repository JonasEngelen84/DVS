using DVS.Commands;
using DVS.Commands.EmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class AddEmployeeViewModel : ViewModelBase
    {
        public AddEmployeeFormViewModel AddEmployeeFormViewModel { get; }

        public AddEmployeeViewModel(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel,
                                    EmployeeStore employeeStore,
                                    ModalNavigationStore modalNavigationStore)
        {
            ICommand addEmployeeCommand = new AddEmployeeCommand(this, employeeStore);
            ICommand cancelEmployeeCommand = new CloseModalCommand(modalNavigationStore);

            AddEmployeeFormViewModel = new AddEmployeeFormViewModel(dVSDetailedClothesListingViewModel,
                                                                    addEmployeeCommand,
                                                                    cancelEmployeeCommand);
        }
    }
}
