using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class OpenAddEditSeasonsCommand(
        ModalNavigationStore modalNavigationStore,
        SizeStore sizeStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        AddClothesViewModel addClothesViewModel,
        EditClothesViewModel editClothesViewModel,
        AddEditClothesListingViewModel addEditListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(
                modalNavigationStore,
                sizeStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                addClothesViewModel,
                editClothesViewModel,
                addEditListingViewModel);

            modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
