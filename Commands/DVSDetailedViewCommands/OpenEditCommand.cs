using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Views;

namespace DVS.Commands.DVSDetailedViewCommands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly SelectedSeasonStore _selectedSeasonStore;

        public OpenEditCommand(
            DVSListingViewModel dVSListingViewModel, ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, SelectedCategoryStore selectedCategoryStore,
            SelectedSeasonStore selectedSeasonStore, SelectedClothesStore selectedClothesStore,
            SelectedEmployeeClothesStore selectedEmployeeClothesStore, ClothesStore clothesStore, EmployeeStore employeeStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _clothesStore = clothesStore;
            _employeeStore = employeeStore;
            _dVSListingViewModel = dVSListingViewModel;
            _modalNavigationStore = modalNavigationStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _selectedCategoryStore = selectedCategoryStore;
            _selectedSeasonStore = selectedSeasonStore;
        }

        int i = 0;
        public override void Execute(object parameter)
        {
            if(i%2 == 0)
            {
                _modalNavigationStore.CurrentViewModel = new AddEditClothesViewModel(
                    _modalNavigationStore, _categoryStore, _seasonStore,
                    _selectedCategoryStore, _selectedSeasonStore, _clothesStore);
            }
            else
            {
                _modalNavigationStore.CurrentViewModel = new AddEditEmployeeViewModel(
                    _dVSListingViewModel, _clothesStore, _employeeStore, _modalNavigationStore);
            }

            i++;
        }
    }
}
