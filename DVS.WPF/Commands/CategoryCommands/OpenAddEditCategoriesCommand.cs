using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CategoryCommands
{
    public class OpenAddEditCategoriesCommand(
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
        DVSListingViewModel dVSListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(
                modalNavigationStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                addClothesViewModel,
                editClothesViewModel,
                SizesCategoriesSeasonsListingViewModel,
                dVSListingViewModel);

            modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
