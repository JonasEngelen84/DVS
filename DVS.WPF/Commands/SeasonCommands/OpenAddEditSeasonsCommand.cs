using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.SeasonCommands
{
    public class OpenAddEditSeasonsCommand(ModalNavigationStore modalNavigationStore,
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
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizeStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly AddEditClothesListingViewModel _addEditListingViewModel = addEditListingViewModel;

        public override void Execute(object parameter)
        {
            AddEditSeasonViewModel addEditSeasonViewModel = new(_modalNavigationStore,
                                                                _sizeStore,
                                                                _categoryStore,
                                                                _seasonStore,
                                                                _clothesStore,
                                                                _clothesSizeStore,
                                                                _employeeClothesSizesStore,
                                                                _employeeStore,
                                                                _addClothesViewModel,
                                                                _editClothesViewModel,
                                                                _addEditListingViewModel);

            _modalNavigationStore.CurrentViewModel = addEditSeasonViewModel;
        }
    }
}
