using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class OpenAddEditCategoriesCommand(ModalNavigationStore modalNavigationStore,
                                              CategoryStore categoryStore,
                                              SeasonStore seasonStore,
                                              SizeStore sizeStore,
                                              ClothesStore clothesStore,
                                              ClothesSizeStore clothesSizeStore,
                                              EmployeeClothesSizesStore employeeClothesSizesStore,
                                              EmployeeStore employeeStore,
                                              AddClothesViewModel addClothesViewModel,
                                              EditClothesViewModel editClothesViewModel,
                                              AddEditClothesListingViewModel addEditListingViewModel,
                                              DVSListingViewModel dVSListingViewModel)
                                              : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly AddEditClothesListingViewModel _addEditListingViewModel = addEditListingViewModel;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override void Execute(object parameter)
        {
            AddEditCategoryViewModel addEditCategorieViewModel = new(_modalNavigationStore,
                                                                     _categoryStore,
                                                                     _seasonStore,
                                                                     _sizeStore,
                                                                     _clothesStore,
                                                                     _clothesSizeStore,
                                                                     _employeeClothesSizesStore,
                                                                     _employeeStore,
                                                                     _addClothesViewModel,
                                                                     _editClothesViewModel,
                                                                     _addEditListingViewModel,
                                                                     _dVSListingViewModel);

            _modalNavigationStore.CurrentViewModel = addEditCategorieViewModel;
        }
    }
}
