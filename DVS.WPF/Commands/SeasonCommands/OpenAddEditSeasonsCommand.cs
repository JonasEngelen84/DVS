using DVS.Domain.Services.Interfaces;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class OpenAddEditSeasonsCommand(
        ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        AddClothesViewModel addClothesViewModel,
        EditClothesViewModel editClothesViewModel,
        SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel,
        IDirtyEntitySaver dirtyEntitySaver)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(
                modalNavigationStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                addClothesViewModel,
                editClothesViewModel,
                SizesCategoriesSeasonsListingViewModel,
                dirtyEntitySaver);

            modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
