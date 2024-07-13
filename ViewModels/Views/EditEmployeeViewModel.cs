using DVS.Commands;
using DVS.Commands.AddEditEmployeeCommands;
using DVS.Stores;
using DVS.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.ViewModels.Views
{
    public class EditEmployeeViewModel : ViewModelBase
    {
        public AddEditEmployeeFormViewModel AddEditEmployeeFormViewModel { get; }
        public ICommand CloseModalCommand { get; }


        public EditEmployeeViewModel(DVSListingViewModel dVSListingViewModel, ClothesStore clothesStore,
            EmployeeStore employeeStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand editEmployeeCommand = new EditEmployeeCommand(this, modalNavigationStore);
            CloseModalCommand = new CloseModalCommand(modalNavigationStore);

            AddEditEmployeeFormViewModel = new(dVSListingViewModel,
                                               clothesStore,
                                               employeeStore,
                                               editEmployeeCommand);
        }
    }
}
