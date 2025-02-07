using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class OpenEditEmployeeCommand(EmployeeListingItemViewModel employeeListingItemViewModel,
                                         ModalNavigationStore modalNavigationStore,
                                         EmployeeStore employeeStore,
                                         ClothesStore clothesStore,
                                         SizeStore sizeStore,
                                         CategoryStore categoryStore,
                                         SeasonStore seasonStore,
                                         ClothesSizeStore clothesSizeStore,
                                         EmployeeClothesSizesStore employeeClothesSizesStore,
                                         DVSListingViewModel dVSListingViewModel,
                                         AddEditEmployeeListingViewModel addEditEmployeeListingViewModel)
                                         : CommandBase
    {
        private readonly EmployeeListingItemViewModel _employeeListingItemViewModel = employeeListingItemViewModel;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;
        private readonly AddEditEmployeeListingViewModel _addEditEmployeeListingViewModel = addEditEmployeeListingViewModel;

        public override void Execute(object parameter)
        {
            Employee employee = _employeeListingItemViewModel.Employee;

            _addEditEmployeeListingViewModel.ClearLists();
            _addEditEmployeeListingViewModel.LoadAvailableSizes();
            _addEditEmployeeListingViewModel.LoadEmployeeClothes(employee);

            EditEmployeeViewModel EditEmployeeViewModel = new(employee,
                                                              _employeeStore,
                                                              _clothesStore,
                                                              _sizeStore,
                                                              _categoryStore,
                                                              _seasonStore,
                                                              _clothesSizeStore,
                                                              _employeeClothesSizesStore,
                                                              _modalNavigationStore,
                                                              _addEditEmployeeListingViewModel);

            _modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
        }
    }
}
