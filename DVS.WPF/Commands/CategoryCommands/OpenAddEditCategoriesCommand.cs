using DVS.Domain.Services.Interfaces;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CategoryCommands
{
    public class OpenAddEditCategoriesCommand(
        ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore,
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
            AddEditCategoryViewModel addEditCategorieViewModel = new(
                modalNavigationStore,
                categoryStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                addClothesViewModel,
                editClothesViewModel,
                SizesCategoriesSeasonsListingViewModel,
                dirtyEntitySaver);

            modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
