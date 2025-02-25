using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class OpenEditEmployeeCommand(
        EmployeeListingItemViewModel employeeListingItemViewModel,
        ModalNavigationStore modalNavigationStore,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        DVSListingViewModel dVSListingViewModel,
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            Employee employee = employeeListingItemViewModel.Employee;

            addEditEmployeeListingViewModel.LoadAvailableSizes();
            addEditEmployeeListingViewModel.LoadEmployeeClothes(employee);

            EditEmployeeViewModel EditEmployeeViewModel = new(
                employee,
                employeeStore,
                clothesStore,
                categoryStore,
                seasonStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                modalNavigationStore,
                addEditEmployeeListingViewModel);

            modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
        }
    }
}
