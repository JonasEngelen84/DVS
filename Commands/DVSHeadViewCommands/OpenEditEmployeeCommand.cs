using DVS.Models;
using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.ListViewItems;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSHeadViewCommands
{
    public class OpenEditEmployeeCommand(EmployeeListingItemViewModel employeeListingItemViewModel,
        ModalNavigationStore modalNavigationStore, EmployeeStore employeeStore, DVSListingViewModel dVSListingViewModel) : CommandBase
    {
        private readonly EmployeeListingItemViewModel _employeeListingItemViewModel = employeeListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override void Execute(object parameter)
        {
            EmployeeModel _employee = _employeeListingItemViewModel.Employee;

            EditEmployeeViewModel EditEmployeeViewModel = new(
                _employee, _employeeStore, _modalNavigationStore, _dVSListingViewModel);

            _modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
        }
    }
}
