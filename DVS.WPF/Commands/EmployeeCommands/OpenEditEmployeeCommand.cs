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
        DVSListingViewModel dVSListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            EditEmployeeViewModel EditEmployeeViewModel = new(
                employeeListingItemViewModel.Employee,
                employeeStore,
                clothesStore,
                categoryStore,
                seasonStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                modalNavigationStore);

            modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
        }
    }
}
