using DVS.Stores;
using DVS.ViewModels.View_ViewModels;
using DVS.Views;

namespace DVS.Commands.DVSViewCommands
{
    public class OpenEditCommand : CommandBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly SelectedCategoryStore _selectedCategoryStore;
        private readonly SelectedSeasonStore _selectedSeasonStore;

        public OpenEditCommand(
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            SelectedCategoryStore selectedCategoryStore,
            SelectedSeasonStore selectedSeasonStore,
            SelectedClothesStore selectedClothesStore,
            SelectedEmployeeClothesStore selectedEmployeeClothesStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
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
                EditClothesViewModel editClothesViewModel = new EditClothesViewModel(_modalNavigationStore,
                                                                                     _categoryStore,
                                                                                     _seasonStore,
                                                                                     _selectedCategoryStore,
                                                                                     _selectedSeasonStore);

                _modalNavigationStore.CurrentViewModel = editClothesViewModel;
            }
            else
            {
                EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel(_modalNavigationStore);

                _modalNavigationStore.CurrentViewModel = editEmployeeViewModel;
            }

            i++;
        }
    }
}
