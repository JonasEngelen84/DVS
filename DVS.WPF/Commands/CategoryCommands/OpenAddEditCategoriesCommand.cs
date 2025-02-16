using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.CategoryCommands
{
    public class OpenAddEditCategoriesCommand(
        ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        SizeStore sizeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        AddClothesViewModel addClothesViewModel,
        EditClothesViewModel editClothesViewModel,
        AddEditClothesListingViewModel addEditListingViewModel,
        DVSListingViewModel dVSListingViewModel)
        : CommandBase
    {
        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(modalNavigationStore,
                                                                     categoryStore,
                                                                     seasonStore,
                                                                     sizeStore,
                                                                     clothesStore,
                                                                     clothesSizeStore,
                                                                     employeeClothesSizesStore,
                                                                     employeeStore,
                                                                     addClothesViewModel,
                                                                     editClothesViewModel,
                                                                     addEditListingViewModel,
                                                                     dVSListingViewModel);

            modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
