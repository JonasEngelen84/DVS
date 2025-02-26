using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class OpenAddEmployeeCommand(
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore,
        DVSListingViewModel dVSListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddEmployeeViewModel addEmployeeViewModel = new(
                employeeStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizeStore,
                modalNavigationStore,
                dVSListingViewModel);

            modalNavigationStore.CurrentViewModel = addEmployeeViewModel;
        }
    }
}
