using DVS.Models;
using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSHeadViewCommands
{
    public class OpenEditEmployeeCommand(DVSListingViewModel dVSListingViewModel,
        EmployeeModel employee, ModalNavigationStore modalNavigationStore) : CommandBase
    {
        private readonly EmployeeModel _employee = employee;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override void Execute(object parameter)
        {
            EditEmployeeViewModel EditEmployeeViewModel = new(
                _employee, _dVSListingViewModel, _modalNavigationStore);

            _modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
        }
    }
}
