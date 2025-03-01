using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class OpenEditEmployeeCommand(
        EmployeeListingItemViewModel employeeListingItemViewModel,
        ModalNavigationStore modalNavigationStore,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            EditEmployeeViewModel EditEmployeeViewModel = new(
                employeeListingItemViewModel.Employee,
                employeeStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                modalNavigationStore);

            modalNavigationStore.CurrentViewModel = EditEmployeeViewModel;
        }
    }
}
