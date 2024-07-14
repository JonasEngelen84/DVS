using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenAddEmployeeCommand(DVSListingViewModel dVSListingViewModel,
        EmployeeStore employeeStore, ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(
                _dVSListingViewModel, _employeeStore, _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
