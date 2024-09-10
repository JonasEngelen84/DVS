using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.ListViewItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditEmployeeCommands
{
    public class OpenEditEmployeeCommand(EmployeeListingItemViewModel employeeListingItemViewModel,
                                         ModalNavigationStore modalNavigationStore,
                                         EmployeeStore employeeStore,
                                         ClothesStore clothesStore,
                                         DVSListingViewModel dVSListingViewModel,
                                         AddEditEmployeeListingViewModel addEditEmployeeListingViewModel)
                                         : CommandBase
    {
        private readonly EmployeeListingItemViewModel _employeeListingItemViewModel = employeeListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public override void Execute(object parameter)
        {
            Employee employee = _employeeListingItemViewModel.Employee;
            _addEditEmployeeListingViewModel.LoadAvailableSizes();
            _addEditEmployeeListingViewModel.LoadEmployeeClothes(employee);

            EditEmployeeViewModel EditEmployeeViewModel = new(employee,
                                                              _employeeStore,
                                                              _clothesStore,
                                                              _modalNavigationStore,
                                                              _dVSListingViewModel,
                                                              _addEditEmployeeListingViewModel);

            _modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
        }
    }
}
