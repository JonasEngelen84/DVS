using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class OpenAddClothesCommand(
        ModalNavigationStore modalNavigationStore,
        SizeStore sizeStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        DVSListingViewModel dVSListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddClothesViewModel addClothesViewModel = new(modalNavigationStore,
                                                          sizeStore,
                                                          categoryStore,
                                                          seasonStore,
                                                          clothesStore,
                                                          clothesSizeStore,
                                                          employeeClothesSizesStore,
                                                          employeeStore,
                                                          dVSListingViewModel);

            modalNavigationStore.CurrentViewModel = addClothesViewModel;
        }
    }
}
